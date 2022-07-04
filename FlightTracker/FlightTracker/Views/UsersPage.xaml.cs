using FlightTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        private readonly IListViewModel listViewModel;
        public UsersPage()
        {
            InitializeComponent();
            this.listViewModel = DependencyService.Get<UsersViewModel>();
            BindingContext = listViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.listViewModel.LoadData();
        }
    }
}