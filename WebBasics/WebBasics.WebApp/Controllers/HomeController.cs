namespace WebBasics.WebApp.Controllers
{
    using HttpServer;
    using ServerFramework;

    public class HomeController : Controller
    {
        public HttpResponse Index()
        {

            return this.View();
        }
    }
}