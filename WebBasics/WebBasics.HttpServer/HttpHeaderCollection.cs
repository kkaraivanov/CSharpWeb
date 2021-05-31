namespace WebBasics.HttpServer
{
    using System.Collections.Generic;

    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> _headers;

        public HttpHeaderCollection() => 
            _headers = new Dictionary<string, HttpHeader>();

        public int Count => 
            _headers.Count;

        public void Add(HttpHeader header) => 
            _headers.Add(header.Name, header);

        public IReadOnlyDictionary<string, HttpHeader> Headers => 
            _headers;
    }
}