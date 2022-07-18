using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Payment
{
    public class ParkingPaymentDto
    {
        public string LicensePlate { get; set; }
        public CreditCardDto CreditCardInfo { get; set; }
    }
}
