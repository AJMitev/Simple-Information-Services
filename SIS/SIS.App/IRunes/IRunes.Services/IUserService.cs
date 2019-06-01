namespace IRunes.Services
{
    using Models;

    public interface IUserService
    {
        User Add(User user);
        User GetUserByUsernameAndPassword(string username, string password);
    }
}
