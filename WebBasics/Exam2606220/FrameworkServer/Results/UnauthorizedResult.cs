﻿namespace FrameworkServer.Results
{
    using Http;

    public class UnauthorizedResult : ActionResult
    {
        public UnauthorizedResult(HttpResponse response)
            : base(response)
            => this.StatusCode = HttpStatusCode.Unauthorized;
    }
}
