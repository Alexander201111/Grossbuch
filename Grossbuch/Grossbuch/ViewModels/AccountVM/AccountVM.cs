using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

using Grossbuch.Models;
using Grossbuch.Views;
using Grossbuch.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Grossbuch.ViewModels
{
    public class AccountVM : BaseViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public Command LoadItemsCommand { get; set; }
        public User User;

        public AccountRepository repository;

        public AccountVM(User _user)
        {
            Title = "Кошельки";
            User = _user;
            repository = new AccountRepository(User);
            Accounts = new ObservableCollection<Account>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewAccountPage, Account>(this, "AddAccount", (obj, item) =>
            {
                var newItem = item as Account;
                Accounts.Add(newItem);
            });

            MessagingCenter.Subscribe<NewAccountPage, Account>(this, "UpdateAccount", async (obj, item) =>
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
                Accounts.Clear();
                var items = await repository.GetAccountsAsync();
                foreach (var item in items)
                {
                    Accounts.Add(item);
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
            repository.UpdateAccountsDBAsync("accounts/");
        }
    }
}
