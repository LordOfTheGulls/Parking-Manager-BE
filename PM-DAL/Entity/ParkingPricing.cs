using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PM_DAL.Entity
{
    [Table("parking_pricing")]
    public class ParkingPricing
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("parking_lot_id")]
        public Int64 ParkingLotId { get; set; }

        [Column("pricing_plan_id")]
        public Int64 PricingPlanId { get; set; } = 1;

        [Column("day_of_week")]
        public Int16 DayOfWeek { get; set; }

        [Column("interval_start")]
        public double? IntervalStart { get; set; } = 0;

        [Column("interval_end")]
        public double? IntervalEnd { get; set; } = 0;

        [Column("rate")]
        public double? Rate { get; set; } = 0;

        [Column("incremental")]
        public double? Incremental { get; set; } = 0;

        [Column("incremental_rate")]
        public double? IncrementalRate { get; set; } = 0;

        [Column("active")]
        public bool Active { get; set; } = true;

        public virtual ParkingPricingPlan PricingPlan { get; set; }
        public virtual ParkingLot ParkingLot { get; set; }
    }
}
