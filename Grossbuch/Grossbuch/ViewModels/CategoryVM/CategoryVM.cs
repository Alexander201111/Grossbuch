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
    public class CategoryVM : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public Command LoadItemsCommand { get; set; }
        public User User;

        private CategoryRepository repository;

        public CategoryVM(User _user)
        {
            Title = "Категории";
            User = _user;

            Categories = new ObservableCollection<Category>();
            repository = new CategoryRepository(User);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewCategoryPage, Category>(this, "AddCategory", async (obj, item) =>
            {
                var newItem = item as Category;
                Categories.Add(newItem);
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
                repository = new CategoryRepository(User);
                Categories.Clear();
                var items = await repository.GetItemsAsync(1);
                foreach (var item in items)
                {
                    Categories.Add(item);
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
            repository.UpdateCategoriesDBAsync();
        }
    }
}
