using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProductPage : ContentPage
    {
        public Product Product { get; set; }

        public NewProductPage()
        {
            InitializeComponent();

            Product = new Product();
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddProduct", Product);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}