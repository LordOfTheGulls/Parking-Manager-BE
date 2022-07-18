using PM_Common.Attributes;
using PM_Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_event_log")]
    public class ParkingEventLog
    {
        [Key]
        [Column("id")]
        public Int16 Id { get; set; }

        [Column("event_id")]
        public Int16 EventId { get; set; }

        [Column("event_payload")]
        public string? EventPayload { get; set; }

        [Column("parking_lot_id")]
        public Int64 ParkingLotId { get; set; }

        [Column("event_date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime EventDate { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        public virtual ParkingEvent Event { get; set; }
        public virtual ParkingLot ParkingLot { get; set; }
    }
}
