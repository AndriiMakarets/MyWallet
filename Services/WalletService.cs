using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.Services
{
    public class WalletService
    {
        private readonly DataContext _context;

        public WalletService(DataContext context)
        {
            _context = context;
        }

        public Task<List<Wallet>> GetWallets(User user)
        {
            return _context.Wallets.Include(t => t.Transactions).Where(t => t.OwnerId == user.Id).ToListAsync();
        }
        public Task<List<Wallet>> GetWalletsForTransaction(Wallet wallet)
        {
            return _context.Wallets
                .Include(t => t.Transactions)
                .Where(t => t.Id != wallet.Id && t.Currency == wallet.Currency)
                .ToListAsync();
        }

        public async Task<Wallet> AddWallet(User user, string name, string description, string currency, decimal initialBalance)
        {
            var wallet = new Wallet()
            {
                Categories = new(),
                Name = name,
                Currency = currency,
                Description = description,
                InitialBalance = initialBalance,
                Owner = user,
                Transactions = new(),
            };
            await _context.AddAsync(wallet);
            await _context.SaveChangesAsync();
            return wallet;
        }

        public async Task DeleteWallet(Wallet wallet)
        {
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
        }
        
        public Task Save() => _context.SaveChangesAsync();

        public Task<Wallet> GetWallet(int walletId) => _context.Wallets.Include(t => t.Transactions).FirstOrDefaultAsync(t => t.Id == walletId);
    }
}
