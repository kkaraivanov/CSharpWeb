namespace SharedTrip
{
    using FrameworkServer.Results.Views;
    using FrameworkServer.Services;

    public static class SharedTripServices
    {
        public static IServiceCollection SharedTripServicesRegister(this IServiceCollection service)
        {
            // TODO: Add services in collections
            service.Add<IViewEngine, CompilationViewEngine>();

            return service;
        }
    }
}