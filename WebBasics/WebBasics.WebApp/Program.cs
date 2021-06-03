namespace WebBasics.WebApp
{
    using System;
    using System.Collections.Generic;
    using HttpServer;
    using ServerFramework;

    class Program
    {
        static void Main(string[] args)
        {
            int port = 8080;
            HostBuilder(port).Run();
        }

        public static IWebHostBuilder HostBuilder(int port) =>
            ServerHost.CreateHost()
                .ConfigureWebHostBuilder( new Startup(), port);
    }

    internal class Startup : IServerFramework
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            
        }

        public void Configure(List<Route> routeTable)
        {
            HttpServer server = new HttpServer(routeTable);
            
        }
    }
}
