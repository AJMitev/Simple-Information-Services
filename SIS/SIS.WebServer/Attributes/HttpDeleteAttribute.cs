namespace SIS.WebServer.Attributes
{
    using HTTP.Enums;

    public class HttpDeleteAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.Delete;
    }
}
