namespace WebBasics.ServerFramework
{
    using System.Runtime.CompilerServices;
    using System.Text;
    using HttpServer;

    using ViewEngineModel;

    public class Controller
    {
        private const string UserSessionId = "UserId";
        private ViewEngine _viewEngine;

        public Controller()
        {
            _viewEngine = new ViewEngine();
        }

        public HttpRequest Request { get; set; }

        protected HttpResponse View(
            object model = null,
            [CallerMemberName] string path = null)
        {
            var content = System.IO.File.ReadAllText(
                "Views/" +
                GetType().Name.Replace("Controller", string.Empty) +
                "/" + path + ".cshtml");
            content = _viewEngine.GetHtml(content, model, GetUserId());

            var responseHtml = AddViewLayout(content, model);
            var response = new HttpResponse("text/html", responseHtml);

            return response;
        }

        protected HttpResponse File(string filePath, string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var file = Encoding.UTF8.GetString(fileBytes);
            var response = new HttpResponse(contentType, file);

            return response;
        }

        protected HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new HttpHeader("Location", url));
            return response;
        }

        protected HttpResponse Error(string errorText)
        {
            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">{errorText}</div>";
            var responseHtml = this.AddViewLayout(viewContent);
            var response = new HttpResponse("text/html", responseHtml, HttpStatusCode.ServerError);

            return response;
        }

        protected string GetUserId() =>
            Request.Session.ContainsKey(UserSessionId) 
                ? Request.Session[UserSessionId] 
                : null;

        private string AddViewLayout(string content, object model = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "____VIEW_GOES_HERE____");
            layout = this._viewEngine.GetHtml(layout, model, GetUserId());
            var responseHtml = layout.Replace("____VIEW_GOES_HERE____", content);
            return responseHtml;
        }
    }
}