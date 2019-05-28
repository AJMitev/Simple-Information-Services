namespace IRunes.App
{
    using SIS.WebServer;

    public static class Program
    {
        public static void Main()
        {
            WebHost.Start(new Startup());
        }
    }
}
