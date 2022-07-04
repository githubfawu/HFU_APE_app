using Microsoft.EntityFrameworkCore;
using FlightTracker.DataAccess;
using FlightTracker.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace FlightTracker.ViewModels
{
    public class HomeViewModel : BaseViewModel, IListViewModel
    {
        private string loginCaption;
        private bool isAdminUser;
        private Session session;
        private bool isLoggedIn;
        private Flight selectedFlight;
        private string searchText;
        private readonly IParaglidingDbContext dbContext;

        public HomeViewModel(IParaglidingDbContext dbContext)
        {
            Title = "About";
            this.dbContext = dbContext;
            LoginCommand = new Command(async () => await ExecuteLogInLogOff());
            ManageUsersCommand = new Command(async () => await NavigateToUsersPage(), () => !IsBusy);
            LoadItemsCommand = new Command(() => LoadData());
            FlightSelectionChangedCommand = new Command(async () => await NavigateToUpdateFlight());
        }

        private async Task NavigateToUpdateFlight()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;
                await Shell.Current.GoToAsync($"FlightPage?SelectedItem={JsonConvert.SerializeObject(selectedFlight)}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task NavigateToUsersPage()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;
                await Shell.Current.GoToAsync("UsersPage");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteLogInLogOff()
        {
            await GoToLoginPage();
        }

        public ICommand LoginCommand { get; }
        public ICommand ManageUsersCommand { get; }

        public ICommand FlightSelectionChangedCommand { get; }

        public string LoginCaption { get => loginCaption; set => SetProperty(ref loginCaption, value); }
        public bool IsAdminUser { get => isAdminUser; set => SetProperty(ref isAdminUser, value); }

        public bool IsLoggedIn { get => isLoggedIn; set => SetProperty(ref isLoggedIn, value); }

        public string SearchText { 
            get => searchText; 
            set 
            { 
                SetProperty(ref searchText, value);
                SearchRecords();
            } 
        }

        public ObservableCollection<Flight> Flights { get; private set; } = new ObservableCollection<Flight>();
        public Flight SelectedFlight
        {
            get => selectedFlight;
            set
            {
                SetProperty(ref selectedFlight, value);
            }
        }
        public ICommand LoadItemsCommand { get; set; }

        private async void SearchRecords()
        {
            if (searchText != null && searchText.Length > 2)
            {
                try
                {
                    if(IsBusy)
                    {
                        return;
                    }

                    IsBusy = true;
                    var records = await dbContext.Flights.Include(f => f.Owner).Where(f =>
                    (f.Owner.FirstName != null && EF.Functions.Like(f.Owner.FirstName, $"%{searchText}%")) ||
                    (f.Owner.LastName != null && EF.Functions.Like(f.Owner.LastName, $"%{searchText}%"))).ToListAsync();

                    Flights.Clear();
                    foreach (var record in records.ToModels())
                    {
                        Flights.Add(record);
                    }
                }
                finally
                {
                    IsBusy = false;
                }
            }

            if(string.IsNullOrWhiteSpace(searchText))
            {
                LoadData();
            }
        }

        public async void LoadData()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                this.session = GetSession();
                LoginCaption = session != null && session.IsValid() ? $"Abmelden" : "Anmelden";
                IsLoggedIn = session?.IsValid() == true;
                IsAdminUser = IsLoggedIn ? session.UserRole == DataAccess.Role.Administrator : false;

                IsBusy = true;
                if (session == null || !session.IsValid())
                {
                    return;
                }

                var flights = new List<Flight>();
                if (IsAdminUser)
                {
                    flights = (await this.dbContext.Flights.Include(f => f.Owner).Select(f => f).ToListAsync()).ToModels();
                }
                else
                {
                    flights = (await this.dbContext.Flights.Include(f => f.Owner).Where(f => f.Owner.Username == session.Username).ToListAsync()).ToModels();
                }

                Flights.Clear();
                foreach (var record in flights)
                {
                    Flights.Add(record);
                }
                OnPropertyChanged(nameof(Flight));
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}