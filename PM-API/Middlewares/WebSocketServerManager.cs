using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace PM_API.Middlewares
{
    public class WebSocketServerManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new();

        public ConcurrentDictionary<string, WebSocket> GetAllSockets()
        {
            return _sockets;
        }

        public string AddSocket(WebSocket socket)
        {
            string ConnectionID = Guid.NewGuid().ToString();

            _sockets.TryAdd(ConnectionID, socket);

            Console.WriteLine("Connection Added!");

            return ConnectionID;
        }
    }
}
