using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailProductPage : ContentPage
    {
        ProductDetailVM viewModel;

        public DetailProductPage(ProductDetailVM viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public DetailProductPage()
        {
            InitializeComponent();

            viewModel = new ProductDetailVM(new Product("Новый товар", 0, 0));
            BindingContext = viewModel;
        }
    }
}