namespace Exam.Controllers
{
    using ExamHelperLibrary.Template;
    using FrameworkServer.Http;

    public class HomeController : BaseControllerTemplate
    { 
        public HttpResponse Index()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Cards/All");
            }

            return View();
        }
    }
}