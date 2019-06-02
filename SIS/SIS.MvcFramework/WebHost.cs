﻿namespace SIS.MvcFramework
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes.Action;
    using Attributes.Http;
    using Attributes.Security;
    using HTTP.Enums;
    using HTTP.Responses;
    using Result;
    using Routing;

    public static class WebHost
    {
        public static void Start(IMvcApplication application)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            AutoRegisterRoutes(application, serverRoutingTable);
            application.ConfigureServices();
            application.Configure(serverRoutingTable);
            var server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void AutoRegisterRoutes(
            IMvcApplication application, IServerRoutingTable serverRoutingTable)
        {
            var controllers = application.GetType().Assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract
                    && typeof(Controller).IsAssignableFrom(type));


            foreach (var controller in controllers)
            {
                var actions = controller
                    .GetMethods(BindingFlags.DeclaredOnly
                        | BindingFlags.Public
                        | BindingFlags.Instance)
                    .Where(x => !x.IsSpecialName
                                && x.DeclaringType == controller
                                && !x.IsVirtual
                                && x.GetCustomAttributes()
                                    .All(a => a.GetType() != typeof(NonActionAttribute)));

                foreach (var action in actions)
                {
                    var path = $"/{controller.Name.Replace("Controller", string.Empty)}/{action.Name}";
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
                        path = $"/{controller.Name.Replace("Controller", string.Empty)}/{attribute.ActionName}";
                    }

                    serverRoutingTable.Add(httpMethod, path, request =>
                    {
                        var controllerInstance = Activator.CreateInstance(controller);
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

                        var response = action.Invoke(controllerInstance, new object[0]) as ActionResult;
                        return response;
                    });

                    Console.WriteLine(httpMethod + " " + path);
                }
            }

            var sb = DateTime.UtcNow;
        }
    }
}