using System;
using SQLite;

namespace FlightTracker.Models
{
    internal class UserAccounts
    {
        [PrimaryKey, AutoIncrement]
        public int Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
