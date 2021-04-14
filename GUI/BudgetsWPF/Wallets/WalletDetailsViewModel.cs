using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.ProgrammingWithCSharp.Budgets.Services;
using Prism.Commands;
using Prism.Mvvm;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Wallets
{
    public class WalletDetailsViewModel : BindableBase
    {
        private bool IsNew;
        public Wallet Wallet { get; }


        public string Name
        {
            get => Wallet.Name ?? "New Wallet";
            set
            {
                Wallet.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public decimal InitialBalance
        {
            get => Wallet.InitialBalance;
            set
            {
                Wallet.InitialBalance = value;
                RaisePropertyChanged(nameof(InitialBalance));
            }
        }

        public string Description
        {
            get => Wallet.Description;
            set
            {
                Wallet.Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public string Currency
        {
            get => Wallet.Currency;
            set
            {
                Wallet.Currency = value;
                RaisePropertyChanged(nameof(Currency));
            }
        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public WalletDetailsViewModel(WalletService service, User user, Action onDelete, Wallet wallet = null)
        {
            if (wallet is null)
                IsNew = true;
            Wallet = wallet ?? new Wallet();
            SaveCommand = new DelegateCommand(async () =>
            {
                if (IsNew)
                {
                    await service.AddWallet(user, Wallet.Name, Wallet.Description, Wallet.Currency,
                        Wallet.InitialBalance);
                }
                else await service.Save();
            });
            DeleteCommand = new DelegateCommand(async () =>
            {
                if (!IsNew)
                    await service.DeleteWallet(Wallet);
                onDelete();
            });
        }
    }
}