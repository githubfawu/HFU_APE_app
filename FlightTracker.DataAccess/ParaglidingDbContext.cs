using Microsoft.EntityFrameworkCore;
using FlightTracker.DataAccess.Entities;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FlightTracker.DataAccess
{
    public class ParaglidingDbContext : DbContext, IParaglidingDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var conString = GetConnectionString();

            optionsBuilder.UseSqlite(conString);
        }

        private string GetConnectionString()
        {
            var dbName = "paragliding.db";
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
            var conString = @$"Data Source=""{dbPath}""";

            return conString;
        }

        public async Task RunMigrationsAsync()
        {
            try
            {
                await this.Database.MigrateAsync();

            }catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task SaveAsync()
        {
            await this.SaveChangesAsync();
        }
        
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Owner)
                .WithMany(p => p.Flights);                
        }
    }
}