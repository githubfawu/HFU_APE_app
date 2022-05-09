using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TestApp1.Models;
using Xamarin.Forms;

namespace TestApp1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string itemTestId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public string ItemTestId
        {
            get
            {
                return itemTestId;
            }
            set
            {
                itemTestId = value;
                LoadTestItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void LoadTestItemId(string itemId)
        {
            try
            {
                var testitem = await DataStore.GetTestItemAsync(itemId);
                Id = testitem.Id;
                Text = testitem.Text;
                Description = testitem.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load TestItem");
            }
        }
    }
}
