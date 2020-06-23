using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Grossbuch.Models;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListCurrenciesPage : ContentView
    {
        public ListCurrenciesPage()
        {
            InitializeComponent();
        }
    }
}