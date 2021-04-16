using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Common;
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
        private readonly User _user;
        public WalletDetailsViewModel(WalletService walletService, User user, Action onDelete, Wallet wallet = null) : base(wallet, onDelete)
        {
            _walletService = walletService;
            _user = user;
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

        protected override Task<Wallet> Get() => _walletService.GetWallet(Item);
    }
}