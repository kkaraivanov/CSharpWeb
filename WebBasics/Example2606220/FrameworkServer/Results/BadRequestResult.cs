namespace FrameworkServer.Results
{
    using Http;

    public class BadRequestResult : ActionResult
    {
        public BadRequestResult(HttpResponse response) 
            : base(response)
            => this.StatusCode = HttpStatusCode.BadRequest;
    }
}
