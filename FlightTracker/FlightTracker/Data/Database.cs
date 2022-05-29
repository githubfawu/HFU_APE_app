using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlightTracker.Models;
using SQLite;

namespace FlightTracker.Data
{
    internal class Database
    {
        public class UserDatabase
        {
            readonly SQLiteAsyncConnection database;

            public UserDatabase(string dbPath)
            {
                database = new SQLiteAsyncConnection(dbPath);
                database.CreateTableAsync<UserAccounts>().Wait();
            }

            public Task<List<UserAccounts>> GetNotesAsync()
            {
                //Get all notes.
                return database.Table<UserAccounts>().ToListAsync();
            }

            public Task<UserAccounts> GetNoteAsync(int id)
            {
                // Get a specific note.
                return database.Table<UserAccounts>()
                    .Where(i => i.Username == id)
                    .FirstOrDefaultAsync();
            }

            public Task<int> SaveNoteAsync(UserAccounts account)
            {
                if (account.Username != 0)
                {
                    // Update an existing note.
                    return database.UpdateAsync(account);
                }
                else
                {
                    // Save a new note.
                    return database.InsertAsync(account);
                }
            }

            public Task<int> DeleteNoteAsync(UserAccounts account)
            {
                // Delete a note.
                return database.DeleteAsync(account);
            }
        }
    }
}
