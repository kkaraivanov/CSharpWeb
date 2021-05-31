using System;

namespace WebBasics.WebApp
{
    using System.Threading.Tasks;
    using HttpServer;

    class Program
    {
        static async Task Main(string[] args)
        {
            int port = 8080;
            var server = new HttpServer();
            await server.Run(port);
        }
    }
}
