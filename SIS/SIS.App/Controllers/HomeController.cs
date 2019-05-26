using SIS.HTTP.Cookies;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System.IO;
using System.Runtime.CompilerServices;

namespace SIS.App.Controllers
{
    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest httpRequest)
        {
            string controllerName = this.GetType().Name.Replace("Controller", "");


            string viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            HtmlResult htmlResult = new HtmlResult(viewContent, HTTP.Enums.HttpResponseStatusCode.Ok);
            htmlResult.Cookies.AddCookie(new HttpCookie("lang","en"));

            return htmlResult;
        }
    }
}