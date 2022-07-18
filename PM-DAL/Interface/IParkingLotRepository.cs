using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interfaces
{
    public interface IParkingLotRepository : IRepositoryBase<ParkingLot>, IRepository
    {
        public Task UpdateLocation(Int64 lotId, decimal latitude, decimal longitude, CancellationToken cancellationToken = default);
    }
}
