using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailDebtPage : ContentPage
    {
        DebtDetailVM viewModel;

        object Facility { get; set; }

        public DetailDebtPage(DebtDetailVM viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewDebtPage(viewModel)));
        }
    }
}