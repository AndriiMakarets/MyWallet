using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWallet;
using System;

namespace Tests_Wallet
{
    [TestClass]
    public class WalletTests
    {
        [TestMethod]
        public void CustomerTestFields()
        {
            var cust1 = new Customer();

            cust1.FirstName = "Andrii";
            cust1.Email = "Andrii.makarets@ukma.udu.ua";
            cust1.LastName = "Makarets";

            Assert.AreEqual(cust1.FirstName, "Andrii");
            Assert.AreEqual(cust1.LastName, "Makarets");
            Assert.AreEqual(cust1.Email, "Andrii.makarets@ukma.udu.ua");
        }

        [TestMethod]
        public void CustomerCategory()
        {
            var cust1 = new Customer();

            cust1.CreateCategory();
            cust1.Categories[0].Colore = "blue";
            cust1.Categories[0].Name = "products";
            cust1.Categories[0].Description = "my first category";

            Assert.AreEqual(cust1.Categories[0].Colore , "blue");
            Assert.AreEqual(cust1.Categories[0].Name, "products");
            Assert.AreEqual(cust1.Categories[0].Description , "my first category");
        }

        [TestMethod]
        public void CustomerFriend()
        {
            var cust1 = new Customer();
            var cust2 = new Customer();

            cust1.AddWallet();
            cust1.Wallets[0].Name="myWallet";

            cust1.Wallets[0].AddTransaction(new Transaction(Guid.NewGuid(), 100, "home", "something",
                new DateTimeOffset(2021, 01, 14, 15, 0, 0, new TimeSpan(2, 0, 0)), new Currency("USD")));
                
                    
                cust1.AddFriend(cust2, cust1.Wallets[0]);

            cust2.FriendWallets[0].AddTransaction((new Transaction(Guid.NewGuid(), 200, "food", "something",
                new DateTimeOffset(2021, 01, 14, 15, 0, 0, new TimeSpan(2, 0, 0)), new Currency("USD"))));
        }
        }
    }
