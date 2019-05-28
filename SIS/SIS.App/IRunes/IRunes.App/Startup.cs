﻿namespace IRunes.App
{
    using IRunes.Data;
    using SIS.WebServer;
    using SIS.WebServer.Routing;

    public class Startup : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        public void ConfigureServices()
        {
        }
    }
}
