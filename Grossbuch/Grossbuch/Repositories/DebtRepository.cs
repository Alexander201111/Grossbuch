using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grossbuch.Models;
using Grossbuch.Server;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Grossbuch.Repositories
{
    public class DebtRepository
    {
        private const string DBFILENAME = "grossbuch.db";
        Context db;
        User User;

        public DebtRepository(User _user)
        {
            db = new Context(DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME));
            User = _user;
        }

        //private async void GetToken(User _user)
        //{
        //    User = await UserServer.LoginAsync(_user.Login, _user.Password);
        //}

        //public async void UpdateDebtsDBAsync(string path)
        //{
        //    try
        //    {
        //        GetToken(User);
        //        bool isDebts = true;
        //        if (path != "debts/") { isDebts = false; }

        //        var fromServer = await DebtServer.Get(path, User.Token);
        //        foreach (Debt newItem in fromServer)
        //        {
        //            Debt item = db.Debts.SingleOrDefault(it => it.Id2 == newItem.Id);
        //            if (item == null)
        //            {
        //                newItem.IsDebt = isDebts;
        //                newItem.User = User;
        //                await AddItemAsync(newItem);
        //            }
        //            else
        //            {
        //                if (item.UpdateTime < newItem.UpdateTime)
        //                {
        //                    if (item.Title != newItem.Title) { item.Title = newItem.Title; }
        //                    if (item.Balance != newItem.Balance) { item.Balance = newItem.Balance; }
        //                    if (item.TotalSum != newItem.TotalSum) { item.TotalSum = newItem.TotalSum; }

        //                    await UpdateItemAsync(item);
        //                }
        //                else
        //                {
        //                    if (item.Title != newItem.Title) { newItem.Title = item.Title; }
        //                    if (item.Balance != newItem.Balance) { newItem.Balance = item.Balance; }
        //                    if (item.TotalSum != newItem.TotalSum) { newItem.TotalSum = item.TotalSum; }

        //                    DebtServer.Update(newItem, User.Token);
        //                }
        //            }
        //        }

        //        var fromLocal = db.Debts.Where(item => item.User.Id == User.Id && item.IsDebt == isDebts).Include(i => i.User).ToList();
        //        foreach (Debt newItem in fromLocal)
        //        {
        //            Debt itemFromServer = fromServer.SingleOrDefault(item => item.Id == newItem.Id2);

        //            if (itemFromServer == null)
        //            {
        //                newItem.Id2 = DebtServer.Add(newItem, User.Token).Id;

        //                try { await UpdateItemAsync(newItem); }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                }
        //            }
        //            else
        //            {
        //                if (newItem.UpdateTime < itemFromServer.UpdateTime)
        //                {
        //                    if (newItem.Title != itemFromServer.Title) { newItem.Title = itemFromServer.Title; }
        //                    if (newItem.Balance != itemFromServer.Balance) { newItem.Balance = itemFromServer.Balance; }
        //                    if (newItem.TotalSum != itemFromServer.TotalSum) { newItem.TotalSum = itemFromServer.TotalSum; }

        //                    try { await UpdateItemAsync(newItem); }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e);
        //                    }
        //                }
        //                else
        //                {
        //                    if (itemFromServer.Title != newItem.Title) { itemFromServer.Title = newItem.Title; }
        //                    if (itemFromServer.Balance != newItem.Balance) { itemFromServer.Balance = newItem.Balance; }
        //                    if (itemFromServer.TotalSum != newItem.TotalSum) { itemFromServer.TotalSum = newItem.TotalSum; }

        //                    DebtServer.Update(itemFromServer, User.Token);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        public DebtRepository(Context _db, User _user)
        {
            db = _db;
            User = _user;
        }

        #region Sync Methods
        //public List<Debt> GetDebts()
        //{
        //    return db.Debts.Where(item => item.IsDebt == true).Include(acc => acc.User).ToList();
        //}

        //public List<Debt> GetPurposes()
        //{
        //    return db.Debts.Where(item => item.IsDebt == false).Include(acc => acc.User).ToList();
        //}
        #endregion

        #region Async Methods
        public async Task<int?> AddItemAsync(Debt debt)
        {
            Debt newDebt = new Debt
            {
                Id = 0,
                Id2 = debt.Id,
                Title = debt.Title,
                Date = debt.Date,
                Term = debt.Term,
                Type = debt.Type,
                Sum = debt.Sum,
                Rate = debt.Rate,
                MounthlyPayment = debt.MounthlyPayment,
                TotalSum = debt.TotalSum,
                IsClosed = debt.IsClosed,
                UpdateTime = DateTime.Now
            };

            newDebt.User = (debt.User != null) ? db.Users.SingleOrDefault(v => v.Id == debt.User.Id) : null;

            var a = db.Debts.Add(newDebt);
            db.SaveChanges();
            int? id = a.Entity.Id;
            return await Task.FromResult(id);
        }

        public async Task<bool> UpdateItemAsync(Debt debt)
        {
            Debt oldItem = db.Debts.Include(acc => acc.User).SingleOrDefault((Debt arg) => arg.Id == debt.Id && arg.User.Id == debt.User.Id);

            if (oldItem == null) { return await Task.FromResult(false); }

            if (oldItem.Id2 != debt.Id2) { oldItem.Id2 = debt.Id2; }
            if (oldItem.Title != debt.Title) { oldItem.Title = debt.Title; }
            if (oldItem.Date != debt.Date) { oldItem.Date = debt.Date; }
            if (oldItem.Term != debt.Term) { oldItem.Term = debt.Term; }
            if (oldItem.Sum != debt.Sum) { oldItem.Sum = debt.Sum; }
            if (oldItem.Type != debt.Type) { oldItem.Type = debt.Type; }
            if (oldItem.Rate != debt.Rate) { oldItem.Rate = debt.Rate; }
            if (oldItem.MounthlyPayment != debt.MounthlyPayment) { oldItem.MounthlyPayment = debt.MounthlyPayment; }
            if (oldItem.TotalSum != debt.TotalSum) { oldItem.TotalSum = debt.TotalSum; }
            if (oldItem.IsClosed != debt.IsClosed) { oldItem.IsClosed = debt.IsClosed; }
            oldItem.UpdateTime = DateTime.Now;

            db.Debts.Update(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = db.Debts.Where(arg => arg.Id == id && arg.User.Id == User.Id).FirstOrDefault();
            db.Debts.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<Debt>> GetItemsAsync(int _userId = 0)
        {
            //try { UpdateDebtsDBAsync("debts/"); }
            //catch { }
            if (User != null) { return await Task.FromResult(db.Debts.Where(item => item.User.Id == User.Id).Include(acc => acc.User).ToList()); }
            else { return await Task.FromResult(db.Debts.Where(item => item.User.Id == _userId).Include(acc => acc.User).ToList()); }
        }

        //public async Task<float> GetTotalActiviesAsync()
        //{
        //    List<Debt> debts = await Task.FromResult(db.Debts.Where(op =>
        //                op.IsDebt == true /*&& op.User.Id == User.Id*/).
        //                Include(op => op.User).ToList());

        //    float totalSum = 0;
        //    foreach (var acc in debts)
        //    {
        //        totalSum = totalSum + acc.Balance;
        //    }

        //    return await Task.FromResult(totalSum);
        //}
        #endregion
    }
}
