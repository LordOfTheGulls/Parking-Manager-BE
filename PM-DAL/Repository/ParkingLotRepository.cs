using Microsoft.EntityFrameworkCore;
using PM_Common.DTO;
using PM_Common.DTO.Parking;
using PM_Common.Exceptions;
using PM_DAL.Entity;
using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Repository
{
    public class ParkingLotRepository : RepositoryBase<ParkingLot>, IParkingLotRepository
    {
        private readonly PMDBContext context;

        public ParkingLotRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ParkingLotDto> GetParkingLotInfo(Int64 lotId, CancellationToken cancellationToken = default)
        {
            ParkingLot? lot = await context.ParkingLot.FirstOrDefaultAsync(lot => (lot.Id == lotId), cancellationToken);

            if (lot == null)
                throw new EntityDoesNotExistException(lotId, typeof(ParkingLot));

            return new ParkingLotDto
            {
                ParkingName = lot.Name,
                ParkingLocation = new ParkingLocationDto
                {
                    Lattitude = lot.Latitude,
                    Longitude = lot.Longitude,
                }
            };
        }

        public async Task UpdateLocation(Int64 lotId, decimal latitude, decimal longitude, CancellationToken cancellationToken = default)
        {
            ParkingLot? lot = await context.ParkingLot.FirstOrDefaultAsync(lot => (lot.Id == lotId), cancellationToken);

            if (lot == null)
                throw new EntityDoesNotExistException(lotId, typeof(ParkingLot));

            lot.Latitude  = latitude;
            lot.Longitude = longitude;
        }
    }
}
