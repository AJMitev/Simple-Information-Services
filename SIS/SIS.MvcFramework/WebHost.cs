namespace SIS.MvcFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes.Action;
    using Attributes.Http;
    using Attributes.Security;
    using DependencyContainer;
    using HTTP.Enums;
    using HTTP.Requests;
    using HTTP.Responses;
    using Logging;
    using Result;
    using Routing;
    using Sessions;
    using IServiceProvider = DependencyContainer.IServiceProvider;

    public static class WebHost
    {
        public static void Start(IMvcApplication application)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            IHttpSessionStorage sessionStorage = new HttpSessionStorage();
            IServiceProvider serviceProvider = new ServiceProvider();

            serviceProvider.Add<ILogger, ConsoleLogger>();
            application.ConfigureServices(serviceProvider);

            AutoRegisterRoutes(application, serverRoutingTable, serviceProvider);
            application.Configure(serverRoutingTable);
            var server = new Server(8000, serverRoutingTable, sessionStorage);
            server.Run();
        }

        private static void AutoRegisterRoutes(
            IMvcApplication application, IServerRoutingTable serverRoutingTable, IServiceProvider serviceProvider)
        {
            var controllers = application.GetType().Assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract
                    && typeof(Controller).IsAssignableFrom(type));


            foreach (var controllerType in controllers)
            {
                var actions = controllerType
                    .GetMethods(BindingFlags.DeclaredOnly
                        | BindingFlags.Public
                        | BindingFlags.Instance)
                    .Where(x => !x.IsSpecialName
                                && x.DeclaringType == controllerType
                                && !x.IsVirtual
                                && x.GetCustomAttributes()
                                    .All(a => a.GetType() != typeof(NonActionAttribute)));

                foreach (var action in actions)
                {
                    var path = $"/{controllerType.Name.Replace("Controller", string.Empty)}/{action.Name}";
                    var attribute = action.GetCustomAttributes()
                        .LastOrDefault(x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute))) as BaseHttpAttribute;
                    var httpMethod = HttpRequestMethod.Get;
                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (attribute?.Url != null)
                    {
                        path = attribute.Url;
                    }

                    if (attribute?.ActionName != null)
                    {
                        path = $"/{controllerType.Name.Replace("Controller", string.Empty)}/{attribute.ActionName}";
                    }

                    serverRoutingTable.Add(httpMethod, path,
                        (request) => ProcessRequest(serviceProvider, controllerType, request, action));

                    Console.WriteLine(httpMethod + " " + path);
                }
            }

            var sb = DateTime.UtcNow;
        }

        private static IHttpResponse ProcessRequest(IServiceProvider serviceProvider, Type controllerType, IHttpRequest request, MethodInfo action)
        {
            var controllerInstance = serviceProvider.CreateInstance(controllerType);
            ((Controller)controllerInstance).Request = request;

            // Authorization - TODO: Refactor this.
            var controllerPrinciple = ((Controller)controllerInstance).User;
            var authorizeAttribute = action.GetCustomAttributes()
                .LastOrDefault(a => a.GetType() == typeof(AuthorizeAttribute)) as AuthorizeAttribute;
            if (authorizeAttribute != null && !authorizeAttribute.IsInAuthority(controllerPrinciple))
            {
                //TODO: Redirect to configured URL
                return new HttpResponse(HttpResponseStatusCode.Forbidden);
            }

            var parameters = action.GetParameters();
            var parameterValues = new List<object>();

            foreach (var parameter in parameters)
            {
                var parameterType = parameter.ParameterType;
                var parameterName = parameter.Name.ToLower();
                ISet<string> httpDataValue = null;

                if (request.QueryData.Any(x => x.Key.ToLower().Equals(parameterName)))
                {
                    httpDataValue = request.QueryData
                        .FirstOrDefault(x => x.Key.ToLower().Equals(parameterName)).Value;
                }
                else if (request.FormData.Any(x => x.Key.ToLower().Equals(parameterName)))
                {
                    httpDataValue = request.FormData
                        .FirstOrDefault(x => x.Key.ToLower().Equals(parameterName)).Value;
                }

                //TODO: Support lists.
                string httpStringValue = httpDataValue?.FirstOrDefault();

                var parameterValue = Convert.ChangeType(httpStringValue, parameterType);
                parameterValues.Add(parameterValue);
            }

            var response = action.Invoke(controllerInstance, parameterValues.ToArray()) as ActionResult;
            return response;
        }
    }
}