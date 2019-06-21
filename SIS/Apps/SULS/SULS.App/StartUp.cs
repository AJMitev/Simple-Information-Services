namespace SULS.App
{
    using Data;
    using Services;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Routing;
    using IServiceProvider = SIS.MvcFramework.DependencyContainer.IServiceProvider;

    public class StartUp : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var db = new SULSContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        { 
            serviceProvider.Add<IUserService,UserService>();
            serviceProvider.Add<IProblemService,ProblemService>();
            serviceProvider.Add<ISubmissionService,SubmissionService>();
        }
    }
}