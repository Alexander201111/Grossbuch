using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

using Grossbuch.Models;
using Grossbuch.Views;
using Grossbuch.Repositories;

namespace Grossbuch.ViewModels
{
    class DebtVM : BaseViewModel
    {
        public ObservableCollection<Debt> Debts { get; set; }
        public Command LoadItemsCommand { get; set; }
        public User User;

        public DebtRepository repository = null;

        public DebtVM(User _user)
        {
            User = _user;
            Title = "Кредиты";
            repository = new DebtRepository(_user);

            Debts = new ObservableCollection<Debt>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewDebtPage, Debt>(this, "AddDebt", (obj, item) => {
                Debts.Insert(0, item as Debt);
            });

            //MessagingCenter.Subscribe<NewOperationPage, Operation>(this, "UpdateOperation", (obj, item) => {
            //    var newItem = item as Operation;
            //    var oldItem = Operations[Operations.IndexOf(Operations.FirstOrDefault(i => i.Id == newItem.Id))];
            //    //await opRepository.UpdateItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Debts.Clear();
                var items = await repository.GetItemsAsync(User.Id);
                foreach (var item in items)
                {
                    Debts.Add(item);
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