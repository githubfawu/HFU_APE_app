using FlightTracker.Attributes;
using FlightTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [SelectedItem]
    public partial class FlightPage : ContentPage
    {
        private readonly IListViewModel viewModel;
        public FlightPage()
        {
            InitializeComponent();
            this.viewModel = DependencyService.Get<FlightViewModel>();
            this.BindingContext = viewModel;
        }

        public string SelectedItem { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.viewModel?.LoadData();
            (this.viewModel as ISelectableViewModel)?.SetSelectedItem(SelectedItem);
        }

        protected override bool OnBackButtonPressed()
        {
            (this.viewModel as ISelectableViewModel)?.Clear();
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (this.viewModel as ISelectableViewModel)?.Clear();
        }
    }
}