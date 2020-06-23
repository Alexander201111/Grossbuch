using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

using Grossbuch.Models;
using Grossbuch.Repositories;
using Grossbuch.Views;

namespace Grossbuch.ViewModels
{
    public class CurrencyVM : BaseViewModel
    {
        public ObservableCollection<Currency> Currencies { get; set; }
        public Command LoadItemsCommand { get; set; }

        private CurrencyRepository repository = new CurrencyRepository();

        public CurrencyVM()
        {
            Title = "Валюта";
            Currencies = new ObservableCollection<Currency>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewCurrencyPage, Currency>(this, "AddCurrency", async (obj, item) =>
            {
                var newItem = item as Currency;
                Currencies.Add(newItem);
                await repository.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Currencies.Clear();
                var items = await repository.GetItemsAsync(1);
                foreach (var item in items)
                {
                    Currencies.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
