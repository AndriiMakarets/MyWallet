using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WalletModel
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }

    public class Authorization
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Time { get; set; }
    }

    public class Wallet
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public decimal InitialBalance { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public List<Category> Categories { get; set; }
        public List<Transaction> Transactions { get; set; }

        public decimal Balance() => 
            Transactions.Where(t => t.ToId == this.Id).Sum(t => t.Amount) -
            Transactions.Where(t => t.From == this).Sum(t => t.Amount);

        public decimal Income() => Transactions
                .Where(t => t.ToId == this.Id && t.DateTime > DateTime.Today.AddMonths(-1))
                .Sum(t => t.Amount);
        public decimal Outcome() => Transactions
                .Where(t => t.ToId == this.Id && t.DateTime > DateTime.Today.AddMonths(-1))
                .Sum(t => t.Amount);
    }

    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public Wallet From { get; set; }
        public int ToId { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public byte[] Icon { get; set; }
    }
}