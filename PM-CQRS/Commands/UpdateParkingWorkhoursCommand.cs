using PM_Common.DTO.Parking.Workhours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class UpdateParkingWorkhoursCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public Int64 ParkingLotId { get; set; }
        public UpdateWorkhoursDTO Workhours { get; set; }
    }
}
