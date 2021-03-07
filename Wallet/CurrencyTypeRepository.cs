using System;
using System.Collections.Generic;

namespace MyWallet
{
    public class CurrencyTypeRepository
    {
        // List of all currencies with their properties.
        public static readonly Dictionary<string, Currency> Currencies =
            new Dictionary<string, Currency>()
            {
                {
                    "BTC", new Currency("BTC", true, "Bitcoin", "฿",
                        new Dictionary<string, CurrencySubType>()
                        {
                            {"mBTC", new CurrencySubType {_symbol = "mBTC "}}
                        }
                    )
                },
                {
                    "LTC", new Currency("LTC", true, "Litecoin", "L",
                        new Dictionary<string, CurrencySubType>()
                        {
                            {"mLTC", new CurrencySubType {_symbol = "mLTC "}}
                        }
                    )
                },
                {"AUD", new Currency("AUD", false, "Australian dollar", "$")},
                {"CAD", new Currency("CAD", false, "Canadian dollar", "$")},
                {"CNY", new Currency("CNY", false, "Renminbi", "¥")},
                {"EUR", new Currency("EUR", false, "Euro", "€")},
                {"GBP", new Currency("GBP", false, "Pound sterling", "£")},
                {"HKD", new Currency("HKD", false, "Hong Kong dollar", "HKD$")},
                {"IDR", new Currency("IDR", false, "Indonesian rupiah", "Rp")},
                {"INR", new Currency("INR", false, "Indian rupee", "Rs")},
                {"JPY", new Currency("JPY", false, "Japanese yen", "¥")},
                {"KRW", new Currency("KRW", false, "South Korean won", "₩")},
                {"MOP", new Currency("MOP", false, "Pataca", "MOP$")},
                {"NZD", new Currency("NZD", false, "New Zealand dollar", "$")},
                {"PHP", new Currency("PHP", false, "Philippine peso", "P")},
                {"RUB", new Currency("RUB", false, "Russian ruble", "PP")},
                {"SGD", new Currency("SGD", false, "Singapore dollar", "S$")},
                {"TWD", new Currency("TWD", false, "New Taiwan dollar", "$")},
                {"USD", new Currency("USD", false, "US dollar", "$")},
                {"VND", new Currency("VND", false, "Vietnamese dong", "₫")},
                {"ZAR", new Currency("ZAR", false, "South African rand", "R")}
            };

        public Currency Get(string id)
        {
            if (Currencies.ContainsKey(id))
            {
                return Currencies[id];
            }
            else
            {
                return null;
            }
        }

        public bool Exists(string id)
        {
            return Currencies.ContainsKey(id);
        }
    }
}