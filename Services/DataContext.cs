using Microsoft.EntityFrameworkCore;
using WalletModel;

namespace AV.ProgrammingWithCSharp.Budgets.Services
{
    public sealed class DataContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Authorization> Authorizations => Set<Authorization>();

        public DataContext() : base()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDb.db;");
        }
    }
}