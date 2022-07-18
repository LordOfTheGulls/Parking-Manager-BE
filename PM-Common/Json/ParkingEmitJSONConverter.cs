using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PM_Common.DTO;
using PM_Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.Json
{
    public class ParkingEmitJSONConverter : JsonConverter
    {
        public ParkingEmitJSONConverter() { }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) { }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            JObject json = JObject.Load(reader);

            return CreateObject(json);
        }

        private object? CreateObject(JToken token)
        {
            if (token.Type == JTokenType.Null) return null;

            JToken? payload = token.First;

            switch (payload?.Path)
            {
                case "parkingMetadata":
                {
                    ParkingMetadataEmitDto? parkingMetadata = new ParkingMetadataEmitDto();

                    JToken? parkingMetadataToken = token["parkingMetadata"];

                    if (parkingMetadataToken != null)
                        parkingMetadata = parkingMetadataToken.ToObject<ParkingMetadataEmitDto>();

                    return parkingMetadata;
                }
                case "parkingStatus":
                {
                    ParkingStatusEmitDto parkingStatus = new ParkingStatusEmitDto();

                    JToken? isParkingOpen    = token["parkingStatus"]?["is_open"];
                    JToken? slotsStatuses    = token["parkingStatus"]?["slots_echo"];
                    JToken? barriersStatuses = token["parkingStatus"]?["barriers_echo"];

                    parkingStatus.IsParkingOpen = isParkingOpen?.Value<bool?>();

                    if(slotsStatuses != null)
                    {
                        parkingStatus.SlotsStatuses = slotsStatuses.ToObject<Dictionary<int, ParkingSlotStatus>>();
                    }

                    if(barriersStatuses != null)
                    {
                       parkingStatus.BarriersStatuses = barriersStatuses.ToObject<ParkingBarrierStatus>();
                    }

                    return parkingStatus;
                }
                case "parkingEvent":
                {
                    ParkingEventEmitDto? parkingEvent = new ParkingEventEmitDto();

                    JToken? parkingEventToken = token["parkingEvent"];

                    if (parkingEventToken != null)
                        parkingEvent = parkingEventToken.ToObject<ParkingEventEmitDto>();

                    return parkingEvent;
                }
                default:
                {
                    return null;
                }
            }
        }
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
            //return objectType == typeof(ParkingStatusEchoDto);
        }
    }
}
