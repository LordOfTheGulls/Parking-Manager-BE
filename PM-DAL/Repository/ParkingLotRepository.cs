using Microsoft.EntityFrameworkCore;
using PM_DAL.Entities;
using PM_DAL.Interfaces;

namespace PM_DAL.Repositories
{
    public class ParkingLotRepository : RepositoryBase<ParkingLot>, IParkingLotRepository
    {
        public ParkingLotRepository(PMDBContext dbContext) : base(dbContext)
        {

        }
    }
}