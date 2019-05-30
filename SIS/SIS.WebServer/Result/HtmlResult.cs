namespace SIS.MvcFramework.Result
{
    using System.Text;
    using Common;
    using HTTP.Enums;
    using HTTP.Headers;

    public class HtmlResult : ActionResult
    {
        public HtmlResult(string content, HttpResponseStatusCode responseStatusCode = HttpResponseStatusCode.Ok)
            : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentType, GlobalConstants.ContentTypeHtml));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
