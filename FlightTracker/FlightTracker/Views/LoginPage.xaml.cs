using FlightTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            this.viewModel = DependencyService.Resolve<LoginViewModel>();
            this.BindingContext = this.viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsSessionValid();
        }
    }
}