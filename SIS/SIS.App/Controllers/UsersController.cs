namespace SIS.App.Controllers
{
    using HTTP.Requests.Contracts;
    using HTTP.Responses.Contracts;

    public class UsersController : BaseController
    {
        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            //using (var context = new DemoDbContext())
            //{
            //    string username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
            //    string password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();

            //    User userFromDb = context.Users
            //        .SingleOrDefault(user => user.Username == username
            //                                 && user.Password == password);

            //    if (userFromDb == null)
            //    {
            //        return this.Redirect("/login");
            //    }

            //    httpRequest.Session.AddParameter("username"
            //        , userFromDb.Username);
            //}

            return this.Redirect("/home");
        }

        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            //using (var context = new DemoDbContext())
            //{
            //    string username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
            //    string password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();
            //    string confirmPassword = ((ISet<string>)httpRequest.FormData["confirmPassword"]).FirstOrDefault();

            //    if (password != confirmPassword)
            //    {
            //        return this.Redirect("/register");
            //    }

            //    User user = new User
            //    {
            //        Username = username,
            //        Password = password
            //    };

            //    context.Users.Add(user);
            //    context.SaveChanges();
            //}

            return this.Redirect("/login");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();

            return this.Redirect("/");
        }
    }
}