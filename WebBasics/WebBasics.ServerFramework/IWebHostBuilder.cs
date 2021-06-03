namespace WebBasics.ServerFramework
{
    using System.Collections.Generic;
    using HttpServer;

    public interface IWebHostBuilder
    {
        IWebHostBuilder ConfigureWebHostBuilder(IServerFramework serverFramework, int port);

        void Run();
    }
}