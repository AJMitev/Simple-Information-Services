namespace SULS.App.Controllers
{
    using Models;
    using Services;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Mapping;
    using SIS.MvcFramework.Results;
    using ViewModels.Submissions;

    public class SubmissionsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly ISubmissionService submissionService;
        private readonly IUserService userService;

        public SubmissionsController(IProblemService problemService, ISubmissionService submissionService, IUserService userService)
        {
            this.problemService = problemService;
            this.submissionService = submissionService;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create(string id)
        {
            var problem = this.problemService.GetById(id).To<SubmissionCreateViewModel>();
            problem.ProblemId = id;
            
            return this.View(problem);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(SubmissionInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect($"/Submissions/Create?id={input.ProblemId}");
            }

            var problem = this.problemService.GetById(input.ProblemId);
            this.submissionService.CreateSubmission(input.Code, problem, this.User.Id);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize()]
        public IActionResult Delete(string id)
        {
           this.submissionService.Delete(id);

           return this.Redirect("/");
        }
    }
}