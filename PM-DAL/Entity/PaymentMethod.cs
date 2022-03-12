using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("payment_method")]
    public class PaymentMethod
    {
        [Column("id")]
        public Int16 Id { get; set; }

        [Column("method_name")]
        public string MethodName { get; set; }

        [Column("method_description")]
        public string? MethodDescription { get; set; }

        [Column("active")]
        public bool IsActive { get; set; } = true;
    }
}