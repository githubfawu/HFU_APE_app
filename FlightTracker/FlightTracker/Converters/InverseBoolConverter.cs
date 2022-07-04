using System;
using System.Globalization;
using Xamarin.Forms;

namespace FlightTracker.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bVal = value as bool?;
            if (bVal != null && bVal.HasValue && bVal.Value)
                return false;

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
