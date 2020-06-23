using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Grossbuch.Models;
using Grossbuch.Repositories;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Grossbuch.ViewModels
{
    public class OperationDetailVM : BaseViewModel
    {
        public class ImageItem
        {
            public string ImageUrl { get; set; }

            public ImageItem(string _path) { ImageUrl = _path; }
        }

        private Operation operation;
        public Operation Operation { get { return operation; } set { operation = value; OnPropertyChanged(); } }

        private List<ImageItem> destinations;
        public List<ImageItem> Destinations { get { return destinations; } set { destinations = value; OnPropertyChanged(); } }

        public User User;

        public bool adding = true;
        public OperationRepository opRepository;
        private AccountRepository accountRep;

        public List<string> ListForPickerType { get; set; }

        #region Properties
        private List<Account> account;
        public List<Account> Accounts { get { return account; } set { account = value; OnPropertyChanged(); } }

        private List<Account> purposes;
        public List<Account> Purposes { get { return purposes; } set { purposes = value; OnPropertyChanged(); } }

        public List<Category> Categories { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<User> Users { get; set; }
        #endregion

        public OperationDetailVM(Operation newOperation, User _user, OperationRepository _opRepository = null)
        {
            Title = newOperation?.Date.ToString();
            opRepository = _opRepository;
            User = _user;
            accountRep = new AccountRepository(User);
            Destinations = new List<ImageItem>();
            if(newOperation != null && !String.IsNullOrEmpty(newOperation.Images))
            {
                foreach (var im in newOperation.StringToPaths()) { Destinations.Add(im); }
            }

            #region Get Properties
            Accounts = accountRep.GetAccounts();
            Purposes = accountRep.GetPurposes();

            CategoryRepository categoryRep = new CategoryRepository(User);
            Categories = categoryRep.GetItems();

            CurrencyRepository currencyRep = new CurrencyRepository();
            Currencies = currencyRep.GetItems();

            UserRepository userRep = new UserRepository();
            Users = userRep.GetItems();

            Accounts.Insert(0, new Account("Выбрать", 0, 0, 0, null));
            Categories.Insert(0, new Category("Выбрать", null));
            Purposes.Insert(0, new Account("Выбрать", 0, 0, 0, null));

            ListForPickerType = new List<string> { "Приход", "Расход", "Перевод" };
            #endregion

            if (newOperation == null)
            {
                Operation = new Operation(DateTime.Now, 0, 2, "", null, null, null, null, Users[0]);
            }
            else
            {
                Operation = newOperation;
                adding = false;
            }
        }

        public async Task SaveAsync()
        {
            Operation.Images = Operation.PathsToString(Destinations);

            if (adding == true)
            {
                Operation.Id = (int)await opRepository.AddItemAsync(Operation);
            }
            else
            {
                await opRepository.UpdateItemAsync(Operation);
            }
        }
        
        public void ChangePurposes(int t)
        {
            if(t == 2)
            {
                Accounts = accountRep.GetAccounts();
                Purposes = accountRep.GetPurposes();
                //Operation.Color = "Red";
            }
            if(t == 1 || t == 3)
            {
                Accounts = accountRep.GetAccounts();
                Purposes = accountRep.GetAccounts();
                //Operation.Color = "Yellow";
                if (t == 1) { Accounts = new List<Account> { new Account("Приход", 0, 0, 0, null) }; /*Operation.Color = "Green";*/ }
            }
        }

        public async void TakePhoto()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Sample",
                    Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                });

                if (photo == null)
                    return;

                List<ImageItem> dest = new List<ImageItem>();
                foreach (var a in Destinations) { dest.Add(a); }
                dest.Add(new ImageItem(photo.Path));
                Destinations = dest;
            }
        }

        public async void GetPhoto()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                List<ImageItem> dest = new List<ImageItem>();
                foreach(var a in Destinations) { dest.Add(a); }
                dest.Add(new ImageItem(photo.Path));
                Destinations = dest;
            }
        }
    }
}
