using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;

namespace FlightTracker.Converters
{
    public class IsCollectionNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            var collection = value as ICollection;
            if (collection == null)
            {
                return false;
            }
            else if (collection.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
