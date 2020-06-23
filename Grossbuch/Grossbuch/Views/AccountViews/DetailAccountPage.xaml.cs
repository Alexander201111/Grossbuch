using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;
using System;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailAccountPage : ContentPage
    {
        AccountDetailVM viewModel;

        public DetailAccountPage(AccountDetailVM viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewAccountPage(viewModel)));
        }

        void Delete_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewOperationPage(viewModel.Operation, viewModel.opRepository)));
            //await Navigation.PushModalAsync(new NavigationPage(new NewAccountPage(viewModel)));
        }
    }
}