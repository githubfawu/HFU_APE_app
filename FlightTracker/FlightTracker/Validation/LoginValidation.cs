using System;

namespace FlightTracker.Validation
{
    internal class LoginValidation
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
    }
}
