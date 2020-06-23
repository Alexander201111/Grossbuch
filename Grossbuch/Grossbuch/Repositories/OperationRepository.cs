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
    public class OperationRepository
    {
        private const string DBFILENAME = "grossbuch.db";
        Context db;
        private User User;

        public OperationRepository(User _user)
        {
            db = new Context(DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME));
            User = _user;
        }

        private async void GetToken(User _user)
        {
            User = await UserServer.LoginAsync(_user.Login, _user.Password);
        }

        public async void UpdateOperationDBAsync(int _id)
        {
            if(User == null)
            {
                User = db.Users.SingleOrDefault(item => item.Id == _id);
                GetToken(User);
            }
            
            try
            {
                var fromServer = await OperationServer.Get("operations/", User.Token);
                foreach (Operation newItem in fromServer)
                {
                    Operation item = db.Operations.Include(op => op.Account).
                            Include(op => op.Category).
                            Include(op => op.Currency).
                            Include(op => op.Purpose).
                            Include(op => op.User).
                            SingleOrDefault(it => it.Id2 == newItem.Id);
                    if (item == null)
                    {
                        newItem.Account = db.Accounts.SingleOrDefault(v => v.Title == newItem.Account.Title);
                        newItem.Category = db.Categories.SingleOrDefault(v => v.Title == newItem.Category.Title);
                        newItem.Currency = db.Currencies.SingleOrDefault(v => v.Title == newItem.Currency.Title);
                        newItem.Purpose = db.Accounts.SingleOrDefault(v => v.Title == newItem.Purpose.Title);
                        newItem.User = User;
                        newItem.Performed = false;
                        await AddItemAsync(newItem);
                    }
                    else
                    {
                        if (item.UpdateTime < newItem.UpdateTime)
                        {
                            if (item.Summ != newItem.Summ) { item.Summ = newItem.Summ; }
                            if (item.Description != newItem.Description) { item.Description = newItem.Description; }

                            await UpdateItemAsync(item);
                        }
                        else
                        {
                            if (item.Summ != newItem.Summ) { newItem.Summ = item.Summ; }
                            if (item.Description != newItem.Description) { newItem.Description = item.Description; }

                            OperationServer.Update(newItem, User.Token);
                        }
                    }
                }

                var fromLocal = await GetItemsAsync(User.Id);
                foreach (Operation newItem in fromLocal)
                {
                    Operation itemFromServer = fromServer.SingleOrDefault(item => item.Id == newItem.Id2);

                    if (itemFromServer == null)
                    {
                        newItem.Id2 = OperationServer.Add(newItem, User.Token).Id;

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
                            if (newItem.Summ != itemFromServer.Summ) { newItem.Summ = itemFromServer.Summ; }
                            if (newItem.Description != itemFromServer.Description) { newItem.Description = itemFromServer.Description; }

                            try { await UpdateItemAsync(newItem); }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        else
                        {
                            if (newItem.Summ != itemFromServer.Summ) { itemFromServer.Summ = newItem.Summ; }
                            if (newItem.Description != itemFromServer.Description) { itemFromServer.Description = newItem.Description; }

                            OperationServer.Update(itemFromServer, User.Token);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public List<Operation> GetItems()
        {
            return db.Operations.ToList();
        }

        public async Task<int?> AddItemAsync(Operation operation, int _userId = -1)
        {
            if (_userId != -1 && User == null)
            {
                User = db.Users.SingleOrDefault(item => item.Id == _userId);
            }

            try
            {
                Operation newOperation = new Operation
                {
                    Date = operation.Date,
                    Summ = operation.Summ,
                    Type = operation.Type,
                    Description = operation.Description,
                    Images = operation.Images,
                    Performed = operation.Performed
                };

                newOperation.Account = (operation.Account != null) ? db.Accounts.SingleOrDefault(v => v.Id == operation.Account.Id) : null;
                newOperation.Category = (operation.Category != null) ? db.Categories.SingleOrDefault(v => v.Id == operation.Category.Id) : null;
                newOperation.Currency = (operation.Currency != null) ? db.Currencies.SingleOrDefault(v => v.Id == operation.Currency.Id) : null;
                newOperation.Purpose = (operation.Purpose != null) ? db.Accounts.SingleOrDefault(v => v.Id == operation.Purpose.Id) : null;

                if (_userId == -1) { newOperation.User = (operation.User != null) ? db.Users.SingleOrDefault(v => v.Id == operation.User.Id) : null; }
                else { newOperation.User = db.Users.SingleOrDefault(v => v.Id == _userId); }

                bool aa = false, bb = false;

                if (operation.Type == 1) //Приход
                {
                    Account newPurpose = newOperation.Purpose;
                    newPurpose.Balance = newPurpose.Balance + newOperation.Summ;
                    AccountRepository accRep = new AccountRepository(db, User);
                    aa = await accRep.UpdateItemAsync(newPurpose);
                    bb = true;
                }
                if ((operation.Type == 2 || operation.Type == 3) && (newOperation.Account != null && newOperation.Purpose != null)) //Расход || Перевод
                {
                    Account newAccount = newOperation.Account;
                    Account newPurpose = newOperation.Purpose;

                    newAccount.Balance = newAccount.Balance - newOperation.Summ;
                    newPurpose.Balance = newPurpose.Balance + newOperation.Summ;

                    AccountRepository accRep = new AccountRepository(db, User);

                    aa = await accRep.UpdateItemAsync(newAccount);
                    bb = await accRep.UpdateItemAsync(newPurpose);
                }

                var a = db.Operations.Add(newOperation);
                db.SaveChanges();
                int? id = a.Entity.Id;
                return await Task.FromResult(id);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<bool> UpdateItemAsync(Operation newOperation)
        {
            Operation oldOperation = db.Operations.
                Include(op => op.Account).
                Include(op => op.Category).
                Include(op => op.Currency).
                Include(op => op.Purpose).
                Include(op => op.User).
                SingleOrDefault(arg => arg.Id == newOperation.Id && arg.User.Id == newOperation.User.Id);

            if (oldOperation == null)
            {
                return await Task.FromResult(false);
            }

            if (newOperation.Date != oldOperation.Date) { oldOperation.Date = newOperation.Date; }
            if (newOperation.Summ != oldOperation.Summ)
            {
                oldOperation.Summ = newOperation.Summ;
            }
            if (newOperation.Type != oldOperation.Type) { oldOperation.Type = newOperation.Type; }
            if (newOperation.Description != oldOperation.Description) { oldOperation.Description = newOperation.Description; }

            db.Operations.Update(oldOperation);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id, int userId)
        {
            var oldItem = db.Operations.Where((Operation arg) => arg.Id == id && arg.User.Id == userId).FirstOrDefault();
            db.Operations.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<Operation>> GetItemsAsync(int userId, object filter = null)
        {
            //UpdateOperationDBAsync(userId);
            if (filter != null)
            {
                if (filter is Account accountForFilter)
                {
                    return await Task.FromResult(db.Operations.Where(op => 
                        op.User.Id == userId && ((op.Account.Id == accountForFilter.Id && op.Account.IsAccount == true) || (op.Purpose.Id == accountForFilter.Id && op.Purpose.IsAccount == false))).
                        Include(op => op.Account).
                        Include(op => op.Category).
                        Include(op => op.Currency).
                        Include(op => op.Purpose).
                        Include(op => op.User).
                        OrderByDescending(x => x.Date).ToList());
                }
                else
                {
                    if (filter is Category categoryForFilter)
                    {
                        return await Task.FromResult(db.Operations.Where(op => op.User.Id == userId && op.Category.Id == categoryForFilter.Id).
                        Include(op => op.Account).
                        Include(op => op.Category).
                        Include(op => op.Currency).
                        Include(op => op.Purpose).
                        Include(op => op.User).
                        OrderByDescending(x => x.Date).ToList());
                    }
                    else
                    {
                        return await Task.FromResult(new List<Operation>());
                    }
                }
            }
            else
            {
                return await Task.FromResult(db.Operations.Where(op => op.User.Id == userId).
                    Include(op => op.Account).
                    Include(op => op.Category).
                    Include(op => op.Currency).
                    Include(op => op.Purpose).
                    Include(op => op.User).
                    OrderByDescending(x => x.Date).ToList());
            }
        }

        public async Task<List<Operation>> GetItemsSumAsync(int userId, string search, DateTime dateStart, DateTime dateFinish)
        {
            return await Task.FromResult(db.Operations.Where(op =>
                        op.User.Id == userId &&
                        (op.Account.Title.Contains(search) || op.Category.Title.Contains(search) || op.Purpose.Title.Contains(search) || op.Description.Contains(search) || op.Summ.ToString().Contains(search)) &&
                        op.Date >= dateStart && op.Date <= dateFinish).
                        Include(op => op.Account).
                        Include(op => op.Category).
                        Include(op => op.Currency).
                        Include(op => op.Purpose).
                        Include(op => op.User).
                        OrderByDescending(x => x.Date).ToList());
        }

        public async Task<Operation> GetItemAsync(int id, int userId)
        {
            return await Task.FromResult(db.Operations.FirstOrDefault(s => s.Id == id && s.User.Id == userId));
        }

        public async Task<float> GetBalanceAsync(DateTime _dateStart, DateTime _dateFinish)
        {
            List<Operation> listIncome = await Task.FromResult(db.Operations.Where(op =>                            //1
                        op.Type == 1 && op.Date >= _dateStart && op.Date <= _dateFinish && op.User.Id == User.Id).
                        Include(op => op.Account).
                        Include(op => op.Category).
                        Include(op => op.Currency).
                        Include(op => op.Purpose).
                        Include(op => op.User).
                        OrderByDescending(x => x.Date).ToList());
            float balance = 0;
            if(listIncome.Count != 0)                                                                               //2
            {
                foreach (var income in listIncome)                                                                  //3
                {
                    if(income.Date > _dateStart && income.Date < _dateFinish)                                       //4
                    {
                        if (income.Type == 1)                                                                       //5
                        {
                            balance = balance + (income.Summ * income.Currency.Coefficient);                        //6
                            continue;                                                                               //7
                        }
                        if (income.Type == 2)                                                                       //8
                        {
                            balance = balance - (income.Summ * income.Currency.Coefficient);                        //9
                            continue;                                                                               //10
                        }
                    }
                }                                                                                                   //11
            }

            return await Task.FromResult(balance);                                                                  //12
        }

        public async Task<float> GetIncomeAsync(DateTime _dateStart, DateTime _dateFinish)
        {
            List<Operation> listIncome = await Task.FromResult(db.Operations.Where(op =>
                        op.Type == 1 && op.Date >= _dateStart && op.Date <= _dateFinish /*&& op.User.Id == User.Id*/).
                        Include(op => op.Account).
                        Include(op => op.Category).
                        Include(op => op.Currency).
                        Include(op => op.Purpose).
                        Include(op => op.User).
                        OrderByDescending(x => x.Date).ToList());
            float totalSum = 0;
            foreach(var income in listIncome)
            {
                totalSum = totalSum + income.Summ;
            }

            return await Task.FromResult(totalSum);
        }

        public async Task<float> GetCostsAsync(DateTime _dateStart, DateTime _dateFinish)
        {
            List<Operation> listIncome = await Task.FromResult(db.Operations.Where(op =>
                        op.Type == 2 && op.Date >= _dateStart && op.Date <= _dateFinish /*&& op.User.Id == User.Id*/).
                        Include(op => op.Account).
                        Include(op => op.Category).
                        Include(op => op.Currency).
                        Include(op => op.Purpose).
                        Include(op => op.User).
                        OrderByDescending(x => x.Date).ToList());
            float totalSum = 0;
            foreach (var income in listIncome)
            {
                totalSum = totalSum + income.Summ;
            }

            return await Task.FromResult(totalSum);
        }

        public async Task<float> GetCurrentSumForAimAsync(Account account, DateTime _dateStart, DateTime _dateFinish)
        {
            List<Operation> listIncome = await Task.FromResult(db.Operations.Where(op =>
                        op.Type == 2 && op.Date >= _dateStart && op.Date <= _dateFinish && (op.Account.Id == account.Id || op.Purpose.Id == account.Id) && op.User.Id == User.Id).
                        Include(op => op.Account).
                        Include(op => op.Category).
                        Include(op => op.Currency).
                        Include(op => op.Purpose).
                        Include(op => op.User).
                        OrderByDescending(x => x.Date).ToList());
            float totalSum = 0;
            foreach (var income in listIncome)
            {
                totalSum = totalSum + income.Summ;
            }

            return await Task.FromResult(totalSum);
        }

        public async Task<float> GetCurrentSumForAimAsync(Category category, DateTime _dateStart, DateTime _dateFinish)
        {
            List<Operation> listIncome = await Task.FromResult(db.Operations.Where(op =>
                        op.Type == 2 && op.Date >= _dateStart && op.Date <= _dateFinish && op.Category.Id == category.Id && op.User.Id == User.Id).
                        Include(op => op.Account).
                        Include(op => op.Category).
                        Include(op => op.Currency).
                        Include(op => op.Purpose).
                        Include(op => op.User).
                        OrderByDescending(x => x.Date).ToList());
            float totalSum = 0;
            foreach (var income in listIncome)
            {
                totalSum = totalSum + income.Summ;
            }

            return await Task.FromResult(totalSum);
        }
    }
}
