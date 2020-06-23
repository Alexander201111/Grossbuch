using Grossbuch.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        AuthVM viewModel;

        public LoginPage()
        {
            InitializeComponent();

            UsernameEntry.Text = "user1";
            PasswordEntry.Text = "password1";
            //UsernameEntry.Text = "admin";
            //PasswordEntry.Text = "admin";

            BindingContext = viewModel = new AuthVM();

            SignUp.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnSignUpButtonClicked()));
        }

        async void OnSignUpButtonClicked()
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await viewModel.LoginAsync(UsernameEntry.Text, PasswordEntry.Text);
            if (viewModel.User != null)
            {
                App.IsUserLoggedIn = true;
                Application.Current.MainPage = new MainPage(viewModel.User);
            }
            else
            {
                //messageLabel.Text = "Неправильный логин или пароль";
                PasswordEntry.Text = string.Empty;
            }
        }
    }
}
