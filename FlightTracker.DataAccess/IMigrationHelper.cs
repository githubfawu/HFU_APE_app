using System.Threading.Tasks;

namespace FlightTracker.DataAccess
{
    public interface IMigrationHelper
    {
        Task MigrateAsync();
    }
}
