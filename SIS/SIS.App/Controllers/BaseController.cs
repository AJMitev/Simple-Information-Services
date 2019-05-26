namespace SIS.App.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using HTTP.Enums;
    using HTTP.Requests.Contracts;
    using HTTP.Responses.Contracts;
    using WebServer.Results;

    public abstract class BaseController
    {
        protected IHttpRequest HttpRequest { get; set; }

        protected Dictionary<string, object> ViewData = new Dictionary<string, object>();
        

        public IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = this.ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            return htmlResult;
        }

        protected IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }

        protected bool IsLoggedIn()
        {
            return this.HttpRequest.Session.ContainsParameter("username");
        }


        private string ParseTemplate(string viewContent)
        {
            foreach (var param in this.ViewData)
            {
                viewContent = viewContent.Replace($"@Model.{param.Key}", param.Value.ToString());
            }

            return viewContent;
        }
    }
}