using FlightTracker.Attributes;
using FlightTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [SelectedItem]
    public partial class UserPage : ContentPage, ISelectableContentPage
    {
        private readonly ISelectableViewModel viewModel;
        public UserPage()
        {
            InitializeComponent();
            viewModel = DependencyService.Get<UserViewModel>();
            BindingContext = viewModel;       
        }

        public string SelectedItem { get; set; }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.SetSelectedItem(SelectedItem);
        }        
    }
}