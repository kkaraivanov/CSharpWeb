namespace ExamHelperLibrary.Template
{
    using FrameworkServer.Controllers;
    using FrameworkServer.Http;
    using Services;

    public class BaseControllerTemplate : Controller
    {
        protected ObjectAttributeValidator AttributeValidator;

        protected HttpResponse Refresh() => 
            Redirect("/");

        protected BaseControllerTemplate() =>
            AttributeValidator = new ObjectAttributeValidator();
    }
}