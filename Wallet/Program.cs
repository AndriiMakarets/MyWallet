using System;

namespace MyWallet
{
    public class Program
    {
        static void Main(string[] args)
        {

            var cust1 = new Customer();

            cust1.CreateCategory();
            cust1.Categories[0].Colore = "blue";
            cust1.Categories[0].Name = "products";
            cust1.Categories[0].Description = "my first category";


        }
    }
}
