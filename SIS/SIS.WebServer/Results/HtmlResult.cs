namespace SIS.WebServer.Results
{
    using System.Text;
    using Common;
    using HTTP.Enums;
    using HTTP.Headers;
    using HTTP.Responses;

    public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseStatusCode responseStatusCode)
        : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentTypeKey, GlobalConstants.ContentTypeHtml));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}