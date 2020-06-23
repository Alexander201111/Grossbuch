using System.Collections.Generic;
using System.Threading.Tasks;
using Grossbuch.Models;
using Grossbuch.Repositories;

namespace Grossbuch.ViewModels
{
    public class AimDetailVM : BaseViewModel
    {
        private Aim aim;
        public Aim Aim { get { return aim; } set { aim = value; OnPropertyChanged(); } }

        public User User;

        public bool adding = true;

        public AimRepository repository;
        AccountRepository accountRep;
        CategoryRepository categoryRep;

        public List<string> ListForPickerType { get; set; }

        public List<object> facilities;
        public List<object> Facilities { get { return facilities; } set { facilities = value; OnPropertyChanged(); } }

        public AimDetailVM(Aim newAim, User _user, AimRepository _repository = null)
        {
            Title = newAim?.Title.ToString();
            repository = _repository;
            User = _user;

            ListForPickerType = new List<string> { "Накопление", "Трата"};

            accountRep = new AccountRepository(User);
            categoryRep = new CategoryRepository(User);
            Facilities = new List<object>();
            foreach (var item in accountRep.GetAccounts()) { Facilities.Add(item); }
            foreach (var item in accountRep.GetPurposes()) { Facilities.Add(item); }
            foreach (var item in categoryRep.GetItems()) { Facilities.Add(item); }

            if (newAim == null)
            {
                Aim = new Aim(_user);
            }
            else
            {
                Aim = newAim;
                adding = false;
            }
        }

        public async Task SaveAsync()
        {
            if (adding == true)
            {
                Aim.Id = (int)await repository.AddItemAsync(Aim);
            }
            else
            {
                await repository.UpdateItemAsync(Aim);
            }
        }

        public void ChangeFacilities(int t)
        {
            if (t == 1)
            {
                Facilities = new List<object>();
                foreach (var item in accountRep.GetAccounts()) { Facilities.Add(item); }
            }
            if (t == 2)
            {
                Facilities = new List<object>();
                foreach (var item in accountRep.GetAccounts()) { Facilities.Add(item); }
                foreach (var item in accountRep.GetPurposes()) { Facilities.Add(item); }
                foreach (var item in categoryRep.GetItems()) { Facilities.Add(item); }
            }
        }
    }
}
