using System.Collections.Generic;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.Services
{
    public class WalletService
    {
        
        private static List<Wallet> Users = new List<Wallet>()
        {
            new Wallet() {Name = "wal1"},
            new Wallet() {Name = "wal2"},
            new Wallet() {Name = "wal3"},
            new Wallet() {Name = "wal4"},
            new Wallet() {Name = "wal5"},
        };

        public List<Wallet> GetWallets()
        {
            return new ();
        }
    }
}
