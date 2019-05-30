namespace IRunes.App.Controllers
{
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;

    public class InfoController : Controller
    {
        public IHttpResponse About(IHttpRequest request)
        {
            return this.View();
        }

        public string Property { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
