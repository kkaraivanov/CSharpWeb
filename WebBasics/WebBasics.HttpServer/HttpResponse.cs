namespace WebBasics.HttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class HttpResponse
    {
        public HttpResponse(string contentType, string content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            StatusCode = statusCode;
            Content = content;
            var headers = new List<HttpHeader>
            {
                {new HttpHeader("Content-Type", contentType) },
                {new HttpHeader("Content-Length", content.Length.ToString()) },
            };
            
            headers.ForEach(h =>
            {
                Headers.Add(h);
            });
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            //sb.Append($"HTTP/1.1 200 {StatusCode}" + ServerConstants.NewLine);
            sb.Append($"HTTP/1.1 {(int)StatusCode} {StatusCode}" + ServerConstants.NewLine);
            var cultureInfo = CultureInfo.CurrentCulture;
            var regioneInfo = new RegionInfo(cultureInfo.LCID);
            var culture = regioneInfo.TwoLetterISORegionName;
            sb.Append($"Location: {culture}" + ServerConstants.NewLine);
            foreach (var header in Headers.Headers.Values)
            {
                //sb.Append($"{header.Name}: {header.Value}" + ServerConstants.NewLine);
                sb.Append($"{header.ToString()}" + ServerConstants.NewLine);
            }

            sb.Append(ServerConstants.NewLine);
            return sb.ToString();
        }

        public HttpStatusCode StatusCode { get; set; }

        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();

        public string Content { get; set; }
    }
}