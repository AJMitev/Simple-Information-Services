namespace SULS.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ProblemService : IProblemService
    {
        private readonly SULSContext context;

        public ProblemService(SULSContext context)
        {
            this.context = context;
        }

        public string CreateProblem(string name, int points)
        {
            var newProblem = new Problem
            {
                Name = name,
                Points = points
            };

            this.context.Problems.Add(newProblem);
            this.context.SaveChanges();

            return newProblem.Id;
        }

        public IEnumerable<Problem> GetAll()
        {
            return this.context.Problems.Include(x=>x.Submissions).ToList();
        }

        public Problem GetById(string id)
        {
            var problemFromDb = this.context.Problems
                .Include(x => x.Submissions)
                .ThenInclude(x=>x.User)
                .SingleOrDefault(x => x.Id == id);

            return problemFromDb;
        }
    }
}