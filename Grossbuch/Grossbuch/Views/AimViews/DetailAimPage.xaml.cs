using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailAimPage : ContentPage
	{
        AimDetailVM viewModel;

        object Facility { get; set; }

        public DetailAimPage(AimDetailVM viewModel)
		{
			InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            if (viewModel.Aim.Facility1 != null) { Facility = viewModel.Aim.Facility1; }
            else { Facility = viewModel.Aim.Facility1; }
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewAimPage(viewModel)));
        }
    }
}