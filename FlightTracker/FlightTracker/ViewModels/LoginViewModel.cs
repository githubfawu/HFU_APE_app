using System.Threading.Tasks;
using FlightTracker.Models;
using FlightTracker.Views;

using Xamarin.Forms;

namespace FlightTracker.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        private string Username { get; set; }
        private string Password { get; set; }


        public LoginViewModel()
        {
            Title = "Anmeldung";
            
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            if (Username == Username)
            {
                var user = await App.Database.GetUserAccountAsync(Username);
                if (user != null)
                {
                    if (user.Password == Password)
                    {
                        //Meldung dass Login erfolgreich
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        //Passwort falsch
                    }
                }
                else
                {
                    //User nicht gefunden
                }
            }
            else
            {
                //User darf nicht leer sein oder ist falsch
            }
        }
    }
}
