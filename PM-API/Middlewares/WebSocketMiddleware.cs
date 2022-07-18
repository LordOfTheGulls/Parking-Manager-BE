using System.Net.WebSockets;
using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PM_Common.DTO;
using PM_Common.Json;
using PM_DAL.UOW;
using PM_CQRS.Dispatcher;
using PM_CQRS.Commands;
using PM_API.Services;

namespace PM_API.Middlewares
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly WebSocketServerManager _serverManager;

        private readonly ParkingSocketManager _parkingSocketManager;

        public WebSocketMiddleware(RequestDelegate next, WebSocketServerManager serverManager, ParkingSocketManager parkingSocketManager)
        {
            _next                 = next                 ?? throw new ArgumentNullException(nameof(next));
            _serverManager        = serverManager        ?? throw new ArgumentNullException(nameof(serverManager));
            _parkingSocketManager = parkingSocketManager ?? throw new ArgumentNullException(nameof(parkingSocketManager));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string path = context.Request.Path.Value;

                if (path == "/ws/raspberry")
                {
                    await _parkingSocketManager.AssignSocket(webSocket);
                }
                else
                {
                    string connectionId = _serverManager.AddClient(webSocket);

                    Console.WriteLine("Connection Established, Socket Created!");

                    await ReceiveMessage(webSocket, async (result, buffer) =>
                    {
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            Console.WriteLine("Received Close Message");

                            string id = _serverManager.GetAllClients().FirstOrDefault(s => s.Value == webSocket).Key;

                            if (_serverManager.GetAllClients().TryRemove(id, out WebSocket socket))
                            {
                                await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, cancellationToken: CancellationToken.None);
                            }

                            return;
                        }
                    });
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            while (socket.State == WebSocketState.Open)
            {
                var buffer = new byte[4024 * 4];

                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
