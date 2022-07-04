
namespace FlightTracker.Validation
{
    internal class UserValidation
    {
        public string IsUsernameOutOfRange(string username)
        {
            if (username.Length == 0)
            {
                return "Der Benutzername darf nicht leer sein";
            }
            if (username.Length < 3)
            {
                return "Der Benutzername darf nicht kürzer als 3 Zeichen sein";
            }
            if (username.Length > 20)
            {
                return "Der Benutzername darf nicht länger als 20 Zeichen sein";
            }
            return "";
        }

        public string IsFirstNameOutOfRange(string firstname)
        {
            if (firstname.Length == 0)
            {
                return "Der Vorname darf nicht leer sein";
            }
            if (firstname.Length < 3)
            {
                return "Der Vorname darf nicht kürzer als 3 Zeichen sein";
            }
            if (firstname.Length > 20)
            {
                return "Der Vorname darf nicht länger als 20 Zeichen sein";
            }
            return "";
        }

        public string IsLastNameOutOfRange(string lastname)
        {
            if (lastname.Length == 0)
            {
                return "Der Nachname darf nicht leer sein";
            }
            if (lastname.Length < 3)
            {
                return "Der Nachname darf nicht kürzer als 3 Zeichen sein";
            }
            if (lastname.Length > 20)
            {
                return "Der Nachname darf nicht länger als 20 Zeichen sein";
            }
            return "";
        }
        public string IsPasswordOutOfRange(string password)
        {
            if (password.Length == 0)
            {
                return "Das Passwort darf nicht leer sein";
            }
            if (password.Length < 3)
            {
                return "Das Passwort darf nicht kürzer als 3 Zeichen sein";
            }
            if (password.Length > 20)
            {
                return "Das Passwort darf nicht länger als 20 Zeichen sein";
            }
            return "";
        }

        public string IsPasswordConfirm(string password, string passwordConfirmation)
        {
            if (password != passwordConfirmation)
            {
                return "Die Passwörter stimmen nicht überein";
            }
            return "";
        }
    }
}
