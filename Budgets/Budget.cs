using System;
using System.Collections.Generic;

namespace AV.ProgrammingWithCSharp.Budgets
{
    public class Budget
    {
        private List<double> transactions;
        private string name;

        public Budget(string name)
        {
            transactions = new List<double>();
            this.name = name;
        }


        public void AddTransaction(double transaction)
        {
            transactions.Add(transaction);
        }

        public Statistics GetStatistics()
        {           
            var result = new Statistics();
            result.LowestTransaction = 9999.99;

            foreach (double transaction in transactions)
            {
                result.HighestTransaction = Math.Max(transaction, result.HighestTransaction);
                result.LowestTransaction = Math.Min(transaction, result.LowestTransaction);
                result.AverageTransaction += transaction;
            }
            result.AverageTransaction /= transactions.Count;
            return result;
        }
    }
}
