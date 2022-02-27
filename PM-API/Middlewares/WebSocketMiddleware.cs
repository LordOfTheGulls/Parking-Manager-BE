using System.Net.WebSockets;
using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PM_API.Middlewares
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly WebSocketServerManager _serverManager;

        public WebSocketMiddleware(RequestDelegate next, WebSocketServerManager serverManager)
        {
            _next          = next;
            _serverManager = serverManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine(context.Request.Path.ToString());

            if (context.WebSockets.IsWebSocketRequest && context.Request.Path == "/ws")
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string connectionID = _serverManager.AddSocket(webSocket);

                await SendConnectionIDAsnyc(webSocket, connectionID);

                Console.WriteLine("Connection Established, Socket Created!");

                await ReceiveMessage(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine($"Message is: {Encoding.UTF8.GetString(buffer)}");
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine("Received Close Message");

                        string id = _serverManager.GetAllSockets().FirstOrDefault(s => s.Value == webSocket).Key;

                        _serverManager.GetAllSockets().TryRemove(id, out WebSocket socket);

                        await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, cancellationToken: CancellationToken.None);

                        return;
                    }
                });
            }
            else
            {
                await _next(context);
            }
        }

        private async Task SendConnectionIDAsnyc(WebSocket socket, string connectionID)
        {
            var buffer = Encoding.UTF8.GetBytes($"ConnectionID: {connectionID}");

            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken: CancellationToken.None);
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
