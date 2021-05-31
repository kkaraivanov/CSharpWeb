namespace WebBasics.HttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HttpRequest
    {
        private static readonly IDictionary<string, Dictionary<string, string>> _sessionsStore = new Dictionary<string, Dictionary<string, string>>();

        public HttpRequest(string request)
        {
            var lines = request.Split(ServerConstants.NewLine);
            var startLine = lines.First().Split(" ");
            Method = ParseHttpMethod(startLine[0]);
            Url = startLine[1];
            Headers = ParseHttpHeaders(lines.Skip(1));
            Cookies = GetCookies();

            var sesion = Cookies.FirstOrDefault(x => x.Name == ServerConstants.CookieName);
            ;
            if (sesion == null)
            {
                var id = Guid.NewGuid().ToString();
                Session = new Dictionary<string, string>();
                _sessionsStore.Add(id, Session);
                Cookies.Add(new Cookie(ServerConstants.CookieName, id));
            }
            else if (!_sessionsStore.ContainsKey(sesion.Value))
            {
                Session = new Dictionary<string, string>();
                _sessionsStore.Add(sesion.Value, Session);
            }
            else
            {
                Session = _sessionsStore[sesion.Value];
            }

            Body = GetBody(lines);
        }
        
        public HttpMethod Method { get; private set; }

        public string Url { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public ICollection<Cookie> Cookies { get; set; }

        public Dictionary<string, string> Session { get; set; }

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

        private ICollection<Cookie> GetCookies()
        {
            var result = new List<Cookie>();
            if (Headers.Headers.Any(x => x.Key == ServerConstants.CookieHeader))
            {
                var cookiesHeader = Headers.Headers.FirstOrDefault(x =>
                    x.Key == ServerConstants.CookieHeader).Value;

                var cookies = cookiesHeader.Value.Split("; ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookie in cookies)
                {
                    result.Add(new Cookie(cookie));
                }
            }

            return result;
        }

        private string GetBody(string[] lines)
        {
            int index = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (index < lines.Length)
            {
                var line = lines[index++];

                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }

                if (!isInHeaders)
                {
                    bodyBuilder.AppendLine(line);
                }
            }

            ;
            return bodyBuilder.ToString().TrimEnd('\n', '\r');
        }
    }
}