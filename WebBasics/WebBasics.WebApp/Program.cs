namespace WebBasics.WebApp
{
    using System;
    using System.Threading.Tasks;
    using ServerFramework;

    class Program
    {
        static async Task Main(string[] args)
        {
            await ServerHost.CreateHost(new Startup());
        }
    }
}
