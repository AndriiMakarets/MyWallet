using System;
using System.Collections.Generic;
using System.Text;

namespace MyWallet
{
   public class Customer
    {
        private Guid _guid;
        private string _lastName;
        private string _firstName;
        private string _email;
        private List<Wallet> _wallets;
        private List<IReadOnlyWallet> _friendWallets;
        private List<Category> _categories = new();


        public Guid Guid
        {
            get { return _guid; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        
        public Customer(Guid guid, string fname, string ldesc, string email)
        {
            _wallets = new List<Wallet>();
            _friendWallets = new List<IReadOnlyWallet>();
            _categories = new List<Category>();
            _guid = guid;
            _firstName = fname;
            _lastName = ldesc;
            _email = email;

        }
        public Customer()
        {
            _guid = Guid.NewGuid();
            _wallets = new List<Wallet>();
            _friendWallets = new List<IReadOnlyWallet>();
            _categories = new List<Category>();
    }

        public List<IReadOnlyWallet> FriendWallets => _friendWallets;
        public List<Wallet> Wallets => _wallets;

        public List<Category> Categories => _categories;

        public void AddWallet()
        {
            _wallets.Add(new Wallet(Guid.NewGuid()));
        }

        public void AddFriend(Customer fr, Wallet wallet)
        {
            fr.FriendWallets.Add(wallet);
        }

        public void CreateCategory()
        {
          // string name, description, colore;
          // name = Console.ReadLine();
          // description = Console.ReadLine();
          // colore = Console.ReadLine();
           _categories.Add(new Category());
        }

        public void AddCategoryToWallet(Wallet wallet, Category category)
        {
         wallet.Categories.Add(category);
        }
    }
}
