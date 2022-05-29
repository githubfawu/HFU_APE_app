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

            SaveUser();//To be moved
        }

        private async Task SaveUser()
        {
            await App.Database.SaveUserAccountAsync(new UserAccount
            {
                Username = "admin",
                Password = "adminpw"
            });
        }
    }
}