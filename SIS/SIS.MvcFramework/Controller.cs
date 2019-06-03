namespace SIS.MvcFramework
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Extensions;
    using HTTP.Requests;
    using Identity;
    using Result;
    using ViewEngine;

    public abstract class Controller
    {
        private readonly IViewEngine viewEngine;
        protected Controller()
        {
            ViewData = new Dictionary<string, object>();
            viewEngine = new SisViewEngine();
        }

        protected Dictionary<string, object> ViewData { get; private set; }

        //TODO: Refactor.
        public Principal User => this.Request.Session.ContainsParameter("principal")
            ? this.Request.Session.GetParameter("principal") as Principal
            : null;

        public IHttpRequest Request { get; set; }

        protected bool IsLoggedIn()
        {
            return this.Request.Session.ContainsParameter("principal");
        }

        protected void SignIn(string id, string username, string email)
        {
            var principal = new Principal
            {
                Id = id,
                Username = username,
                Email = email
            };

            this.Request.Session.AddParameter("principal", principal);
        }

        protected void SignOut()
        {
            this.Request.Session.ClearParameters();
        }

        protected ActionResult View([CallerMemberName]string view = null)
        {
            return this.View<object>(null,view);
        }

        protected ActionResult View<T>(T model = null, [CallerMemberName] string view = null)
            where T : class
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = System.IO.File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");
            viewContent = this.viewEngine.GetHtml(viewContent, model);

            string layoutContent = System.IO.File.ReadAllText("Views/_Layout.html");
            layoutContent = this.viewEngine.GetHtml(layoutContent, model);
            layoutContent = layoutContent.Replace("@RenderBody()", viewContent);


            var htmlResult = new HtmlResult(layoutContent);

            return htmlResult;
        }

        protected ActionResult Redirect(string url)
        {
            return new RedirectResult(url);
        }

        protected ActionResult Xml(object content)
        {

            return new XmlResult(content.ToXml());
        }

        protected ActionResult Json(object content)
        {
            return new JsonResult(content.ToJson());
        }

        protected ActionResult File(byte[] content)
        {
            return new FileResult(content);
        }
    }
}
