using AppEntwicklungFabriceTest.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppEntwicklungFabriceTest.Views
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