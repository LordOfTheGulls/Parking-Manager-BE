using PM_Common.DTO.Parking.Blacklist;
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
        Task<List<ParkingBlacklistDto>> GetParkingBlacklist(Int64 parkingLotId, CancellationToken cancellationToken);
        Task<bool> IsLicensePlateBlacklisted(string licensePlate, CancellationToken cancellationToken = default);
        Task AddToParkingBlacklist(Int64 parkingLotId, CancellationToken cancellationToken);
        Task UpdateParkingBlacklist(Int64 blacklistId, ParkingBlacklistDto blackList, CancellationToken cancellationToken);
        Task DeleteParkingBlacklist(Int64 blacklistId, CancellationToken cancellationToken);
    }
}
