using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_hours_plan")]
    public class ParkingWorkhoursPlan
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set; }
    }
}
