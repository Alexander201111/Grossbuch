using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailCategoryPage : ContentPage
    {
        CategoryDetailVM viewModel;

        public DetailCategoryPage(CategoryDetailVM viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public DetailCategoryPage()
        {
            InitializeComponent();

            viewModel = new CategoryDetailVM(new Category("Новая группа", null));
            BindingContext = viewModel;
        }
    }
}