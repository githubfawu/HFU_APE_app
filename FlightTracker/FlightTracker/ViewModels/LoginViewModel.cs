using System.Threading.Tasks;
using FlightTracker.Models;
using FlightTracker.Views;

using Xamarin.Forms;

namespace FlightTracker.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public string Username { get; set; }
        public string Password { get; set; }


        public LoginViewModel()
        {
            Title = "Anmeldung";
            
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            if (Username != "")
            {
                var user = await App.Database.GetUserAccountAsync(Username);
                if (user != null)
                {
                    if (user.Password == Password)
                    {
                        App.Current.MainPage = new AboutPage();
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
                //User darf nicht leer sein
            }
        }
    }
}
