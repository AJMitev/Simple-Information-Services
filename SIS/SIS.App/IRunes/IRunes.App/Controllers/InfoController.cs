namespace IRunes.App.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Result;

    public class InfoController : Controller
    {
        public ActionResult Json()
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

        public ActionResult About()
        {
            return this.View();
        }
    }
}
