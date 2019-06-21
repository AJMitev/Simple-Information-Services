namespace SULS.App.ViewModels.Submissions
{
    using SIS.MvcFramework.Attributes.Validation;

    public class SubmissionInputModel
    {
        [RequiredSis]
        [StringLengthSis(30,800, "Code length must be between 30 and 800 symbols.")]
        public string Code { get; set; }
        public string ProblemId { get; set; }
    }
}