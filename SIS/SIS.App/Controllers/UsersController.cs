namespace SIS.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using HTTP.Requests.Contracts;
    using HTTP.Responses.Contracts;
    using Models;

    public class UsersController : BaseController
    {
        public IHttpResponse Login()
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new AppDbContext())
            {
                string username = httpRequest.FormData["username"]?.ToString();
                string password = httpRequest.FormData["password"]?.ToString();

                User userFromDb = context.Users
                    .SingleOrDefault(user => user.Username == username
                                             && user.Password == password);

                if (userFromDb == null)
                {
                    return this.Redirect("/login");
                }

                httpRequest.Session.AddParameter("username", userFromDb.Username);
            }

            return this.Redirect("/home");
        }

        public IHttpResponse Register()
        {
            return this.View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var context = new AppDbContext())
            {
                string username = httpRequest.FormData["username"]?.ToString();
                string password = httpRequest.FormData["password"]?.ToString();
                string confirmPassword = httpRequest.FormData["confirmPassword"]?.ToString();

                if (password != confirmPassword)
                {
                    return this.Redirect("/register");
                }

                User user = new User
                {
                    Username = username,
                    Password = password
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return this.Redirect("/login");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();

            return this.Redirect("/");
        }
    }
}