using Grossbuch.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Operations, Title="Лента" },
                new HomeMenuItem {Id = MenuItemType.Analitics, Title="Аналитика" },
                new HomeMenuItem {Id = MenuItemType.Accounts, Title="Кошельки" },
                new HomeMenuItem {Id = MenuItemType.Categories, Title="Категории" },
                new HomeMenuItem {Id = MenuItemType.Purposes, Title="Места" },
                new HomeMenuItem {Id = MenuItemType.Tasks, Title="Цели" },
                new HomeMenuItem {Id = MenuItemType.Debts, Title="Кредиты" },
                new HomeMenuItem {Id = MenuItemType.Settings, Title="Настройки" },
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var page = Application.Current.MainPage;
                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}