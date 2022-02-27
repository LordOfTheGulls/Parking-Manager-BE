using Microsoft.EntityFrameworkCore;
using PM_DAL.Entities;

namespace PM_DAL
{
    public class PMDBContext : DbContext
    {
        public PMDBContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<ParkingLot> ParkingLot { get; set; }
    }
}