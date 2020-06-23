using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;
using System.Linq;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOperationsPage : ContentPage
    {
        OperationVM viewModel;
        object filter;

        public ListOperationsPage(User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new OperationVM(user);
        }

        public ListOperationsPage(User user, object _filter = null)
        {
            InitializeComponent();
            filter = _filter;
            BindingContext = viewModel = new OperationVM(user, filter);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Operation op))
                return;

            await Navigation.PushAsync(new DetailOperationPage(new OperationDetailVM(viewModel.Operations.FirstOrDefault(o => o.Id == op.Id), viewModel.User, viewModel.opRepository)));

            // Manually deselect item.
            OperationsListView.SelectedItem = null;
        }

        async void AddOperation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewOperationPage(null, viewModel.User, viewModel.opRepository)));
        }

        void SincWithServer(object sender, EventArgs e)
        {
            viewModel.SincWithServer();
        }

        async void Change_Clicked(object sender, EventArgs e)
        {

            if (filter is Account _filter)
            {
                await Navigation.PushModalAsync(new NavigationPage(new NewAccountPage(_filter, viewModel.User)));
            }
            //else
            //{
            //    Purpose _filter1 = filter as Purpose;
            //    await Navigation.PushModalAsync(new NavigationPage(new NewPurposePage(_filter1)));
            //}
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Operations == null || viewModel.Operations.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void ChangeColorSum()
        {
            foreach(var view in OperationsListView.ItemTemplate.Bindings.Values)
            {
                
            }
        }
    }
}
