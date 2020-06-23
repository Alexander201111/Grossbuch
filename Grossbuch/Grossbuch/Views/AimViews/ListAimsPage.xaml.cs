using Grossbuch.Models;
using Grossbuch.ViewModels;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListAimsPage : ContentPage
    {
        AimVM viewModel;

        public ListAimsPage(User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new AimVM(user);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Operation op))
                return;

            await Navigation.PushAsync(new DetailAimPage(new AimDetailVM(viewModel.Aims.FirstOrDefault(o => o.Id == op.Id), viewModel.User, viewModel.repository)));

            // Manually deselect item.
            AimsListView.SelectedItem = null;
        }

        async void AddAim_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewAimPage(null, viewModel.User, viewModel.repository)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Aims == null || viewModel.Aims.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
