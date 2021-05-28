using System;

namespace WebBasics.HttpServer
{
    using System.Threading.Tasks;

    class Program
    {
        public static async Task Main(string[] args)
        {
            var address = "127.0.0.1";
            var port = 8080;

            var server = new HttpServer(8080);
            await server.Start();
        }
    }
}
