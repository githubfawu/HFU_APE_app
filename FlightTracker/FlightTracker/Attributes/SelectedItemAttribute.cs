using Xamarin.Forms;

namespace FlightTracker.Attributes
{
    public class SelectedItemAttribute : QueryPropertyAttribute
    {
        public SelectedItemAttribute() : base("SelectedItem", "SelectedItem")
        {
        }
    }
}
