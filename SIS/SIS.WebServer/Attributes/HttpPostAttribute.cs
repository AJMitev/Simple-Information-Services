namespace SIS.WebServer.Attributes
{
    using HTTP.Enums;

    public class HttpPostAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.Post;
    }
}
