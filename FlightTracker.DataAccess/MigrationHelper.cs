using System.Threading.Tasks;

namespace FlightTracker.DataAccess
{
    public class MigrationHelper : IMigrationHelper
    {
        private readonly IParaglidingDbContext _dbContext;
        public MigrationHelper(IParaglidingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.RunMigrationsAsync();
        }
    }
}
