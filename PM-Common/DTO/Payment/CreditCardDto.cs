using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Payment
{
    public class CreditCardDto
    {
        public string CreditCardHolderName { get; set; }
        public string CreditCardNumber { get; set; }
        public Int16 CVC { get; set; }
    }
}
