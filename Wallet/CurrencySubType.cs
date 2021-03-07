using System;

namespace MyWallet
{
    public class CurrencySubType // different types of internet currencies
    {
        public string _symbol;

        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }
    }
}
