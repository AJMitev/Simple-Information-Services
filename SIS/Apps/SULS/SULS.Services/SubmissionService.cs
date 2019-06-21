namespace SULS.Services
{
    using System;
    using System.Linq;
    using Data;
    using Models;

    public class SubmissionService : ISubmissionService
    {
        private readonly SULSContext context;

        public SubmissionService(SULSContext context)
        {
            this.context = context;
        }

        public string CreateSubmission(string code, Problem problem, string userId)
        {
            Random rnd = new Random();

            var newSubmission = new Submission
            {
                ProblemId = problem.Id,
                Code = code,
                UserId =  userId,
                AchievedResult = rnd.Next(0,problem.Points+1)
            };


            this.context.Submissions.Add(newSubmission);
            this.context.SaveChanges();

            return newSubmission.Id;
        }

        public void Delete(string id)
        {
            var submission = this.context.Submissions.SingleOrDefault(x => x.Id == id);

            if (submission == null)
            {
                return;
            }

            this.context.Submissions.Remove(submission);
            this.context.SaveChanges();
        }
    }
}