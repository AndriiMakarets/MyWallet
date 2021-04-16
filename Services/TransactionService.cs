using System.Threading.Tasks;
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

        public async Task<Transaction> AddTransaction()
        {
            return null;
        }
         
        public Task Save() => _context.SaveChangesAsync();
    }
}