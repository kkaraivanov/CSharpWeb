namespace FrameworkServer.Results
{
    using Http;

    public class HtmlResult : ContentResult
    {
        public HtmlResult(HttpResponse response, string html) 
            : base(response, html, HttpContentType.Html)
        {
        }
    }
}
