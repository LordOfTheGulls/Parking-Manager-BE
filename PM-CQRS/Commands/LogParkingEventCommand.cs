using PM_Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class LogParkingEventCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();

        public ParkingEventEmitDto ParkingEvent { get; set; }
    }
}
