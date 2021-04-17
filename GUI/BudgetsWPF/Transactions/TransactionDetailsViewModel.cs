using System;
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
        public TransactionDetailsViewModel(Transaction? item, Action onItemDelete, TransactionService service, WalletService walletService, Action notifyTransactionChanged) : base(item, onItemDelete)
        {
            _service = service;
            _walletService = walletService;
            PropertyChanged += (sender, args) => notifyTransactionChanged();
        }

        protected override Task Save()
        {
            return _service.Save();
        }

        protected override Task Delete()
        {
            return _service.DeleteTransaction(Item);
        }

        protected override async Task Add()
        {
            var toWallet = await _walletService.GetWallet(Item.ToId);
            await _service.AddTransaction(Item.From, toWallet, Item.Amount, Item.Description);
        }

        protected override Task<Transaction> Get()
        {
            return _service.GetTransaction(Item.Id);
        }
    }
}