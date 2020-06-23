using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grossbuch.Models;
using Grossbuch.Server;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Grossbuch.Repositories
{
    class CategoryRepository
    {
        private const string DBFILENAME = "grossbuch.db";
        Context db;
        User User;

        public CategoryRepository(User _user)
        {
            db = new Context(DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME));
            User = _user;
        }

        private async Task GetToken(User _user)
        {
            User newUser = await UserServer.LoginAsync(_user.Login, _user.Password);
            User.Token = newUser.Token;
        }

        public async void UpdateCategoriesDBAsync()
        {
            try
            {
                await GetToken(User);

                var fromServer = await CategoryServer.Get("categories/", User.Token);
                foreach (Category newItem in fromServer)
                {
                    Category item = db.Categories.SingleOrDefault(it => it.Id2 == newItem.Id);
                    if (item == null)
                    {
                        newItem.User = User;
                        await AddItemAsync(newItem);
                    }
                    else
                    {
                        if (item.UpdateTime < newItem.UpdateTime)
                        {
                            if (item.Title != newItem.Title) { item.Title = newItem.Title; }
                            if (item.TotalSum != newItem.TotalSum) { item.TotalSum = newItem.TotalSum; }

                            await UpdateItemAsync(item);
                        }
                        else
                        {
                            if (item.Title != newItem.Title) { newItem.Title = item.Title; }
                            if (item.TotalSum != newItem.TotalSum) { newItem.TotalSum = item.TotalSum; }

                            CategoryServer.Update(newItem, User.Token);
                        }
                    }
                }

                var fromLocal = db.Categories.Include(i => i.User).Where(item => item.User.Id == User.Id).ToList();
                foreach (Category newItem in fromLocal)
                {
                    Category itemFromServer = fromServer.SingleOrDefault(item => item.Id == newItem.Id2);

                    if (itemFromServer == null)
                    {
                        newItem.Id2 = CategoryServer.Add(newItem, User.Token).Id;

                        try { await UpdateItemAsync(newItem); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    else
                    {
                        if (newItem.UpdateTime < itemFromServer.UpdateTime)
                        {
                            if (newItem.Title != itemFromServer.Title) { newItem.Title = itemFromServer.Title; }
                            if (newItem.TotalSum != itemFromServer.TotalSum) { newItem.TotalSum = itemFromServer.TotalSum; }

                            try { await UpdateItemAsync(newItem); }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        else
                        {
                            if (itemFromServer.Title != newItem.Title) { itemFromServer.Title = newItem.Title; }
                            if (itemFromServer.TotalSum != newItem.TotalSum) { itemFromServer.TotalSum = newItem.TotalSum; }

                            CategoryServer.Update(itemFromServer, User.Token);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #region Async Methods
        public async Task<int?> AddItemAsync(Category category)
        {
            Category newCategory = new Category
            {
                Id = 0,
                Id2 = category.Id,
                Title = category.Title,
                TotalSum = category.TotalSum,
                UpdateTime = DateTime.Now
            };

            newCategory.User = (category.User != null) ? db.Users.SingleOrDefault(v => v.Id == category.User.Id || v.Login == category.User.Login) : null;

            var a = db.Categories.Add(newCategory);
            db.SaveChanges();
            int? id = a.Entity.Id;
            return await Task.FromResult(id);
        }

        public async Task<bool> UpdateItemAsync(Category category)
        {
            Category oldItem = db.Categories.Include(acc => acc.User).SingleOrDefault((Category arg) => arg.Id == category.Id);

            if (oldItem == null) { return await Task.FromResult(false); }

            if (oldItem.Id2 != category.Id2) { oldItem.Id2 = category.Id2; }
            if (oldItem.Title != category.Title) { oldItem.Title = category.Title; }
            if (oldItem.TotalSum != category.TotalSum) { oldItem.TotalSum = category.TotalSum; }

            db.Categories.Update(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id, int userId)
        {
            var oldItem = db.Categories.Where((Category arg) => arg.Id == id && arg.User.Id == userId).FirstOrDefault();
            db.Categories.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<Category>> GetItemsAsync(int userId)
        {
            //UpdateCategoriesDBAsync();
            return await Task.FromResult(db.Categories.ToList());
        }

        public async Task<Category> GetItemAsync(int id, int userId)
        {
            return await Task.FromResult(db.Categories.FirstOrDefault(s => s.Id == id && s.User.Id == userId));
        }
        #endregion

        #region Methods
        public void AddItem(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void UpdateItem(Category category)
        {
            var oldItem = db.Categories.Where((Category arg) => arg.Id == category.Id && arg.User.Id == category.User.Id).FirstOrDefault();
            db.Categories.Remove(oldItem);
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void DeleteItem(int id, int userId)
        {
            var oldItem = db.Categories.Where((Category arg) => arg.Id == id && arg.User.Id == userId).FirstOrDefault();
            db.Categories.Remove(oldItem);
            db.SaveChanges();
        }

        public List<Category> GetItems()
        {
            return db.Categories.ToList();
        }
        #endregion
    }
}
