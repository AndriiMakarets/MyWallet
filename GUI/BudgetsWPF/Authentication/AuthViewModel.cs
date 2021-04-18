using System;
using System.ComponentModel;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Mvvm;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Authentication
{
    public class AuthViewModel : BindableBase
    {
        private readonly Action<User> _signInSuccess;
        private readonly AuthenticationService _authentication;
        public INotifyPropertyChanged CurrentViewModel { get; set; }
        private SignInViewModel _signInViewModel;
        private SignUpViewModel _signUpViewModel;

        public AuthViewModel(Action<User> signInSuccess, AuthenticationService authentication)
        {
            _signInSuccess = signInSuccess;
            _authentication = authentication;
            _signInViewModel = new SignInViewModel(() =>
            {
                CurrentViewModel = _signUpViewModel;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }, _signInSuccess, authentication);
            _signUpViewModel = new SignUpViewModel(() =>
            {
                CurrentViewModel = _signInViewModel;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }, authentication);

            CurrentViewModel = _signInViewModel;
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}