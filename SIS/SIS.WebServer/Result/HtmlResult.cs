namespace SIS.WebServer.Result
{
    using System.Text;
    using WebServer.Common;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using SIS.HTTP.Responses;

    public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseStatusCode responseStatusCode = HttpResponseStatusCode.Ok)
            : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentType, GlobalConstants.ContentTypeHtml));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
