namespace SIS.MvcFramework.Result
{
    using System.Text;
    using Common;
    using HTTP.Enums;
    using HTTP.Headers;

    public class TextResult : ActionResult
    {
        public TextResult(string content, HttpResponseStatusCode responseStatusCode, 
            string contentType = GlobalConstants.ContentTypePlainText) : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentType, contentType));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

        public TextResult(byte[] content, HttpResponseStatusCode responseStatusCode,
            string contentType = GlobalConstants.ContentTypePlainText) : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentType, contentType));
            this.Content = content;
        }
    }
}
