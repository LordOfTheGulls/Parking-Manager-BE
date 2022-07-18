using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("payment")]
    public class ParkingPayment
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("amount")]
        public double Amount { get; set; }

        [Column("payment_method_id")]
        public Int16 ParkingPaymentMethodId { get; set; }

        [Column("parking_inout_log_id")]
        public Int64 ParkingInOutLogId { get; set; }

        [Column("parking_lot_id")]
        public Int64 ParkingLotId { get; set; }

        [Column("date_created")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [Column("date_modified")]
        public DateTime? DateModified { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        public virtual ParkingPaymentMethod ParkingPaymentMethod { get; set; }
        public virtual ParkingInOutLog ParkingInOutLog { get; set; }
        public virtual ParkingLot ParkingLot { get; set; }
    }
}
