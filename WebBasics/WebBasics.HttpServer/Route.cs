namespace WebBasics.HttpServer
{
    using System;

    public class Route
    {
        public Route(string url, HttpMethod method, Func<HttpRequest, HttpResponse> action)
        {
            Url = url;
            Method = method;
            Action = action;
        }

        public string Url { get; set; }

        public HttpMethod Method { get; set; }

        public Func<HttpRequest, HttpResponse> Action { get; set; }
    }
}