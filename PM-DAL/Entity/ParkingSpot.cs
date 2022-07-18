using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_spot")]
    public class ParkingSpot
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("spot_name")]
        public string? SpotName { get; set; }

        [Column("spot_number")]
        public int SpotNumber { get; set; }

        [Column("spot_covered")]
        public bool IsSpotCovered { get; set; } = false;

        [Column("spot_taken")]
        public bool IsTaken { get; set; } = false;

        [Column("spot_reserved")]
        public bool IsReserved { get; set; } = false;

        [Column("active")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<ParkingSpotType> ParkingSpotTypes { get; set; }

        public virtual ICollection<ParkingFloor> ParkingFloors { get; set; }
    }
}
