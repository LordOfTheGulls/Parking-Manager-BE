using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PM_Common.DTO.Parking.Blacklist;
using PM_Common.Exceptions;

namespace PM_DAL.Repository
{
    public class ParkingLotBlacklistRepository : RepositoryBase<ParkingLotBlacklist>, IParkingLotBlacklistRepository
    {
        private readonly PMDBContext context;

        public ParkingLotBlacklistRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> IsLicensePlateBlacklisted(string licensePlate, CancellationToken cancellationToken = default)
        {
            return await context.ParkingLotBlacklist.AnyAsync(x => x.LicensePlate.Equals(licensePlate), cancellationToken);
        }

        public async Task<List<ParkingBlacklistDto>> GetParkingBlacklist(Int64 parkingLotId, CancellationToken cancellationToken)
        {
            var query = context.ParkingLotBlacklist.Where(p => p.ParkingLotId == parkingLotId)
                        .Select(p => new ParkingBlacklistDto()
                        {
                            BlacklistId = p.Id,
                            LicensePlate = p.LicensePlate ?? "",
                        }).OrderBy(p => p.BlacklistId);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task UpdateParkingBlacklist(Int64 blacklistId, ParkingBlacklistDto blackList, CancellationToken cancellationToken)
        {
            ParkingLotBlacklist parkingBlackList = await context.ParkingLotBlacklist.FirstOrDefaultAsync(p => p.Id == blacklistId, cancellationToken);

            if(parkingBlackList == null)
            {
                throw new EntityDoesNotExistException(blacklistId, typeof(ParkingLotBlacklist));
            }

            parkingBlackList.LicensePlate = blackList.LicensePlate;
        }

        public async Task DeleteParkingBlacklist(Int64 blacklistId, CancellationToken cancellationToken)
        {
            ParkingLotBlacklist parkingBlackList = await context.ParkingLotBlacklist.FirstOrDefaultAsync(p => p.Id == blacklistId, cancellationToken);

            if (parkingBlackList == null)
            {
                throw new EntityDoesNotExistException(blacklistId, typeof(ParkingLotBlacklist));
            }

            context.Remove(parkingBlackList);
        }

        public async Task AddToParkingBlacklist(Int64 parkingLotId, CancellationToken cancellationToken)
        {
            await context.AddAsync(new ParkingLotBlacklist()
            {
                ParkingLotId = parkingLotId,
                LicensePlate = "N/A"
            }, cancellationToken);
        }
    }
}
