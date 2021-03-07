using System;
using System.Collections.Generic;
using System.Text;

namespace MyWallet
{

    public interface IReadOnlyWallet
    {
        public Guid Guid { get; }
        public decimal CurrentBalance { get; }
        public decimal InitialBalance { get; }
        public string Name { get; }
        public string Description { get; }
        public Type MoneyCurrency { get; }
        public Guid CustomerGuid { get; }
        public void AddTransaction(Transaction tr);
        public void ShowTransactions(int from);
        public void ShowTransactions();
    }

    public class Wallet : IReadOnlyWallet
    {
        private Guid _guid;
        private string _name;
        private decimal _initialBalance;
        private string _description;
        private Type _moneyCurrency;
        private Guid _customerGuid;
        private List<Transaction> _transactions;
        private decimal _currentBalance;
        private List<Category> _categories;

        public Guid Guid
        {
            get { return _guid; }
        }

        public decimal CurrentBalance
        {
            get { return _currentBalance; }
            set { _currentBalance = value; }
        }
        public decimal InitialBalance
        {
            get { return _initialBalance; }
            set { _initialBalance = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Type MoneyCurrency
        {
            get { return _moneyCurrency; }
            set { _moneyCurrency = value; }
        }

        public Guid CustomerGuid
        {
            get { return _customerGuid; }
            set { _customerGuid = value; }
        }

         public List<Category> Categories{ get; set;}

        public Wallet(Guid custGuid)
        {
            _guid = Guid.NewGuid();
            _customerGuid = custGuid;
            _transactions = new List<Transaction>();
            _categories = new List<Category>();
        }

        public Wallet(Guid guid, string name, string desc, Type curr, Guid custGuid)
        {
            _guid = guid;
            _customerGuid = custGuid;
            _name = name;
            _description = desc;
            _moneyCurrency = curr;
            _transactions = new List<Transaction>();
            _categories = new List<Category>();
        }

        public void AddTransaction(Transaction tr)
        {

            _transactions.Add(tr);
            _currentBalance += tr.MoneySum;
        }

        public void ShowTransactions(int from)
        {
            for (int i=from; i<from+10; i++)
            {
                Console.WriteLine(_transactions[i].ToString());
            }
        }

        public void ShowTransactions()
        {
            for (int i = 0; i <10; i++)
            {
                Console.WriteLine(_transactions[i].ToString());
            }
        }

        public void DeleteTransaction(Guid trId)
        {
           for(int i = 0; i < _transactions.Count; i++)
            {
                if (_transactions[i].Guid == trId)
                    _transactions[i] = null;
            }
        }

        public decimal SumOfIncome()
            {
            decimal res=0;
            for(int i = 0; i < _transactions.Count; i++)
            {
                if (_transactions[i].TransactionTime.AddMonths(1) > DateTime.Now)
                {
                    if (_transactions[i].MoneySum > 0)
                        res += _transactions[i].MoneySum;
                }
            }
            return res;
            }

        public decimal SumOfExpense()

            {
            decimal res=0;
            for(int i = 0; i < _transactions.Count; i++)
            {
                if (_transactions[i].TransactionTime.AddMonths(1) > DateTime.Now)
                {
                    if (_transactions[i].MoneySum < 0)
                        res += _transactions[i].MoneySum;
                }
            }
            return res;
            }
    }
}
