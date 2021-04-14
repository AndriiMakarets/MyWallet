using System;
using Xunit;

namespace AV.ProgrammingWithCSharp.Budgets.Tests
{
    public class BudgetsTests
    {
        [Fact]
        public void StatisticsTest()
        {
            //arrange
            var budget = new Budget("");
            budget.AddTransaction(48.5);
            budget.AddTransaction(136.49);
            budget.AddTransaction(300);

            //act
            var result = budget.GetStatistics();
           
            //assert
            Assert.Equal(161.66, result.AverageTransaction, 2);
            Assert.Equal(300, result.HighestTransaction);
            Assert.Equal(48.5, result.LowestTransaction);
        }
    }
}
