using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Common;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Transactions;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Commands;
using Prism.Mvvm;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Wallets
{
    public class WalletDetailsViewModel : CrudElemViewModel<Wallet>
    {
        public string Name
        {
            get => Item.Name ?? "New Wallet";
            set => Setter(() => Item.Name = value);
        }

        public bool AlreadyInDb => ItemState is not EntityState.Added or EntityState.Pending;

        public decimal CurrentBalance { get; set; }

        public decimal Income { get; set; }

        public decimal Outcome { get; set; }

        public decimal InitialBalance
        {
            get => Item.InitialBalance;
            set => Setter(() => Item.InitialBalance = value);
        }

        public string Description
        {
            get => Item.Description;
            set => Setter(() => Item.Description = value);
        }

        public string Currency
        {
            get => Item.Currency;
            set => Setter(() => Item.Currency = value);
        }

        private readonly WalletService _walletService;
        private readonly TransactionService _transactionService;
        private readonly User _user;

        public WalletDetailsViewModel(WalletService walletService, User user, Action onDelete,
            Action<TransactionsViewModel> onManageTransactionRequest, TransactionService transactionService,
            Action toWalletList,
            Wallet wallet = null) : base(wallet, onDelete)
        {
            _walletService = walletService;
            _user = user;
            _transactionService = transactionService;
            ManageTransactionsCommand = new DelegateCommand(() =>
                onManageTransactionRequest(new TransactionsViewModel(walletService, transactionService, _user, Item,
                    () => RaisePropertyChanged(), toWalletList)), () => AlreadyInDb);
            PropertyChanged += async (sender, args) => await UpdateTransactionInfo();
            var _ = UpdateTransactionInfo();
        }

        private async Task UpdateTransactionInfo()
        {
            if (ItemState is not EntityState.Pending or EntityState.Added && Item is not null)
            {
                var tr = await _transactionService.GetAllRelatedTransactions(Item);
                Income = tr.Where(t => t.ToId == Item.Id && t.DateTime > DateTime.Now.AddMonths(-1)).Sum(t => t.Amount);
                Outcome = tr.Where(t => t.From.Id == Item.Id && t.DateTime > DateTime.Now.AddMonths(-1))
                    .Sum(t => t.Amount);
                CurrentBalance = InitialBalance
                                 + tr.Where(t => t.ToId == Item.Id).Sum(t => t.Amount)
                                 - tr.Where(t => t.From.Id == Item.Id).Sum(t => t.Amount);
            }
        }

        protected override async Task Save()
        {
            await _walletService.Save();
        }

        protected override async Task Delete()
        {
            await _walletService.DeleteWallet(Item);
            await _walletService.Save();
        }

        protected override async Task Add()
        {
            await _walletService.AddWallet(_user, Item.Name, Item.Description, Item.Currency, Item.InitialBalance);
            await _walletService.Save();
        }

        protected override Task<Wallet> Get() => _walletService.GetWallet(Item.Id);

        public DelegateCommand ManageTransactionsCommand { get; }
    }
}