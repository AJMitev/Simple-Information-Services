namespace SIS.App
{
    using SIS.App.Controllers;
    using WebServer;
    using WebServer.Routing;
    using WebServer.Routing.Contracts;

    public class Launcher
    {
        static void Main(string[] args)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(HTTP.Enums.HttpRequestMethod.Get, "/", request => new HomeController(request).Index(request));

            Server server = new Server(8000,serverRoutingTable);
            server.Run();
        }
    }
}
