namespace ExamHelperLibrary
{
    using Data;
    using FrameworkServer;
    using FrameworkServer.Services;
    using Microsoft.EntityFrameworkCore;
    using Services;

    public static class ExamHelperServices
    {
        public static IServiceCollection ExamHelperServicesRegister(this IServiceCollection service)
        {
            service.Add<ApplicationDbContext>();
            service.Add<IPasswordHasher, PasswordHasher>();
            service.Add<IValidator, Validator>();
            return service;
        }

        public static HttpServer DatabaseMigrate(this HttpServer server)
        {
            server.WithConfiguration<ApplicationDbContext>(context => context.Database.Migrate());

            return server;
        }
    }
}
