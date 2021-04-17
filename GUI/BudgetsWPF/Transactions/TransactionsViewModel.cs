using System;
using System.Collections.ObjectModel;
using System.Linq;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Common;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Commands;
using Prism.Mvvm;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Transactions
{
    public class TransactionsViewModel : BindableBase
    {
        public ObservableCollection<TransactionDetailsViewModel> Transactions { get; set; }

        private TransactionDetailsViewModel _currentTransaction;

        public TransactionsViewModel(WalletService service, TransactionService transactionService, User user,
            Wallet wallet, Action notifyTransactionListChanged, Action toWalletList)
        {
            Transactions = new();
            AddTransactionCommand = new DelegateCommand(() =>
            {
                lock (Transactions)
                {
                    var i = Transactions.Count;
                    var tr = new TransactionDetailsViewModel(null, wallet, () =>
                    {
                        CurrentTransaction = null;
                        Transactions.RemoveAt(i);
                    }, transactionService, service, notifyTransactionListChanged);
                    Transactions.Add(tr);
                    CurrentTransaction = tr;
                }
            });
            transactionService.GetAllRelatedTransactions(wallet).ContinueWith(t =>
            {
                foreach (var transaction in t.Result)
                {
                    lock (Transactions)
                    {
                        var i = Transactions.Count;
                        var tr = new TransactionDetailsViewModel(transaction, wallet, () =>
                        {
                            CurrentTransaction = null;
                            Transactions.RemoveAt(i);
                        }, transactionService, service, notifyTransactionListChanged);
                        Transactions.Add(tr);
                        CurrentTransaction = tr;
                    }
                }
            });
            ToWalletListCommand = new DelegateCommand(toWalletList,
                () => Transactions.All(t => t.GetItemState() == EntityState.Unchanged));
        }

        public TransactionDetailsViewModel CurrentTransaction
        {
            get { return _currentTransaction; }
            set
            {
                _currentTransaction = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand AddTransactionCommand { get; }
        public DelegateCommand ToWalletListCommand { get; }
    }
}