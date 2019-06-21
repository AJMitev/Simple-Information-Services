namespace SULS.App.ViewModels.Problems
{
    using SIS.MvcFramework.Attributes.Validation;

    public class ProblemCreateInputModel
    {
        [RequiredSis]
        [StringLengthSis(5,20, "Problem name should be between 5 and 20 symbols.")]
        public string Name { get; set; }

        [RequiredSis]
        [RangeSis(50,300, "Points value should be an integer between 50 and 300.")]
        public int Points { get; set; }
    }
}