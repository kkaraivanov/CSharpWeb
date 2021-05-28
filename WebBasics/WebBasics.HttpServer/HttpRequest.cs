namespace WebBasics.HttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpRequest
    {
        public HttpRequest(string request)
        {
            var lines = request.Split(ServerConstants.NewLine);
            var startLine = lines.First().Split(" ");
            Method = ParseHttpMethod(startLine[0]);
            Url = startLine[1];
            if (Url.Contains("/favicon"))
            {
                return;
            }
            ;
            Headers = ParseHttpHeaders(lines.Skip(1));
            var bodyLines = lines.Skip(Headers.Count + 2).ToArray();
            Body = string.Join(ServerConstants.NewLine, bodyLines);
        }

        public HttpMethod Method { get; private set; }

        public string Url { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        private HttpMethod ParseHttpMethod(string method)
            => method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException($"Method '{method}' is not supported."),
            };

        private static HttpHeaderCollection ParseHttpHeaders(IEnumerable<string> headerLines)
        {
            var headerCollection = new HttpHeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var headerParts = headerLine.Split(":", 2);

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid.");
                }

                var header = new HttpHeader(headerParts[0], headerParts[1].Trim());
                headerCollection.Add(header);
            }

            return headerCollection;
        }
    }
}