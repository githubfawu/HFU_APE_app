using System;
using System.Collections.Generic;
using System.Text;

namespace FlightTracker.DataAccess.Queries.Response
{
    public class IdentityUser
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public Role Role { get; set; }
    }
}
