using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListCategoriesPage : ContentPage
    {
        CategoryVM viewModel;

        public ListCategoriesPage(User _user)
        {
            InitializeComponent();

            BindingContext = viewModel = new CategoryVM(_user);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Category item))
            {
                return;
            }

            //await Navigation.PushAsync(new DetailCategoryPage(new CategoryDetailVM(item)));
            await Navigation.PushAsync(new ListOperationsPage(viewModel.User, item));

            // Manually deselect item.
            CategoriesListView.SelectedItem = null;
        }

        async void AddCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCategoryPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Categories.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        void SincWithServer(object sender, EventArgs e)
        {
            viewModel.SincWithServer();
        }
    }
}