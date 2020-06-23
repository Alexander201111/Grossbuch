using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Grossbuch.Models;
using Grossbuch.Repositories;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDebtPage : ContentPage
    {
        DebtDetailVM debtVM;

        #region Constructors
        public NewDebtPage()
        {
            InitializeComponent();
        }

        public NewDebtPage(DebtDetailVM _debtVM)
        {
            InitializeComponent();
            BindingContext = debtVM = _debtVM;
            InitSelectedPickers(debtVM.Debt);
        }

        public NewDebtPage(Debt newDebt, User _user, DebtRepository _repository)
        {
            InitializeComponent();
            BindingContext = debtVM = new DebtDetailVM(newDebt, _user, _repository);
            InitSelectedPickers(newDebt);
        }

        private void InitSelectedPickers(Debt newDebt)
        {
            if (newDebt == null)
            {
                typePicker.SelectedIndex = 1;
            }
            else
            {
                typePicker.SelectedIndex = debtVM.ListForPickerType.IndexOf(TypeToString(debtVM.Debt.Type));
            }
        }
        #endregion

        void Picker_SelectedIndexChanged(object sender, EventArgs e) //Обработчик PickerType
        {
            int t = StringToType(typePicker.Items[typePicker.SelectedIndex]);
            if (t != 0)
            {
                debtVM.Debt.Type = t;
            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (AreDetailsValid())
            {
                await debtVM.SaveAsync();
                if (debtVM.adding == true) { MessagingCenter.Send(this, "AddOperation", debtVM.Debt); }
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

        bool AreDetailsValid()
        {
            //try
            //{
            //    float a = float.Parse(debtVM.Debt.PlannedSum.ToString());
            //    return (a >= 0) ? true : false;
            //}
            //catch
            //{
            //    return false;
            //}
            return true;
        }

        #region Parsing
        private string TypeToString(int type)
        {
            switch (type)
            {
                case 1: return "Ежемесячный возврат части кредита с уплатой процентов";
                case 2: return "Аннуитетный платеж";
                case 3: return "Единовременный возврат кредита с периодической уплатой процентов";
            }
            return null;
        }

        private int StringToType(string type)
        {
            switch (typePicker.Items[typePicker.SelectedIndex])
            {
                case "Ежемесячный возврат части кредита с уплатой процентов": return 1;
                case "Аннуитетный платеж": return 2;
                case "Единовременный возврат кредита с периодической уплатой процентов": return 3;
            }
            return 0;
        }
        #endregion
    }
}