using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Authentication;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Wallets;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Mvvm;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF
{
    public class MainViewModel : BindableBase
    {
        private BindableBase _currentViewModel;

        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }
        }

        public MainViewModel()
        {
            var ctx = new DataContext();
            var auth = new AuthenticationService(ctx);
            var wallets = new WalletService(ctx);
            var transactions = new TransactionService(ctx);
            auth.TryAuthenticateAsync().ContinueWith(t =>
            {
                if (t.Result is { } user)
                    CurrentViewModel = new WalletsViewModel(wallets, transactions, user,
                        model => CurrentViewModel = model, m => CurrentViewModel = m);
            });
            CurrentViewModel =
                new AuthViewModel(
                    u => CurrentViewModel = new WalletsViewModel(wallets, transactions, u,
                        model => CurrentViewModel = model, m => CurrentViewModel = m), auth);
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}