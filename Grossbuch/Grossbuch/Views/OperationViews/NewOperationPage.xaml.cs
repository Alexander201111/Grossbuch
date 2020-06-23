using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

using Grossbuch.Models;
using Grossbuch.Repositories;
using Grossbuch.ViewModels;
using Grossbuch.Services;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewOperationPage : ContentPage
    {
        OperationDetailVM opVM;

        string sumOperation = "";

        #region Constructors
        public NewOperationPage(OperationDetailVM _opVM)
        {
            InitializeComponent();
            BindingContext = opVM = _opVM;
            InitSelectedPickers(opVM.Operation);
        }

        public NewOperationPage(Operation newOperation, User _user, OperationRepository opRepository)
        {
            InitializeComponent();
            BindingContext = opVM = new OperationDetailVM(newOperation, _user, opRepository);
            InitSelectedPickers(newOperation);
        }

        private void InitSelectedPickers(Operation newOperation)
        {
            if (newOperation == null)
            {
                typePicker.SelectedIndex = 1;
                accountPicker.SelectedIndex = 0;
                categoryPicker.SelectedIndex = 0;
                currencyPicker.SelectedIndex = 0;
                purposePicker.SelectedIndex = 0;
            }
            else
            {
                typePicker.SelectedIndex = opVM.ListForPickerType.IndexOf(TypeToString(opVM.Operation.Type));
                accountPicker.SelectedItem = opVM.Accounts.SingleOrDefault(item => item.Id == newOperation.Account.Id);
                categoryPicker.SelectedItem = opVM.Categories.SingleOrDefault(item => item.Id == newOperation.Category.Id);
                currencyPicker.SelectedItem = opVM.Currencies.SingleOrDefault(item => item.Id == newOperation.Currency.Id);
                purposePicker.SelectedItem = opVM.Purposes.SingleOrDefault(item => item.Id == newOperation.Purpose.Id);
            }
        }
        #endregion

        void Picker_SelectedIndexChanged(object sender, EventArgs e) //Обработчик PickerType
        {
            int t = StringToType(typePicker.Items[typePicker.SelectedIndex]);
            if (t != 0)
            {
                opVM.Operation.Type = t;
                opVM.ChangePurposes(t);
                accountPicker.SelectedIndex = 0;
                purposePicker.SelectedIndex = 0;
            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            opVM.Operation.Account = (Account)accountPicker.SelectedItem;
            opVM.Operation.Category = (Category)categoryPicker.SelectedItem;
            opVM.Operation.Currency = (Currency)currencyPicker.SelectedItem;
            opVM.Operation.Purpose = (Account)purposePicker.SelectedItem;

            if (opVM.Operation.Account != null && opVM.Operation.Account.Title == "Приход" || opVM.Operation.Account.Title == "Выбрать") { opVM.Operation.Account = null; }
            if (opVM.Operation.Category != null && opVM.Operation.Category.Title == "Выбрать") { opVM.Operation.Category = null; }
            if (opVM.Operation.Purpose != null && opVM.Operation.Purpose.Title == "Выбрать") { opVM.Operation.Purpose = null; }

            if (AreDetailsValid())
            {
                await opVM.SaveAsync();
                if (opVM.adding == true) { MessagingCenter.Send(this, "AddOperation", opVM.Operation); }
                //else { MessagingCenter.Send(this, "UpdateOperation", opVM.Operation); }
                await Navigation.PopModalAsync();
            }
            else
            {
                messageLabel.Text = "Введены неверные данные";
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        #region Parsing
        private string TypeToString(int type)
        {
            switch (type)
            {
                case 1: return "Приход";
                case 2: return "Расход";
                case 3: return "Перевод";
            }
            return null;
        }

        private int StringToType(string type)
        {
            switch (typePicker.Items[typePicker.SelectedIndex])
            {
                case "Приход": return 1;
                case "Расход": return 2;
                case "Перевод": return 3;
            }
            return 0;
        }
        #endregion

        bool AreDetailsValid()
        {
            //try
            //{
            //    float a = float.Parse(opVM.Operation.Sum.ToString());
            //    return (a >= 0) ? true : false;
            //}
            //catch
            //{
            //    return false;
            //}
            return true;
        }

        #region Photo
        void TakePhoto(object sender, EventArgs e) //Сделать снимок
        {
            opVM.TakePhoto();
        }

        void GetPhoto(object sender, EventArgs e) //Выбрать фото
        {
            opVM.GetPhoto();
        }

        void AddToListImages(object sender, EventArgs e)
        {
            opVM.GetPhoto();
        }

        private async void BtnScan_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtBarcode.Text = result;
                    opVM.Operation.Summ = ParseSumm(txtBarcode.Text);
                    opVM.Operation.Date = ParseDate(txtBarcode.Text);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private float ParseSumm(string str)
        {
            string sum = "";
            try { sum = str.Substring(20, 5); return float.Parse(sum); }
            catch (Exception) { return 0; }
        }

        private DateTime ParseDate(string str)
        {
            string date = "";
            try
            {
                date = str.Substring(2, 8) + str.Substring(11, 6);
                return DateTime.ParseExact("20090508144052", "yyyyMMddHHmmss",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception) { return DateTime.Now; }
        }
        #endregion
    }
}