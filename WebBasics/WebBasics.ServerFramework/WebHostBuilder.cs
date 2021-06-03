using System;

namespace WebBasics.ServerFramework
{
    using System.Collections.Generic;
    using HttpServer;

    public class WebHostBuilder : IWebHostBuilder
    {
        private IServerFramework _serviceFramework;
        private IHttpServer _httpServer;
        private List<Route> _routeTable;
        private int _httpPort = 80;

        public IWebHostBuilder ConfigureWebHostBuilder(IServerFramework serverFramework, int port)
        {
            _httpPort = port;
            _httpServer = ServerHost.CreateHostAsync(serverFramework);
            ;

            return this;
        }

        public void Run()
        {
            _httpServer.Run(_httpPort);
        }
    }
}
