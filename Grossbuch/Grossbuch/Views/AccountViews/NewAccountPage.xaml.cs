using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grossbuch.Models;
using Grossbuch.ViewModels;
using Grossbuch.Repositories;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAccountPage : ContentPage
    {
        AccountDetailVM acVM;

        public NewAccountPage(AccountDetailVM _acVM)
        {
            InitializeComponent();
            BindingContext = acVM = _acVM;
        }

        public NewAccountPage(Account _account, User _user)
        {
            InitializeComponent();
            BindingContext = acVM = new AccountDetailVM(_account, _user);
        }

        public NewAccountPage(bool isAccount, User _user)
        {
            InitializeComponent();
            BindingContext = acVM = new AccountDetailVM(isAccount, _user);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (AreDetailsValid())
            {
                await acVM.SaveAsync();
                if (acVM.adding == true)
                {
                    if (acVM.Account.IsAccount == true) { MessagingCenter.Send(this, "AddAccount", acVM.Account); }
                    else { MessagingCenter.Send(this, "AddPurpose", acVM.Account); }
                }
                //else { MessagingCenter.Send(this, "UpdateAccount", opVM.Operation); }
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
            //    float a = float.Parse(opVM.Operation.Sum.ToString());
            //    return (a >= 0) ? true : false;
            //}
            //catch
            //{
            //    return false;
            //}
            return true;
        }
    }
}