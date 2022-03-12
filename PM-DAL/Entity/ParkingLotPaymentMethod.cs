using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_lot_payment_method")]
    public class ParkingLotPaymentMethod
    {
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("active")]
        public bool IsActive { get; set; } = true;

        public virtual ParkingLot ParkingLot { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
