namespace FlightTracker.ViewModels
{
    public interface ISelectableViewModel
    {
        void SetSelectedItem(string item);
        void Clear();
    }
}
