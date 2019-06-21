namespace SULS.Services
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Data;
    using Models;

    public class UserService : IUserService
    {
        private readonly SULSContext context;

        public UserService(SULSContext context)
        {
            this.context = context;
        }

        public string CreateUser(string username, string email, string password)
        {
            var newUser = new User
            {
                Username = username,
                Email =  email,
                Password = this.HashPassword(password)
            };

            this.context.Users.Add(newUser);
            this.context.SaveChanges();

            return newUser.Id;
        }

        public User GetUserById(string id)
        {
            return this.context.Users.SingleOrDefault(x => x.Id == id);
        }

        public User GetUserOrNull(string username, string password)
        {
            var passwordHash = this.HashPassword(password);
            var user = this.context.Users.FirstOrDefault(
                x => x.Username == username
                     && x.Password == passwordHash);
            return user;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}