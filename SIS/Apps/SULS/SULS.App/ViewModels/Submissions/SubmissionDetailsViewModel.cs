namespace SULS.App.ViewModels.Submissions
{
    using System;

    public class SubmissionDetailsViewModel
    {
        public string SubmissionId { get; set; }

        public string Username { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}