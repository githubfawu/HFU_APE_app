using System.Threading.Tasks;
using FlightTracker.ViewModels;
using FlightTracker.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            
            var vm = new LoginViewModel();
            this.BindingContext = vm;

            SaveUser();
            GetUser("admin");
            
        }

        private async Task SaveUser()
        {
            await App.Database.SaveUserAccountAsync(new UserAccount
            {
                Username = "admin",
                Password = "adminpw"
            });
        }

        private async Task GetUser(string username)
        {
            var result = await App.Database.GetUserAccountAsync(username);
            DisplayAlert("UserAccount", result.Password, "OK");
        }
    }
}