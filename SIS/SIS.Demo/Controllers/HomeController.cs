namespace SIS.Demo.Controllers
{
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;

    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            string content = "<h1>Hello World!</h1>";

            return new HtmlResult(content, HTTP.Enums.HttpResponseStatusCode.Ok);
        }
    }
}