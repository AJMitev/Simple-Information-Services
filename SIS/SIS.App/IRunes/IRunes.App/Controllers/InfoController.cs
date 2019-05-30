namespace IRunes.App.Controllers
{
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Result;

    public class InfoController : Controller
    {
        public ActionResult Json(IHttpRequest request)
        {
            var obj = new
            {
                Name = "Gosho",
                Age = 28,
                Occupation = "Kabeldjiq",
                Married = false,
                Gender = "Gendio"
            };

            return this.Json(obj);
        }

        public IHttpResponse About(IHttpRequest request)
        {
            return this.View();
        }
    }
}
