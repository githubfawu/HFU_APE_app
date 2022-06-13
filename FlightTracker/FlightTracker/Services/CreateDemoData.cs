using FlightTracker.Models;

namespace FlightTracker.Services
{
    public class CreateDemoData
    { 
        public void CreateUsers()
        {
            App.Database.SaveUserAccountAsync(new UserAccount
            {
                Username = "admin",
                Password = "adminpw",
                Role = "Administrator"
            });

            App.Database.SaveUserAccountAsync(new UserAccount
            {
                Username = "user",
                Password = "user",
                Role = "User"
            });
        }

        //public void CreateFlights()
        //{
        //    App.Database.SaveFlightAsync(new Flight()
        //    {
        //        Owner = "Fabrice",
        //        Flightname = "Erster Flug im Mai",
        //        Comments = "Bei Brigels versenkt...wie immer",
        //        FlightLength = 138
        //    });

        //    App.Database.SaveFlightAsync(new Flight()
        //    {
        //        Owner = "Urs",
        //        Flightname = "Letzter Flug im August",
        //        Comments = "Herrliches Sommerwetter",
        //        FlightLength = 185
        //    });

        //}
    }
}

