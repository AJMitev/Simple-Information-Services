namespace SULS.App.Controllers
{
    using System.Linq;
    using Services;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Results;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IProblemService problemService;

        public HomeController(IProblemService problemService)
        {
            this.problemService = problemService;
        }


        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                var allProblems = this.problemService.GetAll()
                    .Select(x => new HomeProblemsListViewModel
                     {
                         Count = x.Submissions.Count,
                         Name = x.Name,
                         Id = x.Id
                     })
                    .ToList();

                return this.View(allProblems, "IndexLoggedIn");
            }
            else
            {
                return this.View();
            }
        }
    }
}