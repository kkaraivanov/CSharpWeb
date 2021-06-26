namespace SharedTrip
{
    using System.Threading.Tasks;
    using ExamHelperLibrary;
    using FrameworkServer;
    using FrameworkServer.Controllers;

    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .ExamHelperServicesRegister()
                    .SharedTripServicesRegister())
                .DatabaseMigrate()
                .Start();
    }
}
