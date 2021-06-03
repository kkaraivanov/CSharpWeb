namespace WebBasics.HttpServer.Helpers
{
    using System.Text;

    public class TextResult : HttpResponse
    {
        public TextResult(string contentType, string content, HttpStatusCode statusCode) 
            : base(contentType, content, statusCode)
        {
            contentType = "text/plain; charset=utf-8";
            Headers.Add(new HttpHeader("Content-Type", contentType));
            Content = content;
        }
    }
}