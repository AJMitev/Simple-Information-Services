using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using System.Collections.Generic;

namespace IRunes.App.Controllers
{
    using SIS.MvcFramework.Results;

    public class HomeController : Controller
    {
        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Test(IEnumerable<string> list)
        {
            return this.View();
        }
    }
}
