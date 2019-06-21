using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Web.Controllers
{
    using SIS.MvcFramework.Results;

    public class HomeController : Controller
    {
        // /
        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        // /Home/Index
        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                return this.View("IndexLoggedIn");
            }
            else
            {
                return this.View();
            }
        }
    }
}
