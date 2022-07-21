using PM_Common.DTO.Parking.Blacklist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class UpdateParkingBlacklist : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public Int64 ParkingLotId { get; set; }
        public ParkingBlacklistDto ParkingBlacklist { get; set; }
    }
}
