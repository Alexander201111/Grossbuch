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
    class AimVM : BaseViewModel
    {
        public ObservableCollection<Aim> Aims { get; set; }
        public Command LoadItemsCommand { get; set; }
        public User User;

        public AimRepository repository = new AimRepository();

        public AimVM(User _user)
        {
            User = _user;
            Title = "Цели";

            Aims = new ObservableCollection<Aim>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewAimPage, Aim>(this, "AddAim", (obj, item) => {
                Aims.Insert(0, item as Aim);
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
                Aims.Clear();
                var items = await repository.GetItemsAsync(User.Id);
                foreach (var item in items)
                {
                    Aims.Add(item);
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
