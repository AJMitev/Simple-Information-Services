namespace SIS.MvcFramework.Result
{
    using System.Text;
    using HTTP.Enums;
    using HTTP.Headers;

    public class JsonResult : ActionResult
    {
        public JsonResult(string content, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) 
            : base(httpResponseStatusCode)
        {
            this.AddHeader(new HttpHeader(HttpHeader.ContentType, "application/json"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}