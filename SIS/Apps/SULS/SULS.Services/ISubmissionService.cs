using SULS.Models;

namespace SULS.Services
{
    public interface ISubmissionService
    {
        string CreateSubmission(string code, Problem problem, string userId);
        void Delete(string id);
    }
}