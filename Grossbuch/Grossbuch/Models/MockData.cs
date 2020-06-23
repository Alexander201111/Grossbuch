using System;
using System.Collections.Generic;
using System.Text;
using Grossbuch.Models;

namespace Grossbuch.Models
{
    class MockData
    {
        private readonly List<Account> Accounts = new List<Account>();
        private readonly List<Account> Purposes = new List<Account>();
        private readonly List<Category> Categories = new List<Category>();
        private readonly List<Currency> Currencies = new List<Currency>();
        private readonly List<Operation> Operations = new List<Operation>();
        private readonly List<User> Users = new List<User>();

        public MockData() { }

        public static void Seed(Context context)
        {
            #region User
            var Users = new User[]
            {
                new User("user1", "password1")
            };

            for (int i = 0; i < Users.Length; i++)
            {
                context.Users.Add(Users[i]);
            }

            context.SaveChanges();
            #endregion

            //#region Account
            //var Accounts = new Account[]
            //{
            //    new Account("Cash", 1000, 0, 0, Users[0]),
            //    new Account("Visa", 2000, 0, 0, Users[0]),
            //    new Account("MIR", 3000, 0, 0, Users[0])
            //};

            //for (int i = 0; i < Accounts.Length; i++)
            //{
            //    context.Accounts.Add(Accounts[i]);
            //}

            //context.SaveChanges();
            //#endregion

            //#region UserAccount
            ////var UserAccounts = new UserAccount[]
            ////{
            ////    new UserAccount(Users[0], Accounts[0]),
            ////    new UserAccount(Users[0], Accounts[1]),
            ////    new UserAccount(Users[0], Accounts[2]),
            ////};

            ////for (int i = 0; i < UserAccounts.Length; i++)
            ////{
            ////    context.UserAccounts.Add(UserAccounts[i]);
            ////}

            ////context.SaveChanges();
            //#endregion

            //#region Category
            //var Categories = new Category[]
            //{
            //    new Category("Home", Users[0]),
            //    new Category("Study", Users[0]),
            //};

            //for (int i = 0; i < Categories.Length; i++)
            //{
            //    context.Categories.Add(Categories[i]);
            //}

            //context.SaveChanges();
            //#endregion

            #region Currency
            var Currencies = new Currency[]
            {
                new Currency("Rub", "Рубли", 1),
                new Currency("USD", "Доллар", 68),
                new Currency("EUR", "Евро", 75)
            };

            for (int i = 0; i < Currencies.Length; i++)
            {
                context.Currencies.Add(Currencies[i]);
            }

            context.SaveChanges();
            #endregion

            //#region Debt
            ////var Debts = new Debt[]
            ////{
            ////    new Debt(null, true, 500, Users[0], DateTime.Now, DateTime.Now)
            ////};

            ////for (int i = 0; i < Debts.Length; i++)
            ////{
            ////    context.Debts.Add(Debts[i]);
            ////}

            ////context.SaveChanges();
            //#endregion

            //#region Purpose
            //var Purposes = new Account[]
            //{
            //    new Account("Magnit", 0, 0, 0, Users[0], false),
            //    new Account("Lenta", 0, 0, 0, Users[0], false),
            //    new Account("Kak raz", 0, 0, 0, Users[0], false)
            //};

            //for (int i = 0; i < Purposes.Length; i++)
            //{
            //    context.Accounts.Add(Purposes[i]);
            //}

            //context.SaveChanges();
            //#endregion

            //#region Operation
            //var Operations = new Operation[]
            //{
            //    new Operation(DateTime.Now, 150, 2, "Buy bread and milk in magnit", Categories[0], Currencies[0], Accounts[1], Purposes[0], Users[0]),
            //    new Operation(DateTime.Now, 30, 2, "Buy pen in lenta", Categories[1], Currencies[0], Accounts[2], Purposes[1], Users[0]),
            //};

            //for (int i = 0; i < Operations.Length; i++)
            //{
            //    context.Operations.Add(Operations[i]);
            //    context.SaveChanges();
            //}

            //context.SaveChanges();
            //#endregion

            //#region Product
            ////var Products = new Product[]
            ////{
            ////    new Product("Bread", 20, "White Bread", 2, Users[0]),
            ////    new Product("Milk", 50, "Milk Prostokvashino", 1, Users[0])
            ////};

            ////for (int i = 0; i < Products.Length; i++)
            ////{
            ////    context.Products.Add(Products[i]);
            ////}

            ////context.SaveChanges();
            //#endregion

            //#region OperationProduct
            ////var OperationProducts = new OperationProduct[]
            ////{
            ////    new OperationProduct(Operations[0], Products[0]),
            ////    new OperationProduct(Operations[0], Products[1])
            ////};

            ////for (int i = 0; i < OperationProducts.Length; i++)
            ////{
            ////    context.OperationProducts.Add(OperationProducts[i]);
            ////}

            ////context.SaveChanges();
            //#endregion

            //#region ProductSet
            ////var ProductSets = new ProductSet[]
            ////{
            ////    new ProductSet("Home everyday", Users[0])
            ////};

            ////for (int i = 0; i < ProductSets.Length; i++)
            ////{
            ////    context.ProductSets.Add(ProductSets[i]);
            ////}

            ////context.SaveChanges();
            //#endregion

            //#region ProductProductSet
            ////var ProductProductSets = new ProductProductSet[]
            ////{
            ////    new ProductProductSet(Products[0], ProductSets[0]),
            ////    new ProductProductSet(Products[1], ProductSets[0])
            ////};

            ////for (int i = 0; i < ProductProductSets.Length; i++)
            ////{
            ////    context.ProductProductSets.Add(ProductProductSets[i]);
            ////}

            ////context.SaveChanges();
            //#endregion
        }

    }
}
