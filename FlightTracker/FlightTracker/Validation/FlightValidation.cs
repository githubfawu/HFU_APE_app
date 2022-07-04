using System;

namespace FlightTracker.Validation
{
    public class FlightValidation
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
    }
}
