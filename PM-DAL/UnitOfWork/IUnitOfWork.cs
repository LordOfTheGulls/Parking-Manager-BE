using PM_DAL.Interfaces;

namespace PM_DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IParkingLotRepository ParkingLotRepository { get; }
    }
}
