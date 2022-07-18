using PM_Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class UpdateOnParkingEmitCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public ParkingMetadataEmitDto? ParkingLocation { get; set; }
        public ParkingEventEmitDto? ParkingEvent { get; set; }
    }
}
