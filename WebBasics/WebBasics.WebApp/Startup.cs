namespace WebBasics.WebApp
{
    using System.Collections.Generic;
    using HttpServer;
    using ServerFramework;
    using ServerFramework.ViewEngineModel;

    public class Startup : IServerFramework
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IHttpServer,HttpServer>();
            serviceCollection.Add<IViewEngine, ViewEngine>();
            serviceCollection.Add<IView, CompileErrorView>();
        }

        public async void Configure(List<Route> routeTable)
        {
            int port = 8080;
            HttpServer server = new HttpServer(routeTable);
            await server.Run(port);
        }
    }
}