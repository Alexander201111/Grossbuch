using System;
using System.Collections.Generic;
using System.Text;

namespace Grossbuch.Models
{
    public enum MenuItemType
    {
        Operations,
        Analitics,
        Accounts,
        Categories,
        Currencies,
        Debts,
        Products,
        ProductSets,
        Purposes,
        Tasks,
        Settings,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
