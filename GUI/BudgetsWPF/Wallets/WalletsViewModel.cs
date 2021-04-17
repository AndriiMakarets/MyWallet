using System;
using System.Collections.ObjectModel;
using System.Linq;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Transactions;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Commands;
using Prism.Mvvm;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Wallets
{
    public class WalletsViewModel : BindableBase
    {
        private readonly WalletService _service;
        private readonly User _user;
        private WalletDetailsViewModel _currentWallet;
        public ObservableCollection<WalletDetailsViewModel> Wallets { get; set; }

        public WalletDetailsViewModel CurrentWallet
        {
            get { return _currentWallet; }
            set
            {
                _currentWallet = value;
                RaisePropertyChanged();
            }
        }

        public WalletsViewModel(WalletService service, TransactionService transactionService, User user, Action<TransactionsViewModel> onManageTransaction, Action<WalletsViewModel> toWalletList)
        {
            _service = service;
            _user = user;
            Wallets = new ObservableCollection<WalletDetailsViewModel>();
            _service.GetWallets(user).ContinueWith(t =>
            {
                foreach (var wallet in t.Result)
                {
                    lock (Wallets)
                    {
                        var i = Wallets.Count;
                        Wallets.Add(new WalletDetailsViewModel(service, user, () =>
                        {
                            CurrentWallet = null;
                            Wallets.RemoveAt(i);
                        }, onManageTransaction , transactionService,() => toWalletList(this), wallet));
                    }
                }
            });
            AddWalletCommand = new DelegateCommand(() =>
            {
                lock (Wallets)
                {
                    var i = Wallets.Count;
                    var wallet = new WalletDetailsViewModel(service, user, () =>
                    {
                        CurrentWallet = null;
                        Wallets.RemoveAt(i);
                    }, onManageTransaction, transactionService, () => toWalletList(this));
                    Wallets.Add(wallet);
                    CurrentWallet = wallet;
                }
            });
        }

        public DelegateCommand AddWalletCommand { get; }


        public string DisplayName => _currentWallet.Name;
    }
}