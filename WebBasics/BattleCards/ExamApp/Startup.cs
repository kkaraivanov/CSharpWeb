namespace Exam
{
    using System.Threading.Tasks;
    using ExamHelperLibrary;
    using ExamHelperLibrary.Services;
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
                    .ExamServicesRegister())
                .DatabaseMigrate()
                .Start();
    }
}
