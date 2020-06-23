using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Views;
using Grossbuch.Models;
using System.Linq;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Grossbuch
{
    public partial class App : Application
    {
        public const string DBFILENAME = "grossbuch.db";
        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();

            //Application.Current.MainPage = new MainPage(new User());

            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME);
            using (var db = new Context(dbPath))
            {
                // Удаляем бд, если она существует
                db.Database.EnsureDeleted();

                // Создаем бд, если она отсутствует
                db.Database.EnsureCreated();
                if (db.Operations.Count() == 0 && db.Users.Count() == 0)
                {
                    MockData.Seed(db);
                }
            }

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage(new User()));
            }

            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
