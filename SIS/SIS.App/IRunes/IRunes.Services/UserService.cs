namespace IRunes.Services
{
    using System.Linq;
    using Data;
    using Models;

    public class UserService : IUserService
    {
        private readonly RunesDbContext context;

        public UserService()
        {
            this.context = new RunesDbContext();
        }

        public User Add(User user)
        {
            User savedEntry = context.Users.Add(user).Entity;
            context.SaveChanges();

            return savedEntry;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return this.context.Users.SingleOrDefault(user => (user.Username == username
                                                               || user.Email == username)
                                                              && user.Password == password);
        }
    }
}