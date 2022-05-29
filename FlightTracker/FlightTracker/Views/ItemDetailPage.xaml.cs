using FlightTracker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FlightTracker.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}