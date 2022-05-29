using System.Collections.Generic;
using System.Threading.Tasks;
using FlightTracker.Models;
using SQLite;

namespace FlightTracker.Data
{
    public class UserDatabase
    {

        readonly SQLiteAsyncConnection database;

        public UserDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UserAccount>().Wait();
        }

        public Task<List<UserAccount>> GetUserAccountsAsync()
        {
            //Get all users.
            return database.Table<UserAccount>().ToListAsync();
        }

        public Task<UserAccount> GetUserAccountAsync(string username)
        {
            // Get a specific user.
            return database.Table<UserAccount>()
                .Where(i => i.Username == username)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAccountAsync(UserAccount username)
        {
            return database.InsertAsync(username);
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
            return database.DeleteAsync(username);
        }
    }
}
