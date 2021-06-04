namespace WebBasics.ServerFramework
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Extensions;
    using HttpServer;

    public static class ServerHost
    {
        public static async Task CreateHost(IServerFramework serverFramework)
        {
            List<Route> routeTable = new List<Route>();
            IServiceCollection serviceCollection = new ServiceCollection();

            serverFramework.ConfigureServices(serviceCollection);
            serverFramework.Configure(routeTable);
            ;
            routeTable.Register(serverFramework, serviceCollection);

            foreach (var route in routeTable)
            {
                Console.WriteLine($"{route.Method} {route.Url}");
            }

            IHttpServer server = new HttpServer(routeTable);
            await server.Run(80);
        }
    }
}