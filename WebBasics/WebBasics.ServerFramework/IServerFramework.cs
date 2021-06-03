namespace WebBasics.ServerFramework
{
    using System.Collections.Generic;
    using HttpServer;

    public interface IServerFramework
    {
        void ConfigureServices(IServiceCollection serviceCollection);

        void Configure(List<Route> routeTable);
    }
}