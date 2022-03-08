using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PM_DAL.Interfaces;
using System.Data;
using System.Data.Common;

namespace PM_DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbConnection _connection;

        private readonly PMDBContext _context;

        //private IDbContextTransaction _transaction;

        //public IParkingLotRepository ParkingLotRepository { get; private set; }

        public UnitOfWork(PMDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            //ParkingLotRepository = new ParkingLotRepository(context);
        }

        public async Task InitializeAsync()
        {
           /* _connection = _context.Database.GetDbConnection();

            if(_connection.State != ConnectionState.Open)
            {
                await _context.Database.OpenConnectionAsync();
            }*/

            //_transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

           // await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
            _context.Dispose();
        }
    }
}
