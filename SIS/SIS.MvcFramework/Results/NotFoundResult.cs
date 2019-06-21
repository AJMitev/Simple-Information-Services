namespace SIS.MvcFramework.Results
{
    using System.Text;
    using HTTP.Enums;

    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(string message, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.NotFound) : base(httpResponseStatusCode)
        {
            this.Content = Encoding.UTF8.GetBytes(message);
        }
    }
}
