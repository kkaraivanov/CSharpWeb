namespace WebBasics.HttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpServer
    {
        private readonly IPAddress _ipAddress;
        private readonly int _port;
        private readonly TcpListener _listener;
        private readonly ConsoleColor _consoleColor;

        public HttpServer(int port, string ipAddress = null)
        {
            _ipAddress = ipAddress != null ? IPAddress.Parse(ipAddress) : IPAddress.Loopback;
            _port = port;
            _listener = new TcpListener(_ipAddress, _port);
            _consoleColor = Console.ForegroundColor;
        }

        public async Task Start()
        {
            _listener.Start();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Server started on IP address {_ipAddress.ToString()} with port {_port}...");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Listening for requests...");
            Console.ForegroundColor = _consoleColor;

            while (true)
            {
                var connection = await _listener.AcceptTcpClientAsync();
                await ConnectionRead(connection);
            }
        }

        private async Task ConnectionRead(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();
                var requestBytes = await ReadRequest(stream);
                var requestText = Encoding.UTF8.GetString(requestBytes);
                Console.WriteLine(requestText);

                var request = new HttpRequest(requestText);
                var content = "Hallo my world";
                var response = new HttpResponse("text/html; charset=UTF-8", content);
                response.Headers.Add(new HttpHeader("Server", ServerConstants.ServerName));
                response.Headers.Add(new HttpHeader("Date", $"{DateTime.UtcNow:r}"));

                var responseText = response.ToString();
                ;
                var responseHeader = Encoding.UTF8.GetBytes(responseText);
                
                await stream.WriteAsync(responseHeader);
                if (!string.IsNullOrEmpty(response.Content))
                {
                    var responseContent = Encoding.UTF8.GetBytes(response.Content);
                    await stream.WriteAsync(responseContent, 0, responseContent.Length);
                }
                //await WriteResponse(stream);
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        private async Task WriteResponse(NetworkStream networkStream)
        {
            var content = @"
<html>
    <head>
        <link rel=""icon"" href=""data:,"">
    </head>
    <body>
        Hello from my server!
    </body>
</html>";
            var contentLength = Encoding.UTF8.GetByteCount(content);

            var response = $@"
HTTP/1.1 200 OK
Server: My Web Server
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/html; charset=UTF-8
{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);
        }
    }
}