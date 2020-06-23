using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Grossbuch.Models;
using Grossbuch.Repositories;
using Grossbuch.ViewModels;

namespace Grossbuch.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewAimPage : ContentPage
	{
        AimDetailVM aimVM;

        #region Constructors
        public NewAimPage()
        {
            InitializeComponent();
        }

        public NewAimPage(AimDetailVM _aimVM)
        {
            InitializeComponent();
            BindingContext = aimVM = _aimVM;
        }

        public NewAimPage(Aim newAim, User _user, AimRepository _repository)
        {
            InitializeComponent();
            BindingContext = aimVM = new AimDetailVM(newAim, _user, _repository);
        }
        #endregion

        void Picker_SelectedIndexChanged(object sender, EventArgs e) //Обработчик PickerType
        {
            int t = StringToType(typePicker.Items[typePicker.SelectedIndex]);
            if (t != 0) { aimVM.Aim.Type = t; aimVM.ChangeFacilities(t); }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var choosenFacility = facilityPicker.SelectedItem;

            if (choosenFacility is Account accountForFacility)
            {
                aimVM.Aim.Facility1 = accountForFacility;
            }
            else
            {
                if (choosenFacility is Category categoryForFacility)
                {
                    aimVM.Aim.Facility2 = categoryForFacility;
                }
            }

            if (AreDetailsValid())
            {
                await aimVM.SaveAsync();
                if (aimVM.adding == true) { MessagingCenter.Send(this, "AddOperation", aimVM.Aim); }
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
                case 1: return "Накопление";
                case 2: return "Трата";
            }
            return null;
        }

        private int StringToType(string type)
        {
            switch (typePicker.Items[typePicker.SelectedIndex])
            {
                case "Накопление": return 1;
                case "Трата": return 2;
            }
            return 0;
        }
        #endregion

        bool AreDetailsValid()
        {
            try
            {
                float a = float.Parse(aimVM.Aim.PlannedSum.ToString());
                return (a >= 0) ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}