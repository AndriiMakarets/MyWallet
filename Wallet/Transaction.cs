using System;
using System.Collections.Generic;
using System.Text;
//using System.Drawing;

namespace MyWallet
{
    public class Transaction
    {
        private Guid _guid;
        private decimal _moneySum;
        private string _category;
        private string _description;
        private Currency _moneyCurrency;
        private DateTimeOffset _transactionTime;
        private string _fileOfTransaction;

        //До транзакції додатково можна прикріплювати файли (зображення або текстові).
        //використати bitmap?
        ////  List<Image> imageList = new List<Image>();
        ////  imageList.Add(Bitmap.FromFile(YourFilePath));


        public Guid Guid
        {
            get { return _guid; }
        }

        public decimal MoneySum
        {
            get { return _moneySum; }
            set { _moneySum = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Currency MoneyCurrency
        {
            get { return _moneyCurrency; }
            set { _moneyCurrency = value; }
        }

        public DateTimeOffset TransactionTime
        {
            get { return _transactionTime; }
            set { _transactionTime = value; }
        }

        public string FileOfTransaction
        {
            get { return _fileOfTransaction; }
            set { _fileOfTransaction = value; }
        }

        public Transaction()
        {
            _guid = Guid.NewGuid();
        }

        public Transaction(Guid guid, decimal moneySum, string category, string description, DateTimeOffset transactionTime, Currency moneyCurrency, string file)
        {
            _guid = guid;
            _moneySum = moneySum;
            _category = category;
            _description = description;
            _moneyCurrency = moneyCurrency;
            _transactionTime = transactionTime;
            _fileOfTransaction = file;
        }

        public Transaction(Guid guid, decimal moneySum, string category, string description, DateTimeOffset transactionTime, Currency moneyCurrency)
        {
            _guid = guid;
            _moneySum = moneySum;
            _category = category;
            _description = description;
            _moneyCurrency = moneyCurrency;
            _transactionTime = transactionTime;

        }

        public override string ToString()
        {
            return $"Category: {_category} Sum: {_moneySum} Currency: {_moneyCurrency} Time: {_transactionTime} Description: {_description}";
        }
    }
}