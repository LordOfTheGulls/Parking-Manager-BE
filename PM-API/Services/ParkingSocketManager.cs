using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PM_API.Middlewares;
using PM_Common.DTO;
using PM_Common.Enums;
using PM_Common.Json;
using PM_CQRS.Commands;
using PM_CQRS.Dispatcher;
using PM_DAL.Interface;
using PM_DAL.Interfaces;
using PM_DAL.Repository;
using PM_DAL.UOW;
using System.Net.WebSockets;
using System.Text;

namespace PM_API.Services
{
    public interface IParkingSocketManager
    {
        public Task AssignSocket(WebSocket socket);
    }

    public class ParkingSocketManager : IParkingSocketManager
    {
        public WebSocket Socket { private set; get; }

        public byte[] MessageBuffer = new byte[1024];

        private WebSocketServerManager WebsocketServerManager;

        public ParkingSocketManager(WebSocketServerManager webSocketServerManager)
        {
            WebsocketServerManager = webSocketServerManager ?? throw new ArgumentNullException(nameof(webSocketServerManager));
        }

        public async Task AssignSocket(WebSocket _socket)
        {
            Socket  = _socket;
        
            await ReceiveMessage(Socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string receivedMessage = Encoding.UTF8.GetString(buffer).TrimEnd('\0');

                    var receivedData = JsonConvert.DeserializeObject<object>(receivedMessage, new ParkingEmitJSONConverter());

                    Console.WriteLine(receivedMessage);

                    SocketPayload broadcastedPayload = new SocketPayload(){
                        EmitType = SocketEmitType.Parking
                    };

                    Type? receivedType = receivedData.GetType();

                    if (receivedType == typeof(ParkingMetadataEmitDto))
                    {
                        broadcastedPayload.PayloadType = "metadata";
                        broadcastedPayload.Payload     = receivedData;
                    }
                    else if(receivedType == typeof(ParkingStatusEmitDto))
                    {
                        broadcastedPayload.PayloadType = "status";
                        broadcastedPayload.Payload = receivedData;
                    }
                    else if (receivedType == typeof(ParkingEventEmitDto))
                    {
                        broadcastedPayload.PayloadType = "event";
                        broadcastedPayload.Payload = receivedData;
                    }

                    await WebsocketServerManager.SendToAllClientsAsync(JsonConvert.SerializeObject(broadcastedPayload, new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    }));

                    return;
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    Console.WriteLine("Received Close Message For RaspBerry");
                    await Socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, cancellationToken: CancellationToken.None);
                    return;
                }
            });
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            while (socket.State == WebSocketState.Open)
            {
                Array.Clear(MessageBuffer, 0, MessageBuffer.Length);

                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(MessageBuffer), cancellationToken: CancellationToken.None);

                handleMessage(result, MessageBuffer);
            }
        }
    }
}


