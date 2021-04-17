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
    }
}