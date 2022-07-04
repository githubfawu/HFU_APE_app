using Microsoft.EntityFrameworkCore;
using FlightTracker.DataAccess.Entities;
using System.Threading.Tasks;

namespace FlightTracker.DataAccess
{
    public interface IParaglidingDbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        
        Task RunMigrationsAsync();
        Task SaveAsync();
    }
}
