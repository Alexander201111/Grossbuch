using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grossbuch.Models;
using Grossbuch.Server;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Grossbuch.Repositories
{
    public class AccountRepository
    {
        private const string DBFILENAME = "grossbuch.db";
        Context db;
        User User;

        public AccountRepository(User _user)
        {
            db = new Context(DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME));
            User = _user;
        }

        private async void GetToken(User _user)
        {
            User = await UserServer.LoginAsync(_user.Login, _user.Password);
        }

        public async void UpdateAccountsDBAsync(string path)
        {
            try
            {
                GetToken(User);
                bool isAccounts = true;
                if (path != "accounts/") { isAccounts = false; }

                var fromServer = await AccountServer.Get(path, User.Token);
                foreach (Account newItem in fromServer)
                {
                    Account item = db.Accounts.SingleOrDefault(it => it.Id2 == newItem.Id);
                    if (item == null)
                    {
                        newItem.IsAccount = isAccounts;
                        newItem.User = User;
                        await AddItemAsync(newItem);
                    }
                    else
                    {
                        if (item.UpdateTime < newItem.UpdateTime)
                        {
                            if (item.Title != newItem.Title) { item.Title = newItem.Title; }
                            if (item.Balance != newItem.Balance) { item.Balance = newItem.Balance; }
                            if (item.TotalSum != newItem.TotalSum) { item.TotalSum = newItem.TotalSum; }

                            await UpdateItemAsync(item);
                        }
                        else
                        {
                            if (item.Title != newItem.Title) { newItem.Title = item.Title; }
                            if (item.Balance != newItem.Balance) { newItem.Balance = item.Balance; }
                            if (item.TotalSum != newItem.TotalSum) { newItem.TotalSum = item.TotalSum; }

                            AccountServer.Update(newItem, User.Token);
                        }
                    }
                }

                var fromLocal = db.Accounts.Where(item => item.User.Id == User.Id && item.IsAccount == isAccounts).Include(i => i.User).ToList();
                foreach (Account newItem in fromLocal)
                {
                    Account itemFromServer = fromServer.SingleOrDefault(item => item.Id == newItem.Id2);

                    if (itemFromServer == null)
                    {
                        newItem.Id2 = AccountServer.Add(newItem, User.Token).Id;

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
                            if (newItem.Balance != itemFromServer.Balance) { newItem.Balance = itemFromServer.Balance; }
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
                            if (itemFromServer.Balance != newItem.Balance) { itemFromServer.Balance = newItem.Balance; }
                            if (itemFromServer.TotalSum != newItem.TotalSum) { itemFromServer.TotalSum = newItem.TotalSum; }

                            AccountServer.Update(itemFromServer, User.Token);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public AccountRepository(Context _db, User _user)
        {
            db = _db;
            User = _user;
        }

        #region Sync Methods
        public List<Account> GetAccounts()
        {
            return db.Accounts.Where(item => item.IsAccount == true).Include(acc => acc.User).ToList();
        }

        public List<Account> GetPurposes()
        {
            return db.Accounts.Where(item => item.IsAccount == false).Include(acc => acc.User).ToList();
        }
        #endregion

        #region Async Methods
        public async Task<int?> AddItemAsync(Account account)
        {
            Account newAccount = new Account
            {
                Id = 0,
                Id2 = account.Id,
                Title = account.Title,
                Balance = account.Balance,
                PlannedSum = account.PlannedSum,
                TotalSum = account.TotalSum,
                IsAccount = account.IsAccount,
                UpdateTime = DateTime.Now
            };

            newAccount.User = (account.User != null) ? db.Users.SingleOrDefault(v => v.Id == account.User.Id) : null;

            var a = db.Accounts.Add(newAccount);
            db.SaveChanges();
            int? id = a.Entity.Id;
            return await Task.FromResult(id);
        }

        public async Task<bool> UpdateItemAsync(Account account)
        {
            Account oldItem = db.Accounts.Include(acc => acc.User).SingleOrDefault((Account arg) => arg.Id == account.Id && arg.User.Id == account.User.Id);

            if (oldItem == null) { return await Task.FromResult(false); }

            if (oldItem.Id2 != account.Id2) { oldItem.Id2 = account.Id2; }
            if (oldItem.Title != account.Title) { oldItem.Title = account.Title; }
            if (oldItem.Balance != account.Balance) { oldItem.Balance = account.Balance; }
            if (oldItem.PlannedSum != account.PlannedSum) { oldItem.PlannedSum = account.PlannedSum; }

            db.Accounts.Update(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = db.Accounts.Where(arg => arg.Id == id && arg.User.Id == User.Id).FirstOrDefault();
            db.Accounts.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            //try { UpdateAccountsDBAsync("accounts/"); }
            //catch { }
            return await Task.FromResult(db.Accounts.Where(item => item.IsAccount == true && item.User.Id == User.Id).Include(acc => acc.User).ToList());
        }

        public async Task<List<Account>> GetPurposesAsync()
        {
            //try { UpdateAccountsDBAsync("purposes/"); }
            //catch { }
            return await Task.FromResult(db.Accounts.Where(item => item.IsAccount == false && item.User.Id == User.Id).Include(acc => acc.User).ToList());
        }

        public async Task<float> GetTotalActiviesAsync()
        {
            List<Account> accounts = await Task.FromResult(db.Accounts.Where(op =>
                        op.IsAccount == true /*&& op.User.Id == User.Id*/).
                        Include(op => op.User).ToList());

            float totalSum = 0;
            foreach (var acc in accounts)
            {
                totalSum = totalSum + acc.Balance;
            }

            return await Task.FromResult(totalSum);
        }
        #endregion
    }
}
