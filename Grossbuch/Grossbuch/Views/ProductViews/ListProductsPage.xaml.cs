using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListProductsPage : ContentPage
    {
        ProductVM viewModel;

        public ListProductsPage(User _user)
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductVM(_user);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Product item))
            {
                return;
            }

            //await Navigation.PushAsync(new DetailProductPage(new ProductDetailVM(item)));
            await Navigation.PushAsync(new ListOperationsPage(viewModel.User, item));

            // Manually deselect item.
            ProductsListView.SelectedItem = null;
        }

        async void AddProduct_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewProductPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Products.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        void SincWithServer(object sender, EventArgs e)
        {
            viewModel.SincWithServer();
        }
    }
}