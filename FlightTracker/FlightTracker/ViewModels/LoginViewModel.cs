using System.Linq;
using FlightTracker.DataAccess;
using FlightTracker.DataAccess.Queries;
using FlightTracker.DataAccess.Queries.Response;
using FlightTracker.Models;
using FlightTracker.Validation;
using Xamarin.Forms;

namespace FlightTracker.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IDbQuery<(string, string), IdentityUser> identityQuery;
        private readonly IParaglidingDbContext dbContext;
        private readonly LoginValidation validation = new LoginValidation();

        private bool isUserLoggedIn;
        private Pilot user;
        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }
        
        #region Property Username
        private string usernameError;
        public string UsernameError
        {
            get => usernameError;
            set
            {
                usernameError = value;
                OnPropertyChanged(nameof(UsernameError));
            }
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                UsernameError = validation.IsUsernameOutOfRange(username);
                OnPropertyChanged(nameof(Username));
                LoginCommand.ChangeCanExecute();
            }
        }
        #endregion

        #region Property Password
        private string passwordError;
        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged(nameof(PasswordError));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                PasswordError = validation.IsPasswordOutOfRange(password);
                OnPropertyChanged(nameof(Password));
                LoginCommand.ChangeCanExecute();
            }
        }
        #endregion

        public Pilot User { get => user; set => SetProperty(ref user, value); }

        public LoginViewModel(IDbQuery<(string, string), IdentityUser> identityQuery, IParaglidingDbContext dbContext)
        {
            this.identityQuery = identityQuery;
            LoginCommand = new Command(OnLoginClicked, (x) => !HasErrors());
            LogoutCommand = new Command(OnLogoutClicked);
            this.dbContext = dbContext;

            IsUserLoggedIn = IsSessionValid();
        }

        private async void OnLogoutClicked(object obj)
        {
            Session.EndSession();
            IsUserLoggedIn = IsSessionValid();
            await GoBack();
        }

        private void Clear()
        {
            Username = "";
            Password = "";
        }

        public bool IsUserLoggedIn { get => isUserLoggedIn; set => SetProperty(ref isUserLoggedIn, value); }

        public bool IsSessionValid()
        {
            var session = GetSession();
            if (session == null)
            {
                IsUserLoggedIn = false;
                Clear();
                return false;
            }

            var isSessionValid = session != null && session.IsValid();
            if (isSessionValid)
            {
                var pilot = dbContext.Pilots.SingleOrDefault(p => p.Username == session.Username);
                User = new Pilot() { Username = pilot.Username, FirstName = pilot.FirstName, LastName = pilot.LastName, Id = pilot.Id, Role = pilot.Role };
                IsUserLoggedIn = true;
            }

            return isSessionValid;
        }

        private bool HasErrors()
        {
            return (UsernameError != "" ||
                    PasswordError != "" ||
            string.IsNullOrWhiteSpace(Username) ||
            string.IsNullOrWhiteSpace(Password));
        }

        private async void OnLoginClicked(object obj)
        {
            var result = await this.identityQuery.ExecuteAsync((Username, Password));
            if (result != null)
            {
                Session.StartSession(result.UserName, result.DisplayName, result.Role);
                IsUserLoggedIn = IsSessionValid();
                Clear();
                await Shell.Current.GoToAsync($"//HomePage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Ungültige Eingaben", "Benutzername und Passwort stimmen nicht überein.", "OK");
                Password = "";
            }
        }
    }
}
