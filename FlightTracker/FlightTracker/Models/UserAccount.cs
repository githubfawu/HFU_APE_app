﻿using SQLite;

namespace FlightTracker.Models
{
    public class UserAccount
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
