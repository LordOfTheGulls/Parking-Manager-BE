using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("Parking_Floor")]
    public class ParkingFloor
    {
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("floor_name")]
        public string? FloorName { get; set; }

        [Column("floor_number")]
        public Int16 Floor_Number { get; set; } = 1;

        [Column("floor_covered")]
        public bool IsFloorCovered { get; set; } = false;

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [Column("date_modified")]
        public DateTime DateModified { get; set; }

        [Column("active")]
        public bool IsActive { get; set; } = true;


        public virtual ICollection<ParkingLot> ParkingLots { get; set; }
    }
}
