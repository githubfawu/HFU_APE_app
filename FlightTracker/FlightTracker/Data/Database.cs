using System.Collections.Generic;
using System.Threading.Tasks;
using FlightTracker.Models;
using SQLite;

namespace FlightTracker.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection connection;

        public Database(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<UserAccount>().Wait();
        }

        public Task<List<UserAccount>> GetUserAccountsAsync()
        {
            //Get all users.
            return connection.Table<UserAccount>().ToListAsync();
        }

        public Task<UserAccount> GetUserAccountAsync(string username)
        {
            // Get a specific user.
            return connection.Table<UserAccount>()
                .Where(i => i.Username == username)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAccountAsync(UserAccount username)
        {
            return connection.InsertAsync(username);
            //if (username.Username != "")
            //{
            //    // Update an existing user.
            //    return database.UpdateAsync(username);
            //}
            //else
            //{
            //    // Save a new user.
            //    return database.InsertAsync(username);
            //}
        }

        public Task<int> DeleteUserAccountAsync(UserAccount username)
        {
            // Delete a user.
            return connection.DeleteAsync(username);
        }

        //public Task<List<Flight>> GetFlightsAsync()
        //{
        //    //Get all users.
        //    return database.Table<Flight>().ToListAsync();
        //}

        //public Task<Flight> GetFlightAsync(string flightname)
        //{
        //    // Get a specific user.
        //    return database.Table<Flight>()
        //        .Where(i => i.Flightname == flightname)
        //        .FirstOrDefaultAsync();
        //}

        //public Task<int> SaveFlightAsync(Flight flight)
        //{
        //    return connection.InsertAsync(flight);
        //    //if (username.Username != "")
        //    //{
        //    //    // Update an existing user.
        //    //    return database.UpdateAsync(username);
        //    //}
        //    //else
        //    //{
        //    //    // Save a new user.
        //    //    return database.InsertAsync(username);
        //    //}
        //}

        //public Task<int> DeleteFlightAsync(Flight flight)
        //{
        //    // Delete a user.
        //    return connection.DeleteAsync(flight);
        //}
    }
}
