using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FlightTracker.ViewModels;
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

            get();
        }

        public async Task get()
        {
            var user = await App.Database.GetUserAccountAsync("admin");
            await DisplayAlert("Info", user.Password, "OK");
        }
    }
}