using Sandbox.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Sandbox.Views
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