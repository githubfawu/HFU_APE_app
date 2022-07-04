using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightTracker.DataAccess;
using FlightTracker.Models;
using FlightTracker.Validation;
using Xamarin.Forms;

namespace FlightTracker.ViewModels
{
    public class UserViewModel : BaseViewModel, ISelectableViewModel
    {
        private int id;
        private Role role;
        private bool isAdministrator;
        private bool isUser;
        private readonly UserValidation validation = new UserValidation();
        private readonly IParaglidingDbContext dbContext;
        public int Id { get => id; set => SetProperty(ref id, value); }

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
            }
        }
        #endregion

        #region Property FirstName
        private string firstNameError;
        public string FirstNameError
        {
            get => firstNameError;
            set
            {
                firstNameError = value;
                OnPropertyChanged(nameof(FirstNameError));
            }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                FirstNameError = validation.IsFirstNameOutOfRange(firstName);
                OnPropertyChanged(nameof(FirstName));
            }
        }
        #endregion

        #region Property Lastname
        private string lastNameError;
        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged(nameof(LastNameError));
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                LastNameError = validation.IsLastNameOutOfRange(lastName);
                OnPropertyChanged(nameof(LastName));
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
            }
        }
        #endregion

        #region Property PasswordConfirmation
        private string passwordConfirmationError;
        public string PasswordConfirmationError
        {
            get => passwordConfirmationError;
            set
            {
                passwordConfirmationError = value;
                OnPropertyChanged(nameof(PasswordConfirmationError));
            }
        }

        private string passwordConfirmation;
        public string PasswordConfirmation
        {
            get => passwordConfirmation;
            set
            {
                passwordConfirmation = value;
                PasswordConfirmationError = validation.IsPasswordConfirm(passwordConfirmation, Password);
                OnPropertyChanged(nameof(PasswordConfirmation));
            }
        }
        #endregion

        public Role Role { get => role; private set => SetProperty(ref role, value); }

        public bool IsAdministrator { get => isAdministrator; 
            set
            {
                SetProperty(ref isAdministrator, value);
                Role = value? Role.Administrator : Role.User;
            }
        }
        public bool IsUser { get => isUser; set
            {
                SetProperty(ref isUser, value);
                Role = value ? Role.User : Role.Administrator;
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public UserViewModel(IParaglidingDbContext dbContext)
        {
            this.dbContext = dbContext;
            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await GoBack());            
        }

        private bool HasErrors()
        {
            return (UsernameError != "" ||
                FirstNameError != "" ||
                LastNameError != "" ||
                PasswordError != "");
        }

        private async Task Save()
        {
            if (HasErrors())
            {
                return;
            }

            var session = GetSession();
            if (session?.IsValid() == true && session.UserRole == Role.Administrator)
            {
                if (Id > 0)
                {
                    var existingUser = await dbContext.Pilots.FindAsync(Id);
                    if (existingUser != null)
                    {
                        existingUser.Password = Password;
                        existingUser.FirstName = FirstName;
                        existingUser.LastName = LastName;
                        existingUser.Username = Username;
                        existingUser.Role = Role;
                        dbContext.Pilots.Update(existingUser);
                        await dbContext.SaveAsync();
                        Clear();
                        await GoBack();

                        return;
                    }
                }

                var isUserNameTaken = (await dbContext.Pilots.FirstOrDefaultAsync(p => EF.Functions.Like(p.Username, $"{Username}"))) != null;
                if (isUserNameTaken)
                {
                    await Shell.Current.DisplayAlert("Benutzername bereits vergeben", $"Der Benutzername {Username} ist nicht verfügbar. Gib bitte einen anderen Benutzernamen ein.", "OK");
                    return;
                }
                await dbContext.Pilots.AddAsync(new DataAccess.Entities.Pilot
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Username = Username,
                    Role = Role,
                    Password = Password,
                });
                await dbContext.SaveAsync();
                Clear();
                await GoBack();
            }
        }

        public void SetSelectedItem(string item)
        {
            if(string.IsNullOrWhiteSpace(item))
            {
                Clear();
                return ;
            }

            var user = JsonConvert.DeserializeObject<Pilot>(item);
            Id = user.Id;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Password = "";
            PasswordConfirmation = "";
            IsAdministrator = user.Role == Role.Administrator;
            IsUser = !IsAdministrator;
            Title = user.ToString();
        }

        public void Clear()
        {
            Id = 0;
            Username = "";
            FirstName = ""; 
            LastName = "";
            Password = "";
            PasswordConfirmation= "";
            IsAdministrator = false;
            IsUser= false;
        }
    }
}
