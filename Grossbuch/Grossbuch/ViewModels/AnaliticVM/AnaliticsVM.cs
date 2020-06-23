using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

using Grossbuch.Models;
using Grossbuch.Repositories;

namespace Grossbuch.ViewModels
{
    class AnaliticsVM : BaseViewModel
    {
        public ObservableCollection<Operation> Operations { get; set; }

        public Command LoadItemsCommand { get; set; }

        public OperationRepository opRepository;

        private readonly User User;
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public string Search { get; set; }

        public AnaliticsVM(User _user)
        {
            Title = "Список транзакций";
            Operations = new ObservableCollection<Operation>();
            User = _user;
            opRepository = new OperationRepository(_user);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Search = "";
            DateFinish = DateTime.Now;
            DateStart = DateFinish.Subtract(new TimeSpan(30, 0, 0, 0));
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Operations.Clear();
                var items = await opRepository.GetItemsSumAsync(1, Search, DateStart, DateFinish);
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

        public async Task FormedListOperationsAsync()
        {
            Operations.Clear();
            var items = await opRepository.GetItemsSumAsync(1, Search, DateStart, DateFinish);
            foreach (var item in items)
            {
                Operations.Add(item);
            }
        }
    }
}
