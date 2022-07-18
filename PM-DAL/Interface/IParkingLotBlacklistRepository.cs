using PM_DAL.Entity;
using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interface
{
    public interface IParkingLotBlacklistRepository : IRepositoryBase<ParkingLotBlacklist>
    {
        Task<bool> IsLicensePlateBlacklisted(string licensePlate, CancellationToken cancellationToken = default);
    }
}
