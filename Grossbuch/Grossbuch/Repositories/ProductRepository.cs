using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grossbuch.Models;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Grossbuch.Repositories
{
    public class ProductRepository
    {
        private const string DBFILENAME = "grossbuch.db";
        Context db;
        User User;

        public ProductRepository(User _user)
        {
            db = new Context(DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME));
            User = _user;
        }

        public ProductRepository(Context _db, User _user)
        {
            db = _db;
            User = _user;
        }

        #region Sync Methods
        public List<Product> GetItems()
        {
            return db.Products.ToList();
        }
        #endregion

        #region Async Methods
        public async Task<int?> AddItemAsync(Product product)
        {
            Product newProduct = new Product
            {
                Id = 0,
                Id2 = product.Id,
                Title = product.Title,
                Price = product.Price,
                Count = product.Count,
                TotalSum = product.TotalSum,
                UpdateTime = DateTime.Now
            };

            var a = db.Products.Add(newProduct);
            db.SaveChanges();
            int? id = a.Entity.Id;
            return await Task.FromResult(id);
        }

        public async Task<bool> UpdateItemAsync(Product product)
        {
            Product oldItem = db.Products.Include(acc => acc.User).SingleOrDefault((Product arg) => arg.Id == product.Id && arg.User.Id == product.User.Id);

            if (oldItem == null) { return await Task.FromResult(false); }

            if (oldItem.Id2 != product.Id2) { oldItem.Id2 = product.Id2; }
            if (oldItem.Title != product.Title) { oldItem.Title = product.Title; }
            if (oldItem.Price != product.Price) { oldItem.Price = product.Price; }
            if (oldItem.Count != product.Count) { oldItem.Count = product.Count; }

            db.Products.Update(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = db.Products.Where(arg => arg.Id == id && arg.User.Id == User.Id).FirstOrDefault();
            db.Products.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<Product>> GetItemsAsync()
        {
            return await Task.FromResult(db.Products.Where(item => item.User.Id == User.Id).Include(acc => acc.User).ToList());
        }
        #endregion
    }
}
