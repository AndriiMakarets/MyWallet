using System.IO;
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
            var transaction = _transactionService.AddTransaction(wallet, wallet1, 100, "a");
            var savedTransaction = _transactionService.GetTransaction(transaction.Id).GetAwaiter().GetResult();
            Assert.AreEqual(transaction.Id, savedTransaction.Id);
            Assert.Pass();
        }

        [Test]
        public void Test9()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 110).GetAwaiter().GetResult();
            var wallet1 = _walletService.AddWallet(_user, "name1", "descr", "$", 110).GetAwaiter().GetResult();
            var transaction = _transactionService.AddTransaction(wallet, wallet1, 100, "a");
            var savedTransaction = _transactionService.GetTransaction(transaction.Id).GetAwaiter().GetResult();
            Assert.AreEqual(transaction.Id, savedTransaction.Id);
            Assert.Pass();
        }

        [Test]
        public void Test10()
        {
            var wallet = _walletService.AddWallet(_user, "name", "descr", "$", 110).GetAwaiter().GetResult();
            var wallet1 = _walletService.AddWallet(_user, "name1", "descr", "$", 110).GetAwaiter().GetResult();
            var transaction = _transactionService.AddTransaction(wallet, wallet1, 100, "a");
            var savedTransaction = _transactionService.GetTransaction(transaction.Id).GetAwaiter().GetResult();
            Assert.AreEqual(transaction.Id, savedTransaction.Id);
            Assert.Pass();
        }
    }
}