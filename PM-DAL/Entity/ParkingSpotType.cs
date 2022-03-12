using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_spot_type")]
    public class ParkingSpotType
    {
        [Column("id")]
        public Int16 Id { get; set; }

        [Column("type")]
        public string SpotType { get; set; }

        [Column("description")]
        public string? SpotDescription { get; set; }

        [Column("active")]
        public bool IsActive { get; set; } = true;
    }
}
