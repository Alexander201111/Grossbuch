using Grossbuch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grossbuch.Repositories
{
    public class AimRepository
    {
        private const string DBFILENAME = "grossbuch.db";
        Context db;
        private readonly User User;

        public AimRepository()
        {
            db = new Context(DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME));
        }

        public async void UpdateAccountsDBAsync()
        {
            //List<Operation> fromServer = await OperationServer.Get("categories/", User.Token);

            //foreach (Operation newItem in fromServer)
            //{
            //    Operation item = db.Operations.SingleOrDefault(t => t.Id2 == newItem.Id);
            //    if (item == null)
            //    {
            //        newItem.Id2 = newItem.Id;
            //        newItem.User = User;

            //        db.Operations.Add(newItem);
            //        db.SaveChanges();
            //    }
            //    else
            //    {
            //        if (item.UpdateTime < newItem.UpdateTime)
            //        {
            //            if (item.Title != newItem.Title) { item.Title = newItem.Title; }
            //            if (item.TotalSum != newItem.TotalSum) { item.TotalSum = newItem.TotalSum; }

            //            db.Categories.Update(item);
            //            db.SaveChanges();
            //        }
            //        else
            //        {
            //            if (item.Title != newItem.Title) { newItem.Title = item.Title; }
            //            if (item.TotalSum != newItem.TotalSum) { newItem.TotalSum = item.TotalSum; }

            //            OperationServer.Update(newItem, User.Token);
            //        }
            //    }
            //}

            //foreach (Operation newAcc in db.Categories.Where(item => item.User.Id == User.Id))
            //{
            //    Operation catFromServer = fromServer.SingleOrDefault(item => item.Id == newAcc.Id2);

            //    if (catFromServer == null)
            //    {
            //        OperationServer.Add(newAcc, User.Token);
            //    }
            //    else
            //    {
            //        if (newAcc.UpdateTime < catFromServer.UpdateTime)
            //        {
            //            if (newAcc.Title != catFromServer.Title) { newAcc.Title = catFromServer.Title; }
            //            if (newAcc.TotalSum != catFromServer.TotalSum) { newAcc.TotalSum = catFromServer.TotalSum; }

            //            db.Categories.Update(newAcc);
            //            db.SaveChanges();
            //        }
            //        else
            //        {
            //            if (catFromServer.Title != newAcc.Title) { catFromServer.Title = newAcc.Title; }
            //            if (catFromServer.TotalSum != newAcc.TotalSum) { catFromServer.TotalSum = newAcc.TotalSum; }

            //            OperationServer.Update(catFromServer, User.Token);
            //        }
            //    }
            //}
        }

        public async Task<int?> AddItemAsync(Aim aim)
        {
            Aim newAim = new Aim
            {
                DateStart = aim.DateStart,
                DateFinish = aim.DateFinish,
                Title = aim.Title,
                Type = aim.Type,
                PlannedSum = aim.PlannedSum,
                CurrentSum = aim.CurrentSum,
                Description = aim.Description
            };

            if (aim.User != null)
            {
                newAim.User = (aim.User != null) ? db.Users.SingleOrDefault(v => v.Id == aim.User.Id) : null;
            }

            if (aim.Facility1 != null)
            {
                newAim.Facility1 = (aim.Facility1 != null) ? db.Accounts.SingleOrDefault(v => v.Id == aim.Facility1.Id) : null;
            }
            else
            {
                newAim.Facility2 = (aim.Facility2 != null) ? db.Categories.SingleOrDefault(v => v.Id == aim.Facility2.Id) : null;
            }
            newAim.UpdateTime = DateTime.Now;

            var a = db.Aims.Add(newAim);
            db.SaveChanges();
            int? id = a.Entity.Id;
            return await Task.FromResult(id);
        }

        public async Task<bool> UpdateItemAsync(Aim newAim)
        {
            Aim oldAim = db.Aims.
                Include(op => op.Facility1).
                Include(op => op.Facility2).
                SingleOrDefault(arg => arg.Id == newAim.Id && arg.User.Id == newAim.User.Id);

            if (oldAim == null)
            {
                return await Task.FromResult(false);
            }

            if (newAim.DateStart != oldAim.DateStart) { oldAim.DateStart = newAim.DateStart; }
            if (newAim.DateFinish != oldAim.DateFinish) { oldAim.DateFinish = newAim.DateFinish; }

            if (newAim.Title != oldAim.Title) { oldAim.Title = newAim.Title; }
            if (newAim.Type != oldAim.Type) { oldAim.Type = newAim.Type; }
            if (newAim.PlannedSum != oldAim.PlannedSum) { oldAim.PlannedSum = newAim.PlannedSum; }
            if (newAim.CurrentSum != oldAim.CurrentSum) { oldAim.CurrentSum = newAim.CurrentSum; }
            if (newAim.Description != oldAim.Description) { oldAim.Description = newAim.Description; }
            newAim.UpdateTime = DateTime.Now;

            db.Aims.Update(oldAim);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id, int userId)
        {
            var oldItem = db.Aims.SingleOrDefault(arg => arg.Id == id && arg.User.Id == userId);
            db.Aims.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<Aim>> GetItemsAsync(int userId)
        {
            List<Aim> list = await Task.FromResult(db.Aims.Where(op => op.User.Id == userId).
                        Include(op => op.Facility1).
                        Include(op => op.Facility2).
                        Include(op => op.User).
                        OrderByDescending(x => x.DateStart).ToList());

            //OperationRepository opRepository = new OperationRepository(User);
            //foreach (var aim in list)
            //{
            //    if(aim.Facility1 != null)
            //    {
            //        float sum = await opRepository.GetCurrentSumForAimAsync(aim.Facility1, aim.DateStart, aim.DateFinish);
            //        aim.CurrentSum = sum;
            //    }
            //    else
            //    {
            //        aim.CurrentSum = await opRepository.GetCurrentSumForAimAsync(aim.Facility2, aim.DateStart, aim.DateFinish);
            //    }
            //}

            return await Task.FromResult(list);
        }

        public async Task<Aim> GetItemAsync(int id, int userId)
        {
            return await Task.FromResult(db.Aims.Include(op => op.Facility1).Include(op => op.Facility2).
                        Include(op => op.User).
                        SingleOrDefault(op => op.Id == id && op.User.Id == userId));
        }
    }
}
