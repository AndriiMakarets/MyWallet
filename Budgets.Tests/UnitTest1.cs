using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AV.ProgrammingWithCSharp.Budgets.Models.ViewModels;
using AV.ProgrammingWithCSharp.Budgets.Services;
using NUnit.Framework;
using WalletModel;

namespace Budgets.Tests
{
    public class Tests
    {
        private DataContext _context;
        private TransactionService _transactionService;
        private WalletService _walletService;
        private AuthenticationService _authenticationService;
        private User _user;
      
        public Tests()
        {
        }

        [SetUp]
        public void Setup()
        {
            _context = new();
            _transactionService = new(_context);
            _walletService = new(_context);
            _authenticationService = new(_context);
            _user = _authenticationService.RegisterUserAsync(new RegisterUser()
                {Email = "banan@mail.com", FirstName = "Banan", LastName = "Pasha", Password = "123"}).GetAwaiter().GetResult();
        }

        [TearDown]
        public void CleanUp()
        {
            File.Delete("MyDb.db");
        }

        [Test]
        public void Test1()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var savedWallet = _walletService.GetWallet(wallet.Id).GetAwaiter().GetResult();
            Assert.AreEqual(wallet.Name, savedWallet.Name);
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var savedWallet = _walletService.GetWallet(wallet.Id).GetAwaiter().GetResult();
            Assert.AreEqual(wallet.Balance(), savedWallet.Balance());
            Assert.Pass();
        }


        [Test]
        public void Test3()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var savedWallet = _walletService.GetWallet(wallet.Id).GetAwaiter().GetResult();
            Assert.AreEqual(wallet.Categories, savedWallet.Categories);
            Assert.Pass();
        }


        [Test]
        public void Test4()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var savedWallet = _walletService.GetWallet(wallet.Id).GetAwaiter().GetResult();
            Assert.AreEqual(wallet.Currency, savedWallet.Currency);
            Assert.Pass();
        }

        [Test]
        public void Test5()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var savedWallet = _walletService.GetWallet(wallet.Id).GetAwaiter().GetResult();
            Assert.AreEqual(wallet.OwnerId, savedWallet.OwnerId);
            Assert.Pass();
        }

        [Test]
        public void Tes6()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var savedWallet = _walletService.GetWallet(wallet.Id).GetAwaiter().GetResult();
            Assert.AreEqual(wallet.Transactions, savedWallet.Transactions);
            Assert.Pass();
        }

        [Test]
        public void Test7()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var savedWallet = _walletService.GetWallet(wallet.Id).GetAwaiter().GetResult();
            Assert.AreEqual(wallet.Owner, savedWallet.Owner);
            Assert.Pass();
        }


        [Test]
        public void Test8()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 110).GetAwaiter().GetResult();
            var wallet1 = _walletService.AddWallet(_user, "name1", "descr", "$", 110).GetAwaiter().GetResult();
            var transaction = _transactionService.AddTransaction(wallet, wallet1, 100, "a").GetAwaiter().GetResult();
            var savedTransaction = _transactionService.GetTransaction(transaction.Id).GetAwaiter().GetResult();
            Assert.AreEqual(transaction.Id, savedTransaction.Id);
            Assert.Pass();
        }

        [Test]
        public void Test9()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 110).GetAwaiter().GetResult();
            var wallet1 = _walletService.AddWallet(_user, "name1", "descr", "$", 110).GetAwaiter().GetResult();
            var transaction = _transactionService.AddTransaction(wallet, wallet1, 100, "a").GetAwaiter().GetResult();
            var savedTransaction = _transactionService.GetTransaction(transaction.Id).GetAwaiter().GetResult();
            Assert.AreEqual(transaction.Id, savedTransaction.Id);
            Assert.Pass();
        }

        [Test]
        public void Test10()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 110).GetAwaiter().GetResult();
            var wallet1 = _walletService.AddWallet(_user, "name1", "descr", "$", 110).GetAwaiter().GetResult();
            var transaction = _transactionService.AddTransaction(wallet, wallet1, 100, "a").GetAwaiter().GetResult(); ;
            var savedTransaction = _transactionService.GetTransaction(transaction.Id).GetAwaiter().GetResult();
            Assert.AreEqual(transaction.Id, savedTransaction.Id);
            Assert.Pass();
        }

        [Test]
        public void Test11()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var wallet1= _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
             _walletService.DeleteWallet(wallet).GetAwaiter().GetResult(); ;
             _walletService.DeleteWallet(wallet1).GetAwaiter().GetResult(); ;
            Assert.AreNotEqual(wallet, wallet1);
            Assert.Pass();
        }

        [Test]
        public void Test12()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 0).GetAwaiter().GetResult();
            var wallet1 = _walletService.AddWallet(_user, "name1", "descr", "$", 0).GetAwaiter().GetResult();
            List<Wallet> list = _walletService.GetWallets(_user).GetAwaiter().GetResult();
            List<Wallet> list2 = new List<Wallet>();
            list2.Add(wallet);
            list2.Add(wallet1);
            Assert.AreEqual(list,list2);
            Assert.Pass();
        }

        [Test]
        public void Test13()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 110).GetAwaiter().GetResult();
            var wallet1 = _walletService.AddWallet(_user, "name1", "descr", "$", 110).GetAwaiter().GetResult();
            var transaction = _transactionService.AddTransaction(wallet, wallet1, 100, "a").GetAwaiter().GetResult();
             _transactionService.DeleteTransaction(transaction).GetAwaiter().GetResult(); ;
            var savedTransaction = _transactionService.GetTransaction(transaction.Id).GetAwaiter().GetResult();
            Assert.AreEqual(null, savedTransaction);
            Assert.Pass();
        }

        [Test]
        public void Test14()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 110).GetAwaiter().GetResult();
            var wallet1 = _walletService.AddWallet(_user, "name1", "descr", "$", 110).GetAwaiter().GetResult();
            var transaction = _transactionService.AddTransaction(wallet, wallet1, 100, "a").GetAwaiter().GetResult();
            List<Transaction> list = _transactionService.GetAllRelatedTransactions(wallet).GetAwaiter().GetResult();
            List<Transaction> expectedList = new List<Transaction>();
            expectedList.Add(transaction);
            Assert.AreEqual(list, expectedList);
            Assert.Pass();
        }

    }
}