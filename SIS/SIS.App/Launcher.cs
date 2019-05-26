namespace SIS.App
{
    using Data;
    using HTTP.Enums;
    using SIS.App.Controllers;
    using WebServer;
    using WebServer.Routing;
    using WebServer.Routing.Contracts;

    public class Launcher
    {
        static void Main()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }

            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            // GET
            serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new HomeController(request).Index());
            serverRoutingTable.Add(HttpRequestMethod.Get, "/home", request => new HomeController(request).Home());
            serverRoutingTable.Add(HttpRequestMethod.Get, "/login", request => new UsersController().Login());
            serverRoutingTable.Add(HttpRequestMethod.Get, "/register", request => new UsersController().Register());
            serverRoutingTable.Add(HttpRequestMethod.Get, "/logout", request => new UsersController().Logout(request));

            // POST
            serverRoutingTable.Add(HttpRequestMethod.Post, "/register", request => new UsersController().RegisterConfirm(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/login", request => new UsersController().LoginConfirm(request));

            Server server = new Server(8000,serverRoutingTable);
            server.Run();
        }
    }
}
