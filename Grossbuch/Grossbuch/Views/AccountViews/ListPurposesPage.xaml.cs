using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPurposesPage : ContentPage
    {
        PurposeVM viewModel;
        User User;

        public ListPurposesPage(User _user)
        {
            InitializeComponent();
            User = _user;
            BindingContext = viewModel = new PurposeVM(_user);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Account item))
                return;

            await Navigation.PushAsync(new ListOperationsPage(viewModel.User, item));

            // Manually deselect item.
            PurposesListView.SelectedItem = null;
        }

        async void AddPurpose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewAccountPage(false, User)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Purposes.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        void SincWithServer(object sender, EventArgs e)
        {
            viewModel.SincWithServer();
        }

        void ListView_ItemTapped(object sender, EventArgs e)
        {

        }
    }
}
