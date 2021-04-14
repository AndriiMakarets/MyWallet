using System;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Authentication;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Navigation;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Wallets;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Mvvm;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF
{
    public class MainViewModel : BindableBase
    {
        public BindableBase CurrentViewModel { get; set; }
        public MainViewModel()
        {
            
            var auth = new AuthenticationService(new DataContext());
            if (auth.TryAuthenticateAsync().GetAwaiter().GetResult() is { } user)
                CurrentViewModel = new WalletsViewModel();
            else CurrentViewModel = new AuthViewModel(u =>
            {
                CurrentViewModel = new WalletsViewModel();
                RaisePropertyChanged(nameof(CurrentViewModel));
            }, auth);
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
