using Grossbuch.Models;
using Grossbuch.Repositories;
using Grossbuch.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grossbuch.ViewModels
{
    public class AuthVM : BaseViewModel
    {
        public User User { get; set; }

        private UserRepository repository = new UserRepository();

        public AuthVM(User _user = null)
        {
            Title = "Авторизация";
            User = _user;
        }

        public async Task LoginAsync(string username, string password)
        {
            User = await repository.LoginAsync(username, password);
        }
    }
}
