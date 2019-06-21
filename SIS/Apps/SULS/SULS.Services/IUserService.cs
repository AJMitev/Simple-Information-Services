namespace SULS.Services
{
    using Models;

    public interface IUserService
    {
        User GetUserOrNull(string username, string password);
        string CreateUser(string username, string email, string password);
        User GetUserById(string id);
    }
}