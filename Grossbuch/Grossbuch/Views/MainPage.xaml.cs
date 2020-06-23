using Grossbuch.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        //public const string DBFILENAME = "grossbuch.db";
        User User;
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage(User user)
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            //GenerateBD();
            User = user;
            Detail = new NavigationPage(new ListOperationsPage(User));

            MenuPages.Add((int)MenuItemType.Operations, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Operations:
                        MenuPages.Add(id, new NavigationPage(new ListOperationsPage(User)));
                        break;
                    case (int)MenuItemType.Analitics:
                        MenuPages.Add(id, new NavigationPage(new AnaliticsPage(User)));
                        break;
                    case (int)MenuItemType.Tasks:
                        MenuPages.Add(id, new NavigationPage(new ListAimsPage(User)));
                        break;
                    case (int)MenuItemType.Debts:
                        MenuPages.Add(id, new NavigationPage(new ListDebtsPage(User)));
                        break;
                    case (int)MenuItemType.Accounts:
                        MenuPages.Add(id, new NavigationPage(new ListAccountsPage(User)));
                        break;
                    case (int)MenuItemType.Categories:
                        MenuPages.Add(id, new NavigationPage(new ListCategoriesPage(User)));
                        break;
                    case (int)MenuItemType.Purposes:
                        MenuPages.Add(id, new NavigationPage(new ListPurposesPage(User)));
                        break;
                    case (int)MenuItemType.Products:
                        MenuPages.Add(id, new NavigationPage(new ListProductsPage(User)));
                        break;
                    case (int)MenuItemType.Settings:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage(User)));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}