namespace IRunes.App.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes.Http;
    using SIS.MvcFramework.Result;
    using ViewModels;

    public class HomeController : Controller
    {
        [HttpGet(Url = "/")]
        public ActionResult IndexSlash()
        {
            return Index();
        }

        public ActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                var model = new UserHomeViewModel { Username = this.User.Username };

                return this.View(model, "Home");
            }

            return this.View();
        }
    }
}
