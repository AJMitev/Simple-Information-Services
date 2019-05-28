namespace SIS.WebServer
{
    using Routing;

    public interface IMvcApplication
    {
        void Configure(IServerRoutingTable serverRoutingTable);

        void ConfigureServices(); // DI
    }
}
