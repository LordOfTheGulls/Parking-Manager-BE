using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Payment
{
    public class ParkingLotPaymentDto
    {
        public Int64 PaymentId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string LicensePlate { get; set; }
        public DateTime Date { get; set; }
    }
}
