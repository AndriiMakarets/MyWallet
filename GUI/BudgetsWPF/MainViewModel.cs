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
            var ctx = new DataContext();
            var auth = new AuthenticationService(ctx);
            auth.TryAuthenticateAsync().ContinueWith(t =>
            {
                if (t.Result is { } user)
                    CurrentViewModel = new WalletsViewModel(new WalletService(ctx), user);
            });
            CurrentViewModel = new AuthViewModel(u =>
            {
                CurrentViewModel = new WalletsViewModel(new WalletService(ctx), u);
                RaisePropertyChanged(nameof(CurrentViewModel));
            }, auth);
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}