namespace SULS.App.Controllers
{
    using System.Linq;
    using Services;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Results;
    using ViewModels.Problems;
    using ViewModels.Submissions;

    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ProblemCreateInputModel input)
        {
            if (!ModelState.IsValid)
            {
               return this.Redirect("/Problems/Create");
            }

            this.problemService.CreateProblem(input.Name, input.Points);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            var problem = this.problemService.GetById(id);

            var viewModel = new ProblemDetailsViewModel
            {
                Name = problem.Name,
                Submissions = problem.Submissions.Select(x => new SubmissionDetailsViewModel
                {
                    Username = x.User.Username,
                    CreatedOn = x.CreatedOn,
                    AchievedResult = x.AchievedResult,
                    MaxPoints = x.Problem.Points,
                    SubmissionId = x.Id
                }).ToList()
            };

            return this.View(viewModel);
        }

        
    }
}