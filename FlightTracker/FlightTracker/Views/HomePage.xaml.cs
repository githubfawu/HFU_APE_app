using FlightTracker.ViewModels;
using Xamarin.Forms;

namespace FlightTracker.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = DependencyService.Resolve<HomeViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as IListViewModel)?.LoadData();
        }
    }
}