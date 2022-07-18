using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

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
    }
}
