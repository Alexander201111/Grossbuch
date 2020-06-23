using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailOperationPage : ContentPage
    {
        OperationDetailVM viewModel;

        //public DetailOperationPage()
        //{
        //    InitializeComponent();

        //    var op = new Operation
        //    {
        //        Date = DateTime.Now,
        //        Description = "This is an item description."
        //    };

        //    viewModel = new OperationDetailVM(op, viewModel.User);
        //    BindingContext = viewModel;
        //}

        public DetailOperationPage(OperationDetailVM viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewOperationPage(viewModel.Operation, viewModel.opRepository)));
            await Navigation.PushModalAsync(new NavigationPage(new NewOperationPage(viewModel)));
        }
    }
}