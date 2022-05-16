using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sandbox.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ICommand OpenWebCommand { get; }
        public ICommand OpenAnotherPage { get; }
        public ICommand ShowAlertCommand { get; set; }
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            OpenAnotherPage = new Command(async (parameter) => await Browser.OpenAsync(parameter.ToString()));
            ShowAlertCommand = new Command(get => MakeAlter());
        }

        void MakeAlter()
        {
            Application.Current.MainPage.DisplayAlert("Alert", "Hello", "Cancel", "ok");
        }
    }
}