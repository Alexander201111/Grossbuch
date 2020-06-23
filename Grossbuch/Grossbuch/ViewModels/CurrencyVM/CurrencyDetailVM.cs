using Grossbuch.Models;

namespace Grossbuch.ViewModels
{
    public class CurrencyDetailVM : BaseViewModel
    {
        public Currency Currency { get; set; }
        public CurrencyDetailVM(Currency currency = null)
        {
            Title = currency?.Title;
            Currency = currency;
        }
    }
}
