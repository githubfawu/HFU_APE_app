using System.Threading.Tasks;

namespace FlightTracker.DataAccess.Queries
{
    public interface IDbQuery<TIn, TOut>
    {
        Task<TOut> ExecuteAsync(TIn input);
    }
}
