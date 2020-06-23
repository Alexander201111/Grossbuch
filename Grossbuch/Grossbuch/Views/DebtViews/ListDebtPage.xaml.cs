using Grossbuch.Models;
using Grossbuch.ViewModels;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListDebtsPage : ContentPage
    {
        DebtVM viewModel;

        public ListDebtsPage(User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new DebtVM(user);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Operation op))
                return;

            await Navigation.PushAsync(new DetailDebtPage(new DebtDetailVM(viewModel.Debts.FirstOrDefault(o => o.Id == op.Id), viewModel.User, viewModel.repository)));

            // Manually deselect item.
            DebtsListView.SelectedItem = null;
        }

        async void AddDebt_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewDebtPage(null, viewModel.User, viewModel.repository)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Debts == null || viewModel.Debts.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
