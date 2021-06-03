namespace WebBasics.ServerFramework
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Extensions;
    using HttpServer;

    public static class ServerHost
    {
        private static IWebHostBuilder _host;

        public static IWebHostBuilder CreateHost()
        {
            _host = new WebHostBuilder();
            return _host;
        }

        public static IHttpServer CreateHostAsync(IServerFramework serverFramework)
        {
            List<Route> routeTable = new List<Route>();
            IServiceCollection serviceCollection = new ServiceCollection();

            serverFramework.ConfigureServices(serviceCollection);
            serverFramework.Configure(routeTable);

            routeTable.Register(serverFramework, serviceCollection);
            return new HttpServer();
        }
    }
}