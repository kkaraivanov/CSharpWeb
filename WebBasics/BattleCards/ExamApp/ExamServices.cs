namespace Exam
{
    using FrameworkServer.Results.Views;
    using FrameworkServer.Services;

    public static class ExamServices
    {
        public static IServiceCollection ExamServicesRegister(this IServiceCollection service)
        {

            service.Add<IViewEngine, CompilationViewEngine>();

            return service;
        }
    }
}