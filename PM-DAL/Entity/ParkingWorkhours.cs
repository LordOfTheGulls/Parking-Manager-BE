using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_hours")]
    public class ParkingWorkhours
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("day_of_week")]
        public short DayOfWeek { get; set; }

        [Column("open_time")]
        public TimeOnly OpenTime { get; set; }

        [Column("close_time")]
        public TimeOnly CloseTime { get; set; }

        [Column("parking_lot_id")]
        public Int64 ParkingLotId { get; set; }

        [Column("parking_hours_plan_id")]
        public Int64 ParkingWorkhoursPlanId { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        public virtual ParkingLot ParkingLot { get; set; }
        public virtual ParkingWorkhoursPlan ParkingWorkhoursPlan { get; set; }
    }
}
