using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace PM_API.Middlewares
{
    public class WebSocketServerManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new();

        public ConcurrentDictionary<string, WebSocket> GetAllClients()
        {
            return _sockets;
        }

        public string AddClient(WebSocket socket)
        {
            string ConnectionID = Guid.NewGuid().ToString();

            _sockets.TryAdd(ConnectionID, socket);

            return ConnectionID;
        }

        private async Task SendToClientAsync(string connectionId, string data, CancellationToken cancelationToken = default)
        {
            if(_sockets.TryGetValue(connectionId, out WebSocket socket))
            {
                await socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Binary, true, cancelationToken);
            }
        }

        public async Task SendToAllClientsAsync(string data, CancellationToken cancelationToken = default)
        {
            foreach(WebSocket socket in _sockets.Values)
            {
                //await socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Binary, true, cancelationToken);
                await socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Binary, true, cancelationToken);
            }
        }
    }
}
