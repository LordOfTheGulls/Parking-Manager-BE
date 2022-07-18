using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO
{
    public class ParkingMetadataEmitDto
    {
        [JsonProperty("lot_id")]
        public Int64 ParkingLotId { get; set; }

        [JsonProperty("is_open")]
        public bool? IsParkingOpen { get; set; }

        [JsonProperty("gps")]
        public ParkingLocationDto? ParkingLocation { get; set; }

        [JsonProperty("system_start_time")]
        public DateTime? StartTime { get; set; }
    }
}
