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
    [Table("parking_event")]
    public class ParkingEvent
    {
        [Key]
        [Column("id")]
        public Int16 Id { get; set; } 

        [Column("event")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;
    }
}
