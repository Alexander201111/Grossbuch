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
    public class PurposeVM : BaseViewModel
    {
        public ObservableCollection<Account> Purposes { get; set; }
        public Command LoadItemsCommand { get; set; }
        public User User;

        private AccountRepository repository;

        public PurposeVM(User _user)
        {
            Title = "Места";
            User = _user;
            repository = new AccountRepository(User);
            Purposes = new ObservableCollection<Account>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewAccountPage, Account>(this, "AddPurpose", async (obj, item) =>
            {
                var newItem = item as Account;
                Purposes.Add(newItem);
            });
            MessagingCenter.Subscribe<NewAccountPage, Account>(this, "UpdatePurpose", async (obj, item) =>
            {
                var newItem = item as Account;
                await repository.UpdateItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                repository = new AccountRepository(User);
                Purposes.Clear();
                var items = await repository.GetPurposesAsync();
                foreach (var item in items)
                {
                    Purposes.Add(item);
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

        public void SincWithServer()
        {
            repository.UpdateAccountsDBAsync("purposes/");
        }
    }
}
