namespace WebBasics.HttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpServer : IHttpServer
    {
        private readonly List<Route> _routTable;
        private readonly ConsoleColor _consoleColor;
        private IPAddress _ipAddress;
        private int _port;
        private TcpListener _listener;

        public HttpServer()
        {
            _routTable = new List<Route>();
            _consoleColor = Console.ForegroundColor;
        }

        public HttpServer(List<Route> routTable)
            :this()
        {
            _routTable = routTable;
        }

        public async Task Run(int port, string ipAddress = null)
        {
            _ipAddress = ipAddress != null ? IPAddress.Parse(ipAddress) : IPAddress.Loopback;
            _port = port;
            _listener = new TcpListener(_ipAddress, _port);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Server started at http://{_ipAddress.ToString()}:{_port}");

            _listener.Start();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Connection is started");
            //Console.WriteLine("Waiting for request...");
            Console.WriteLine();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Waiting for new request...");
                var client = await _listener.AcceptTcpClientAsync();
                await Listen(client);
            }
        }

        private async Task Listen(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();
                var requestBytes = await ReadRequest(stream);
                var requestText = Encoding.UTF8.GetString(requestBytes);
                var request = new HttpRequest(requestText);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Incoming request...");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Method \"{request.Method}\"");
                Console.WriteLine($"Request url http://{_ipAddress.ToString()}:{_port}{request.Url}");
                Console.WriteLine($"Headers count {request.Headers.Count} headers");
                Console.WriteLine($"Cookies {request.Cookies.FirstOrDefault()?.Name}: {request.Cookies.FirstOrDefault()?.Value}");

                if (_routTable.Any())
                {
                    await MakeResponse(stream, request);
                }
                else
                {
                    var content = "Hallo my world";
                    var response = new HttpResponse("text/html; charset=UTF-8", content);
                    response.Headers.Add(new HttpHeader("Server", ServerConstants.ServerName));
                    response.Headers.Add(new HttpHeader("Date", $"{DateTime.UtcNow:r}"));

                    var session = request.Cookies.FirstOrDefault(x => x.Name == ServerConstants.CookieName);
                    ;
                    if (session != null)
                    {
                        var sendingCookie = new SendingCookie(session.Name, session.Value);
                        sendingCookie.Url = "/";
                        response.Cookies.Add(sendingCookie);
                    }

                    var responseText = response.ToString();
                    var responseHeader = Encoding.UTF8.GetBytes(responseText);
                    await stream.WriteAsync(responseHeader);
                    
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        var responseContent = Encoding.UTF8.GetBytes(response.Content);
                        await stream.WriteAsync(responseContent, 0, responseContent.Length);
                    }
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Server send response...");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(responseText.TrimEnd('\n', '\r'));
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Client connection is closed...");
                Console.WriteLine();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task MakeResponse(NetworkStream stream, HttpRequest request)
        {
            HttpResponse response;
            var route = _routTable
                .FirstOrDefault(x =>
                    String.Compare(x.Url, request.Url, StringComparison.OrdinalIgnoreCase) == 0 &&
                    x.Method == request.Method);

            response = route != null ?
                route.Action(request) :
                new HttpResponse("text/html; charset=UTF-8", string.Empty, HttpStatusCode.NotFound);

            response.Headers.Add(new HttpHeader("Server", ServerConstants.ServerName));
            response.Headers.Add(new HttpHeader("Date", $"{DateTime.UtcNow:r}"));

            var session = request.Cookies.FirstOrDefault(x => x.Name == ServerConstants.CookieName);
            if (session != null)
            {
                var sendingCookie = new SendingCookie(session.Name, session.Value);
                sendingCookie.Url = "/";
                response.Cookies.Add(sendingCookie);

            }

            var responseText = response.ToString();
            var responseHeaders = Encoding.UTF8.GetBytes(responseText);
            await stream.WriteAsync(responseHeaders);

            if (!string.IsNullOrEmpty(response.Content))
            {
                var responseContent = Encoding.UTF8.GetBytes(response.Content);
                await stream.WriteAsync(responseContent, 0, responseContent.Length);
            }
        }

        private async Task<byte[]> ReadRequest(NetworkStream networkStream)
        {
            var result = new List<byte>();
            int offset = 0;
            byte[] buffer = new byte[ServerConstants.BufferSize];

            while (true)
            {
                var currentReadCount = await networkStream.ReadAsync(buffer, offset, buffer.Length);
                offset += currentReadCount;

                if (currentReadCount < buffer.Length)
                {
                    var readByte = new byte[currentReadCount];
                    Array.Copy(buffer, readByte, currentReadCount);
                    result.AddRange(readByte);
                    return result.ToArray();
                }
                else
                {
                    result.AddRange(buffer);
                }
            }

            return result.ToArray();
        }
    }
}