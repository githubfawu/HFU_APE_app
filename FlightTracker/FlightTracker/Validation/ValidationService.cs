using System;

namespace FlightTracker.Validation
{
    public class ValidationService : IValidationService
    {
        public string IsDateOutOfRange(DateTime date)
        {
            if (date < DateTime.Now.AddDays(-60))
            {
                return "Es können keine Flüge erfasst werden, die älter als 60 Tage sind";
            }
            if (date > DateTime.Now.AddDays(5))
            {
                return "Es können keine Flüge mehr als 5 Tage in der Zukunft erfasst werden";
            }
            return "";
        }

        public string IsDistanceOutOfRange(int? distance)
        {
            if (distance > 999)
            {
                return "Die maximale Distanz beträgt 999 Kilometer";
            }
            if (distance == 0)
            {
                return "Die Flugdistanz darf nicht 0 sein.";
            }
            return "";
        }

        public string IsCommentOutOfRange(int? length)
        {
            if (length > 200)
            {
                return "Der Kommentar darf maximal 200 Zeichen lang sein";
            }
            return "";
        }

        public string IsDurationOutOfRange(TimeSpan duration)
        {
            if (duration.TotalMinutes < 15)
            {
                return "Die Fluglänge muss mindestens 15 Minuten betragen";
            }
            if (duration.TotalHours > 12)
            {
                return "Die Fluglänge darf höchstens 12 Stunden betragen";
            }
            return "";
        }

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
