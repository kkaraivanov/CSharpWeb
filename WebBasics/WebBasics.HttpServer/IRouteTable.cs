namespace WebBasics.HttpServer
{
    using System;

    public interface IRouteTable
    {
        string Url { get; }

        HttpMethod Method { get; }

        Func<HttpRequest, HttpResponse> Action { get;}
    }
}