using PM_Common.DTO.Parking.Workhours;
using PM_DAL.Entity;
using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interface
{
    public interface IParkingWorkhoursRepository: IRepositoryBase<ParkingWorkhours>, IRepository
    {
        public Task<List<WorkhoursDTO>> GetWorkhours(Int64 parkingLotId, Int64 parkingWorkhoursPlanId, CancellationToken cancellationToken);
        public Task UpdateParkingHours(Int64 parkingLotId, UpdateWorkhoursDTO updateWorkhours, CancellationToken cancellationToken);
    }
}
