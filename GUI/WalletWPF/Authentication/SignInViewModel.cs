using Prism.Commands;
using System;
using System.Windows;
using WalletWPF.Authentication;

namespace WalletWPF
{
    public class SignInViewModel
    {

        private AuthenticationUser _authUser = new AuthenticationUser();
        public string Login
        {
            get
            {
                return _authUser.Login;
            }
            set
            {
                if (_authUser.Login != value)
                {
                    _authUser.Login = value;
                  //  OnPropertyChanged();
                  //  SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string Password
        {
            get
            {
                return _authUser.Password;
            }
            set
            {
                if (_authUser.Password != value)
                {
                    _authUser.Password = value;
                  //  OnPropertyChanged();
                   // SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public SignInViewModel(Action gotoSignUp, Action gotoWallets)
        {
            SignInCommand = new DelegateCommand(SignIn, IsSignInEnabled);
        }
        private async void SignIn()
        {
            if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password))
                MessageBox.Show("Login or password is empty.");
            else
            {
                //var authService = new AuthenticationService();
                User user = null;
                try
                {
                   
                    //user = await authService.AuthenticateAsync(_authUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In failed: {ex.Message}");
                    return;
                }
                finally
                {
                   
                }
                MessageBox.Show($"Sign In was successful for user {user.FirstName} {user.LastName}");
                
            }
        }

        private bool IsSignInEnabled()
        {
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password);
        }

        public DelegateCommand SignInCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SignUpCommand { get; }
    }
}
