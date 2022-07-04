
using System;

namespace FlightTracker.Validation
{
    public interface IValidationService
    {
        public string IsDateOutOfRange(DateTime date);
        public string IsDistanceOutOfRange(int? distance);
        public string IsCommentOutOfRange(int? length);
        public string IsDurationOutOfRange(TimeSpan duration);
        public string IsUsernameOutOfRange(string username);
        public string IsPasswordOutOfRange(string password);
        public string IsFirstNameOutOfRange(string firstname);
        public string IsLastNameOutOfRange(string lastName);
        public string IsPasswordConfirm(string password, string passwordConfirmation);
    }
}
