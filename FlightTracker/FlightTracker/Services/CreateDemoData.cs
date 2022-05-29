using System.Threading.Tasks;
using FlightTracker.Models;

namespace FlightTracker.Services
{
    internal class CreateDemoData
    {
        private void CreateUsers()
        {
            App.Database.SaveUserAccountAsync(new UserAccount
            {
                Username = "admin",
                Password = "adminpw",
                Role = "Administrator"
            });
        }
    }
}
