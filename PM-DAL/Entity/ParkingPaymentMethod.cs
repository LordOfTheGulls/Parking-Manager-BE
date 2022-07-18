using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("payment_method")]
    public class ParkingPaymentMethod
    {
        [Key]
        [Column("id")]
        public Int16 Id { get; set; }

        [Column("method_name")]
        public string PaymentMethodName { get; set; }

        [Column("method_description")]
        public string? PaymentMethodDescription { get; set; }

        [Column("active")]
        public bool IsActive { get; set; } = true;

        public virtual ParkingLot ParkingLot { get; set; }
    }
}
