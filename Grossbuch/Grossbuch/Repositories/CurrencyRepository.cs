using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grossbuch.Models;
using Xamarin.Forms;

namespace Grossbuch.Repositories
{
    class CurrencyRepository
    {
        private const string DBFILENAME = "grossbuch.db";
        Context db;

        public CurrencyRepository()
        {
            db = new Context(DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME));
        }

        public List<Currency> GetItems()
        {
            return db.Currencies.ToList();
        }

        #region Async Methods
        public async Task<bool> AddItemAsync(Currency currency)
        {
            db.Currencies.Add(currency);
            db.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Currency currency)
        {
            var oldItem = db.Currencies.Where((Currency arg) => arg.Id == currency.Id).FirstOrDefault();
            db.Currencies.Remove(oldItem);
            db.Currencies.Add(currency);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id, int userId)
        {
            var oldItem = db.Currencies.Where((Currency arg) => arg.Id == id).FirstOrDefault();
            db.Currencies.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<Currency>> GetItemsAsync(int userId)
        {
            return await Task.FromResult(db.Currencies.ToList());
        }

        public async Task<Currency> GetItemAsync(int id, int userId)
        {
            return await Task.FromResult(db.Currencies.FirstOrDefault(s => s.Id == id));
        }
        #endregion
    }
}
