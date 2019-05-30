namespace SIS.MvcFramework.Result
{
    using System.Text;
    using HTTP.Enums;
    using HTTP.Headers;

    public class XmlResult : ActionResult
    {
        public XmlResult(string content, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) 
            : base(httpResponseStatusCode)
        {
            this.AddHeader(new HttpHeader(HttpHeader.ContentType, "application/xml"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}