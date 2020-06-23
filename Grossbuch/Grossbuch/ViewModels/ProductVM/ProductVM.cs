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
    public class ProductVM : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public Command LoadItemsCommand { get; set; }
        public User User;

        private ProductRepository repository;

        public ProductVM(User _user)
        {
            Title = "Категории";
            User = _user;

            Products = new ObservableCollection<Product>();
            repository = new ProductRepository(User);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewProductPage, Product>(this, "AddProduct", async (obj, item) =>
            {
                var newItem = item as Product;
                Products.Add(newItem);
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
                repository = new ProductRepository(User);
                Products.Clear();
                var items = await repository.GetItemsAsync();
                foreach (var item in items)
                {
                    Products.Add(item);
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
            //repository.UpdateProductsDBAsync();
        }
    }
}
