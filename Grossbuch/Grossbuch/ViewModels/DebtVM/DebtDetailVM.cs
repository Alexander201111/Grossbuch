using System.Collections.Generic;
using System.Threading.Tasks;
using Grossbuch.Models;
using Grossbuch.Repositories;

namespace Grossbuch.ViewModels
{
    public class DebtDetailVM : BaseViewModel
    {
        private Debt debt;
        public Debt Debt { get { return debt; } set { debt = value; OnPropertyChanged(); } }

        public User User;

        public bool adding = true;

        public DebtRepository repository;

        public List<string> ListForPickerType { get; set; }

        public DebtDetailVM(Debt newDebt, User _user, DebtRepository _repository = null)
        {
            Title = newDebt?.Title.ToString();
            repository = _repository;
            User = _user;
            ListForPickerType = new List<string>
            {
                "Ежемесячный возврат части кредита",
                "Аннуитетный платеж",
                "Единовременный возврат кредита"
            };

            if (newDebt == null)
            {
                Debt = new Debt(_user);
            }
            else
            {
                Debt = newDebt;
                adding = false;
            }
        }

        public async Task SaveAsync()
        {
            if (adding == true)
            {
                Debt.Id = (int)await repository.AddItemAsync(Debt);
            }
            else
            {
                await repository.UpdateItemAsync(Debt);
            }
        }
    }
}
