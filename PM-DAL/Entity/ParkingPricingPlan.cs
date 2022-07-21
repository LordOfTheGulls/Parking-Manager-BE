using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_pricing_plan")]
    public class ParkingPricingPlan
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set; }
    }
}
