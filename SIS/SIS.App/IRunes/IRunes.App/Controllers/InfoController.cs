namespace IRunes.App.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Result;

    public class InfoController : Controller
    {        
        public ActionResult About()
        {
            return this.View();
        }
    }
}
