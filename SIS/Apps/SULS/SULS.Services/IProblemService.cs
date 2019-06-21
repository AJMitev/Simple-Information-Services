namespace SULS.Services
{
    using System.Collections.Generic;
    using Models;

    public interface IProblemService
    {
        string CreateProblem(string name, int points);
        IEnumerable<Problem> GetAll();
        Problem GetById(string id);
    }
}