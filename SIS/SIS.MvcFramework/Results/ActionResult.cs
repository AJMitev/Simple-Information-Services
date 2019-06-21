namespace SIS.MvcFramework.Results
{
    using HTTP.Enums;
    using HTTP.Responses;

    public abstract class ActionResult : HttpResponse, IActionResult
    {
        protected ActionResult(HttpResponseStatusCode httpResponseStatusCode) : base(httpResponseStatusCode)
        {
        }
    }
}
