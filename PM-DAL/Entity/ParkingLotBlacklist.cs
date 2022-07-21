using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_lot_blacklist")]
    public class ParkingLotBlacklist
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("parking_lot_id")]
        public Int64 ParkingLotId { get; set; }

        [Column("license_plate")]
        public string? LicensePlate { get; set; } = "";

        [Column("active")]
        public bool IsActive { get; set; } = true;

        public virtual ParkingLot ParkingLot { get; set; }
    }
}
