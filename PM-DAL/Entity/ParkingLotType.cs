using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_lot_type")]
    public class ParkingLotType
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("type")]
        public string LotType { get; set; }

        [Column("description")]
        public string LotDescription{ get; set; }

        [Column("active")]
        public bool IsActive { get; set; } = true;
    }
}
