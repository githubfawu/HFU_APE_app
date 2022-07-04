using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightTracker.DataAccess;
using FlightTracker.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace FlightTracker.ViewModels
{
    public class UsersViewModel : BaseViewModel, IListViewModel
    {
        private readonly IParaglidingDbContext dbContext;
        private Pilot selectedUser;
        private string searchText;

        public ObservableCollection<Pilot> Users { get; private set; } = new ObservableCollection<Pilot>();
        public Pilot SelectedUser { 
            get => selectedUser; 
            set
            {
                SetProperty(ref selectedUser, value);
            }
        }
        public ICommand LoadItemsCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand SelectedUserChangedCommand { get; }

        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                SearchRecords();
            }
        }

        private async void SearchRecords()
        {
            if (searchText != null && searchText.Length > 2)
            {
                try
                {
                    if (IsBusy)
                    {
                        return;
                    }

                    IsBusy = true;
                    var records = await dbContext.Pilots.Where(p =>
                    (p.FirstName != null && EF.Functions.Like(p.FirstName, $"%{searchText}%")) ||
                    (p.LastName != null && EF.Functions.Like(p.LastName, $"%{searchText}%"))).ToListAsync();

                    Users.Clear();
                    //Users.AddRange(records.ToModels());
                    foreach (var record in records.ToModels())
                    {
                        Users.Add(record);
                    }
                }
                finally
                {
                    IsBusy = false;
                }
            }

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadData();
            }
        }

        public UsersViewModel(IParaglidingDbContext dbContext)
        {
            this.dbContext = dbContext;
            LoadItemsCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => 
            {
                await Shell.Current.GoToAsync("UserPage"); 
            });

            SelectedUserChangedCommand = new Command(async () => await NavigateToUserPage()); 
        }

        private async Task NavigateToUserPage()
        {
            try
            {
                if(IsBusy)
                {
                    return;
                }

                IsBusy = true;
                await Shell.Current.GoToAsync($"UserPage?SelectedItem={JsonConvert.SerializeObject(selectedUser)}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void LoadData()
        {
            var pilots = (await this.dbContext.Pilots.Select(p => p).ToListAsync()).ToModels();
            Users.Clear();
            foreach (var record in pilots)
            {
                Users.Add(record);
            }
            OnPropertyChanged(nameof(Flight));
        }
    }
}
