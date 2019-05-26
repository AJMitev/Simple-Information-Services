﻿namespace SIS.App.Controllers
{
    using HTTP.Requests.Contracts;
    using HTTP.Responses.Contracts;

    public class HomeController : BaseController
    {
        public HomeController(IHttpRequest httpRequest)
        {
            this.HttpRequest = httpRequest;
        }

        public IHttpResponse Index()
        {
            return this.View();
        }

        public IHttpResponse Home()
        {
            if (!this.IsLoggedIn())
            {
                return this.Redirect("/login");
            }

            this.ViewData["Username"] = this.HttpRequest.Session.GetParameter("username");
            return this.View();
        }
    }
}