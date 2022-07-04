using Microsoft.EntityFrameworkCore;
using FlightTracker.DataAccess.Helpers;
using FlightTracker.DataAccess.Queries.Response;
using System.Threading.Tasks;

namespace FlightTracker.DataAccess.Queries
{
    public class IdentityQuery : IDbQuery<(string, string), IdentityUser>
    {
        private readonly IParaglidingDbContext dbContext;

        public IdentityQuery(IParaglidingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IdentityUser> ExecuteAsync((string, string) input)
        {
            if (string.IsNullOrWhiteSpace(input.Item1) || string.IsNullOrWhiteSpace(input.Item2))
            {
                return null;
            }

            var hash = Obfuscator.Encrypt(input.Item2);
            var user = await this.dbContext.Pilots.SingleOrDefaultAsync(p => p.Username == input.Item1 && p.Password == hash);
            return (user != null? new IdentityUser { DisplayName = user.FirstName, Role = user.Role, UserName = user.Username}: null);
        }
    }
}
