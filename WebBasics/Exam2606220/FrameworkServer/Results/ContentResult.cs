namespace FrameworkServer.Results
{
    using Http;

    public class ContentResult : ActionResult
    {
        public ContentResult(
            HttpResponse response,
            string content, 
            string contentType)
            : base(response) 
            => this.SetContent(content, contentType);
    }
}
