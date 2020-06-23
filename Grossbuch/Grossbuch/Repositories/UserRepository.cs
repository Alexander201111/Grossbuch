using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grossbuch.Models;
using Grossbuch.Server;
using Xamarin.Forms;

namespace Grossbuch.Repositories
{
    class UserRepository
    {
        private const string DBFILENAME = "grossbuch.db";
        Context db;

        public User User;

        public UserRepository()
        {
            db = new Context(DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME));
        }

        public List<User> GetItems()
        {
            return db.Users.ToList();
        }

        public async Task<User> LoginAsync(string login, string password)
        {
            User = db.Users.SingleOrDefault(u => u.Login == login && u.Password == password);
            if(User == null)
            {
                User = await UserServer.LoginAsync(login, password);
                User.Id2 = User.Id;
                User.Id = (int)await AddItemAsync(User);

                return await Task.FromResult(User);
            }
            return await Task.FromResult(User);
        }

        #region Async Methods
        public async Task<int?> AddItemAsync(User user)
        {
            User newUser = new User()
            {
                Id = 0,
                Id2 = user.Id,
                Name = user.Name,
                Login = user.Login,
                Password = user.Password
            };

            var a = db.Users.Add(newUser);
            db.SaveChanges();
            int? id = a.Entity.Id;
            return await Task.FromResult(id);
        }

        public async Task<bool> UpdateItemAsync(User user)
        {
            var oldItem = db.Users.Where((User arg) => arg.Id == user.Id).FirstOrDefault();
            db.Users.Remove(oldItem);
            db.Users.Add(user);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = db.Users.Where((User arg) => arg.Id == id).FirstOrDefault();
            db.Users.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<User>> GetItemsAsync()
        {
            return await Task.FromResult(db.Users.ToList());
        }

        public async Task<User> GetItemAsync(int id)
        {
            return await Task.FromResult(db.Users.FirstOrDefault(s => s.Id == id));
        }
        #endregion
    }
}
