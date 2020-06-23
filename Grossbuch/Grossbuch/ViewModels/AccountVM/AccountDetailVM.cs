using Grossbuch.Models;
using Grossbuch.Repositories;
using System.Threading.Tasks;

namespace Grossbuch.ViewModels
{
    public class AccountDetailVM : BaseViewModel
    {
        private Account account;
        public Account Account { get { return account; } set { account = value; OnPropertyChanged(); } }

        public User User;

        public bool adding = true;
        public AccountRepository accountRep;

        public AccountDetailVM(Account _account, User _user, AccountRepository _repository = null)
        {
            Title = account?.Title;
            accountRep = _repository ?? new AccountRepository(_user);
            User = _user;

            if (_account == null)
            {
                Account = new Account("", 0, 0, 0, User);
                adding = true;
            }
            else
            {
                Account = _account;
                adding = false;
            }
        }

        public AccountDetailVM(bool _isAccount, User _user, AccountRepository _repository = null)
        {
            Title = account?.Title;
            accountRep = _repository ?? new AccountRepository(_user);
            User = _user;

            Account = new Account("", 0, 0, 0, User, _isAccount);
            adding = true;
        }

        public async Task SaveAsync()
        {
            if (adding == true)
            {
                await accountRep.AddItemAsync(Account);
            }
            else
            {
                await accountRep.UpdateItemAsync(Account);
            }
        }
    }
}
