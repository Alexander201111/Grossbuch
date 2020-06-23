using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;
using System.Linq;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListAccountsPage : ContentPage
    {
        AccountVM viewModel;
        User User;

        public ListAccountsPage(User _user)
        {
            InitializeComponent();
            User = _user;
            BindingContext = viewModel = new AccountVM(_user);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Account item))
                return;

            await Navigation.PushAsync(new ListOperationsPage(viewModel.User, item));

            // Manually deselect item.
            AccountsListView.SelectedItem = null;
        }

        async void AddAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewAccountPage(true, User)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Accounts.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!(e.Item is Account op))
                return;

            await Navigation.PushAsync(new DetailAccountPage(new AccountDetailVM(viewModel.Accounts.FirstOrDefault(o => o.Id == op.Id), viewModel.User, viewModel.repository)));

            // Manually deselect item.
            AccountsListView.SelectedItem = null;
        }

        void SincWithServer(object sender, EventArgs e)
        {
            viewModel.SincWithServer();
        }
    }
}
