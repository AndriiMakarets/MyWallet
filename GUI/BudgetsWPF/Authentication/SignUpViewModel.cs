using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Navigation;
using AV.ProgrammingWithCSharp.Budgets.Models.ViewModels;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Commands;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Authentication
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private RegisterUser _regUser = new ();
        private Action _gotoSignIn;
        
        public AuthNavigatableTypes Type
        {
            get
            {
                return AuthNavigatableTypes.SignUp;
            }
        }

        public string Email
        {
            get
            {
                return _regUser.Email;
            }
            set
            {
                if (_regUser.Email != value)
                {
                    _regUser.Email = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Password
        {
            get
            {
                return _regUser.Password;
            }
            set
            {
                if (_regUser.Password != value)
                {
                    _regUser.Password = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string FirstName
        {
            get
            {
                return _regUser.FirstName;
            }
            set
            {
                if (_regUser.FirstName != value)
                {
                    _regUser.FirstName = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string LastName
        {
            get
            {
                return _regUser.LastName;
            }
            set
            {
                if (_regUser.LastName != value)
                {
                    _regUser.LastName = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand SignUpCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SignInCommand { get; }

        public SignUpViewModel(Action gotoSignIn)
        {
            SignUpCommand = new DelegateCommand(SignUp, IsSignUpEnabled);
            CloseCommand = new DelegateCommand(() => Environment.Exit(0));
            _gotoSignIn = gotoSignIn;
            SignInCommand = new DelegateCommand(_gotoSignIn);
        }

        private async void SignUp()
        {

            var authService = new AuthenticationService(new DataContext());
            try
            {
                await authService.RegisterUserAsync(_regUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign In failed: {ex.Message}");
                return;
            }

            MessageBox.Show($"User successfully registered, please Sign In");
            _gotoSignIn.Invoke();
        }

        private bool IsSignUpEnabled()
        {
            return !String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Password) && !String.IsNullOrWhiteSpace(LastName);
        }

        public void ClearSensitiveData()
        {
            _regUser = new ();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
