using Grossbuch.Models;
using Grossbuch.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage(User _user)
		{
			InitializeComponent ();
		}

        void DeleteDB(object sender, EventArgs e)
        {
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath("grossbuch.db");
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

                DisplayAlert("Уведомление", "База данных удалена", "ОK");
            }
        }
    }
}