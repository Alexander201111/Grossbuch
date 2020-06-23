using Grossbuch.Models;
using Grossbuch.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnaliticListOperationsPage : ContentPage
    {
        AnaliticsVM analiticsVM;
        User User;

        public AnaliticListOperationsPage()
        {
            InitializeComponent();
            BindingContext = analiticsVM = new AnaliticsVM(null);
        }

        public AnaliticListOperationsPage(User _user)
        {
            InitializeComponent();
            User = _user;
            BindingContext = analiticsVM = new AnaliticsVM(_user);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Operation op))
                return;

            await Navigation.PushAsync(new DetailOperationPage(new OperationDetailVM(analiticsVM.Operations.FirstOrDefault(o => o.Id == op.Id), User, analiticsVM.opRepository)));

            // Manually deselect item.
            OperationsListView.SelectedItem = null;
        }

        void FormedListOperations(object sender, PropertyChangedEventArgs e)
        {
            analiticsVM.FormedListOperationsAsync();
        }
    }
}
