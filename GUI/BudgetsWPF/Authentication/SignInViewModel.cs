using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AV.ProgrammingWithCSharp.Budgets.Models.ViewModels;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Commands;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Authentication
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        private readonly AuthUser _authUser = new ();
        private readonly Action<User> _gotoWallets;
        private bool _isEnabled = true;


        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _authUser.Email;
            set
            {
                if (_authUser.Email != value)
                {
                    _authUser.Email = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string Password
        {
            get => _authUser.Password;
            set
            {
                if (_authUser.Password != value)
                {
                    _authUser.Password = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand SignInCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SignUpCommand { get; }

        public SignInViewModel(Action gotoSignUp, Action<User> gotoWallets)
        {
            SignInCommand = new DelegateCommand(SignIn, IsSignInEnabled);
            CloseCommand = new DelegateCommand(() => Environment.Exit(0));
            SignUpCommand = new DelegateCommand(gotoSignUp);
            _gotoWallets = gotoWallets;
        }

        private async void SignIn()
        {
            if (String.IsNullOrWhiteSpace(Email) || String.IsNullOrWhiteSpace(Password))
                MessageBox.Show("Email or password is empty.");
            else
            {
                var authService = new AuthenticationService(new DataContext());
                User user = null;
                try
                {
                    IsEnabled = false;
                    user = await authService.AuthenticateAsync(_authUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In failed: {ex.Message}");
                    return;
                }
                finally
                {
                    IsEnabled = true;
                }
                MessageBox.Show($"Sign In was successful for user {user.FirstName} {user.LastName}");
                _gotoWallets(user);
            }
        }

        private bool IsSignInEnabled()
        {
            return !String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Password);
        }

        public void ClearSensitiveData()
        {
            Password = "";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
