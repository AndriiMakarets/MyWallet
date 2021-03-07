using System;
using System.Collections.Generic;
using System.Text;

namespace MyWallet
{
    public class Currency : IEquatable<Currency>
    {
        public readonly string _id;
        public readonly bool _isDigital;
        public readonly string _currencyName;
        public readonly string _symbol;


        public readonly Dictionary<string, CurrencySubType> SubTypes;
        public CurrencySubType DisplayingSubType { get; private set; }
        private CurrencyTypeRepository _repo = new CurrencyTypeRepository();

        public Currency(string id)
        {
            if (!_repo.Exists(id))
            {
                throw new ArgumentException("Invalid ISO Currency Code");
            }

            var newCurrency = _repo.Get(id);
            _id = newCurrency._id;
            _isDigital = newCurrency._isDigital;
            _currencyName = newCurrency._currencyName;
            _symbol = newCurrency._symbol;

        }

        public Currency(string isoCode, bool isDigital, string generalName, string symbol)
        {
            _id = isoCode;
            _isDigital = isDigital;
            _currencyName = generalName;
            _symbol = symbol;

        }

        public Currency(string id, bool isDigital, string name, string symbol, Dictionary<string, CurrencySubType> subtypes)
        {
            _id = id;
            _isDigital = isDigital;
            _currencyName = name;
            _symbol = symbol;

            SubTypes = subtypes;
        }

        #region Equals and !Equals
        public static bool operator ==(Currency a, Currency b) { return Equals(a, b); }

        public static bool operator !=(Currency a, Currency b) { return !Equals(a, b); }

        public bool Equals(Currency other)
        {
            if (ReferenceEquals(null, other)) 
                return false;
            if (ReferenceEquals(this, other)) 
                return true;

            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;
            else if (ReferenceEquals(this, obj)) 
                return true;
            else if(obj.GetType() != typeof(Currency)) 
                return false;

            return Equals((Currency)obj);
        }

        #endregion

        public CurrencySubType GetSubType(string key)
        {
            if (SubTypes != null)
            {
                if (SubTypes.ContainsKey(key))
                    return SubTypes[key];
            }

            return null;
        }

        public void DisplayAsSubType(string key)
        {
            DisplayingSubType = GetSubType(key);
        }

        public override string ToString() { return this._id + _symbol; }
        public override int GetHashCode() { return _id.GetHashCode(); }
    }
}
