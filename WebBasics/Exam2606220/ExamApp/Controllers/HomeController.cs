namespace SharedTrip.Controllers
{
    using ExamHelperLibrary.Template;
    using FrameworkServer.Http;

    public class HomeController : BaseControllerTemplate
    {
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}