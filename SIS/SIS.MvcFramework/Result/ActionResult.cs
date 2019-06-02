namespace SIS.MvcFramework.Result
{
    using HTTP.Enums;
    using HTTP.Responses;

    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(HttpResponseStatusCode httpResponseStatusCode)
            : base(httpResponseStatusCode){}
    }
}