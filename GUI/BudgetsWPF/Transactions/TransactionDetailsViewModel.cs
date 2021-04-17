using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Common;
using AV.ProgrammingWithCSharp.Budgets.Services;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Transactions
{
    public class TransactionDetailsViewModel : CrudElemViewModel<Transaction>
    {
        private readonly TransactionService _service;
        private readonly WalletService _walletService;
        private readonly Action _notifyTransactionChanged;
        private readonly Wallet _wallet;
        public TransactionDetailsViewModel(Transaction? item, Wallet wallet, Action onItemDelete, TransactionService service, WalletService walletService, Action notifyTransactionChanged) : base(item, onItemDelete)
        {
            _service = service;
            _walletService = walletService;
            _notifyTransactionChanged = notifyTransactionChanged;
            _wallet = wallet;
            ToWallets = new();
            walletService.GetWalletsForTransaction(wallet).ContinueWith(t =>
            {
                foreach (var wallet in t.Result)
                {
                   ToWallets.Add(wallet); 
                }
                RaisePropertyChanged(nameof(ToWallets));
            });
        }

        
        public string Description
        {
            get => Item.Description;
            set => Setter(() => Item.Description = value);
        }
        public decimal Amount
        {
            get => Item.Amount;
            set => Setter(() => Item.Amount = value);
        }
        
        public ObservableCollection<Wallet> ToWallets { get; }
        public Wallet CurrentToWallet { get; set; }
        protected override async Task Save()
        {
            await _service.Save();
            _notifyTransactionChanged();
        }

        protected override async Task Delete()
        {
            await _service.DeleteTransaction(Item);
            _notifyTransactionChanged();
        }

        protected override async Task Add()
        {
            await _service.AddTransaction(_wallet, CurrentToWallet , Item.Amount, Item.Description);
            _notifyTransactionChanged();
        }

        protected override Task<Transaction> Get()
        {
            return _service.GetTransaction(Item.Id);
        }
    }
}