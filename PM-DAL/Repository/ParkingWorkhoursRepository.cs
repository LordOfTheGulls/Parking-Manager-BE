using Microsoft.EntityFrameworkCore;
using PM_Common.DTO.Parking.Workhours;
using PM_Common.Exceptions;
using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Repository
{
    public class ParkingWorkhoursRepository : RepositoryBase<ParkingWorkhours>, IParkingWorkhoursRepository
    {
        private readonly PMDBContext context;

        public ParkingWorkhoursRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task UpdateParkingHours(Int64 parkingLotId, UpdateWorkhoursDTO updateWorkhours, CancellationToken cancellationToken)
        {
            var workhours = await context.ParkingWorkhours.FirstOrDefaultAsync(
                v => v.ParkingLotId == parkingLotId && v.Id == updateWorkhours.WorkhourId, cancellationToken);

            if(workhours == null)
            {
                throw new EntityDoesNotExistException(parkingLotId, typeof(ParkingWorkhours));
            }

            workhours.OpenTime  = TimeOnly.Parse(updateWorkhours.OpenTime);
            workhours.CloseTime = TimeOnly.Parse(updateWorkhours.CloseTime);
        }

        public async Task<List<WorkhoursDTO>> GetWorkhours(Int64 parkingLotId, Int64 parkingWorkhoursPlanId, CancellationToken cancellationToken)
        {
             var workhours = await context.ParkingWorkhours
                            .Where(v => v.ParkingLotId == parkingLotId && v.ParkingWorkhoursPlanId == parkingWorkhoursPlanId)
                            .Select(v => new WorkhoursDTO()
                            {
                                WorkhourId = v.Id,
                                WeekDay    = v.DayOfWeek,
                                OpenTime   = v.OpenTime.ToString(),
                                CloseTime  = v.CloseTime.ToString()
                            }).OrderBy(w => w.WeekDay)
                            .ToListAsync(cancellationToken);
            return workhours;
        }
    }
}
