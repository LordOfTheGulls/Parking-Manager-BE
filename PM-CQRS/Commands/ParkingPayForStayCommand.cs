using PM_Common.DTO.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class ParkingPayForStayCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public Int64 ParkingLotId { get; set; }
        public string LicensePlate { get; set; }
        public CreditCardDto CreditCardInfo { get; set; }
    }
}
