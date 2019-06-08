namespace SIS.MvcFramework
{
    using DependencyContainer;
    using Routing;

    public interface IMvcApplication
    {
        void Configure(IServerRoutingTable serverRoutingTable);

        void ConfigureServices(IServiceProvider serviceProvider);
    }
}
