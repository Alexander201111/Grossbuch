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
    class OperationVM : BaseViewModel
    {
        public ObservableCollection<Operation> Operations { get; set; }
        public Command LoadItemsCommand { get; set; }
        public User User;

        public bool IsHideBalanceField { get; set; }
        public object Filter { get; set; }

        public OperationRepository opRepository;

        public OperationVM(User _user, object filter = null)
        {
            User = _user;
            opRepository = new OperationRepository(_user);
            Filter = filter;
            if (filter != null)
            {
                if (filter is Account accountForFilter) { Title = accountForFilter.Title; }
                else
                {
                    if (filter is Category categoryForFilter) { Title = categoryForFilter.Title; }
                    else
                    {
                        Title = "Лента";
                    }
                }
            }
            else { Title = "Лента"; }

            Operations = new ObservableCollection<Operation>();

            if (filter != null)
            {
                if (filter is Category categoryForFilter) { IsHideBalanceField = true; }
                else { IsHideBalanceField = false; }
            }

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(filter));

            MessagingCenter.Subscribe<NewOperationPage, Operation>(this, "AddOperation", (obj, item) => {
                Operations.Insert(0, item as Operation);
            });

            //MessagingCenter.Subscribe<NewOperationPage, Operation>(this, "UpdateOperation", (obj, item) => {
            //    var newItem = item as Operation;
            //    var oldItem = Operations[Operations.IndexOf(Operations.FirstOrDefault(i => i.Id == newItem.Id))];
            //    //await opRepository.UpdateItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand(object filter)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Operations.Clear();
                var items = await opRepository.GetItemsAsync(User.Id, filter);
                foreach (var item in items)
                {
                    Operations.Add(item);
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
            opRepository.UpdateOperationDBAsync(User.Id);
        }
    }
}
