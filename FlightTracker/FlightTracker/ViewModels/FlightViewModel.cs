using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightTracker.DataAccess;
using FlightTracker.Models;
using FlightTracker.Validation;
using Xamarin.Forms;

namespace FlightTracker.ViewModels
{
    public class FlightViewModel : BaseViewModel, ISelectableViewModel, IListViewModel, INotifyPropertyChanged
    {
        private readonly IParaglidingDbContext dbContext;
        private Pilot selectedPilot;
        private readonly FlightValidation validation = new FlightValidation();

        public FlightViewModel(IParaglidingDbContext dbContext)
        {
            this.SelectedDate = DateTime.Today;
            this.Duration = new TimeSpan(0, 0, 0);

            this.dbContext = dbContext;
            this.DeleteCommand = new Command(async () =>
            {
                await Delete();
            });

            this.CancelCommand = new Command(async () => await CancelCreate());
            this.SaveCommand = new Command(async () => await Save());
            this.LoadItemsCommand = new Command(() => LoadData());
            
        }
        private async Task CancelCreate()
        {
            Cancel();
            await GoToHomePage();
        }
        private async Task Delete()
        {
            var confirmed = await Shell.Current.DisplayAlert("Löschen", "Bist du sicher, dass du diesen Flug löschen möchtest?", "Ja", "Nein");
            if (confirmed)
            {
                if (Id > 0)
                {
                    var flight = await dbContext.Flights.FindAsync(Id);
                    if (flight != null)
                    {
                        dbContext.Flights.Remove(flight);
                        await dbContext.SaveAsync();
                        await GoBack();
                    }
                }
                else
                {
                    await GoToHomePage();
                }                
            }
        }

        private async Task Save()
        {
            if(HasErrors())
            {
                return;
            }
            try
            {
                IsBusy = true;
                var owner = await dbContext.Pilots.FindAsync(selectedPilot.Id);
                if (Id > 0)
                {
                    var flight = await dbContext.Flights.FindAsync(id);
                    if(flight != null)
                    {
                        flight.Duration = Duration;
                        flight.Distance = Distance;
                        flight.Date = SelectedDate.Date;
                        flight.Comments = Comments;
                        flight.Owner = owner;
                        dbContext.Flights.Update(flight);
                    }
                }
                else
                {
                    await dbContext.Flights.AddAsync(new DataAccess.Entities.Flight
                    {
                        Comments = Comments,
                        Date = SelectedDate.Date,
                        Distance = Distance,
                        Duration = Duration,
                        Owner = owner
                });                                 
                }

                await dbContext.SaveAsync();
                Cancel();
                await GoToHomePage();
                await GoBack();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ObservableCollection<Pilot> Pilots { get; private set; } = new ObservableCollection<Pilot>();

        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        
        private int id;
        public int Id { get => id; set => SetProperty(ref id, value); }
        public Pilot SelectedPilot { get => selectedPilot; set => SetProperty(ref selectedPilot, value); }

        #region Property SelectedDate
        private string selectedDateError;
        public string SelectedDateError
        {
            get => selectedDateError;
            set
            {
                selectedDateError = value;
                OnPropertyChanged(nameof(SelectedDateError));
            }
        }

        private DateTime selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                SelectedDateError = validation.IsDateOutOfRange(selectedDate);
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        #endregion

        #region Property Distance
        private string distanceError;
        public string DistanceError
        {
            get => distanceError;
            set
            {
                distanceError = value;
                OnPropertyChanged(nameof(DistanceError));
            }
        }

        private int distance;
        public int Distance
        {
            get => distance;
            set
            {
                distance = value;
                DistanceError = validation.IsDistanceOutOfRange(distance);
                OnPropertyChanged(nameof(Distance));
            }
        }
        #endregion

        #region Property Comments
        private string commentsError;
        public string CommentsError
        {
            get => commentsError;
            set
            {
                commentsError = value;
                OnPropertyChanged(nameof(CommentsError));
            }
        }

        private string comments;
        public string Comments
        {
            get => comments;
            set
            {
                comments = value;
                CommentsError = validation.IsCommentOutOfRange(comments.Length);
                OnPropertyChanged(nameof(Comments));
            }
        }
        #endregion

        #region Property Duration
        private string durationError;
        public string DurationError
        {
            get => durationError;
            set
            {
                durationError = value;
                OnPropertyChanged(nameof(DurationError));
            }
        }

        private TimeSpan duration;
        public TimeSpan Duration
        {
            get => duration;
            set
            {
                duration = value;
                DurationError = validation.IsDurationOutOfRange(duration);
                OnPropertyChanged(nameof(Duration));
            }
        }
        #endregion

        public bool IsAdminUser
        {
            get
            {
                var session = GetSession();
                return session != null &&
                    session.IsValid() == true &&
                    session.UserRole == Role.Administrator;
            }
        }

        public void SetSelectedItem(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                return;
            }

            var flight = JsonConvert.DeserializeObject<Flight>(item);
            if (flight != null)
            {
                this.Id = flight.Id;
                this.SelectedPilot = Pilots.FirstOrDefault(p => p.Id == flight.Owner.Id);
                this.SelectedDate = flight.Date;
                this.Distance = flight.Distance;
                this.Duration = flight.Duration;
                this.Comments = flight.Comments;
                Title = SelectedPilot.ToString();
            }
        }

        private void Cancel()
        {
            this.SelectedPilot = null;
            this.SelectedDate = DateTime.Now;
            this.Distance = 0;
            this.Duration = new TimeSpan(0, 30, 0);
            this.Comments = "";
        }

        public async void LoadData()
        {
            try
            {
                IsBusy = true;
                var session = GetSession();
                if(session == null || !session.IsValid())
                {
                    await GoToLoginPage();
                    return;
                }

                var pilots = (await dbContext.Pilots.ToListAsync()).ToModels();
                var currentUser = session.Username;
                Pilots.Clear();
                foreach (var pilot in pilots)
                {
                    Pilots.Add(pilot);
                }
                this.SelectedPilot = pilots.FirstOrDefault(p => p.Username == currentUser);
                OnPropertyChanged(nameof(IsAdminUser));
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand LoadItemsCommand { get; }
        public bool HasErrors()
        {
            return this.SelectedPilot == null ||
                this.SelectedDateError != "" ||
                this.DistanceError != "" ||
                this.DurationError != "" ||
                this.CommentsError != "";
        }

        public void Clear()
        {
            this.Id = 0;            
        }
    }
}
