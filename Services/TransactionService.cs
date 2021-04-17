using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.Services
{
    public class TransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddTransaction(Wallet from, Wallet to, decimal amount, string description)
        {
            if (from.Currency != to.Currency)
                throw new ArgumentException("Different currencies");
            var tr = new Transaction()
            {
                Amount = amount,
                Category = null,
                Currency = to.Currency,
                DateTime = DateTime.Now,
                Description = description,
                From = from,
                ToId = to.Id
            };
            await _context.Transactions.AddAsync(tr);
            await _context.SaveChangesAsync();
            return tr;
        }

        public async Task DeleteTransaction(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public Task<List<Transaction>> GetAllRelatedTransactions(Wallet wallet)
        {
            return _context.Transactions
                .Include(t => t.From)
                .Where(t => t.From.Id == wallet.Id || t.ToId == wallet.Id)
                .ToListAsync();
        }

        public async Task<Transaction> GetTransaction(int trId)
        {
            return await _context.Transactions.FindAsync(trId);
        }

        public Task Save() => _context.SaveChangesAsync();
    }
}