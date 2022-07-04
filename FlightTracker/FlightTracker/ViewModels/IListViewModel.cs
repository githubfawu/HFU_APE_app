using System.Windows.Input;

namespace FlightTracker.ViewModels
{
    public interface IListViewModel
    {
        void LoadData();
        ICommand LoadItemsCommand { get; }
    }
}
