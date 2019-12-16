using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;
using System.Data;

namespace NorthwindConsole.Utils
{
    class ProductDisplays
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void DisplayAll()
        {
            var db = new NorthwindContext();
            var query = db.Products.OrderBy(p => p.ProductName);
            Console.Clear();

            Console.WriteLine($"{query.Count()} record(s) returned");
            Console.WriteLine("------------------");
            foreach (var item in query)
            {
                if (item.Discontinued == true)
                {
                    Console.WriteLine($"{item.ProductName} - Discontinued");
                }
                else if (item.Discontinued == false)
                {
                    Console.WriteLine($"{item.ProductName} - Active");
                }
            }
            Console.WriteLine();
        }

        public static void DisplayAllWithIDs()
        {
            var db = new NorthwindContext();
            var query = db.Products.OrderBy(p => p.ProductName);
            Console.Clear();

            Console.WriteLine($"{query.Count()} record(s) returned");
            Console.WriteLine("------------------");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductID}) {item.ProductName}");
            }
            Console.WriteLine();
        }

        public static void DisplayActive()
        {
            var db = new NorthwindContext();
            var query = db.Products.OrderBy(p => p.ProductName).Where(p => p.Discontinued == false);
            Console.Clear();

            Console.WriteLine($"{query.Count()} record(s) returned");
            Console.WriteLine("------------------");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductName}");
            }
            Console.WriteLine();
        }

        public static void DisplayDiscontinued()
        {
            var db = new NorthwindContext();
            var query = db.Products.OrderBy(p => p.ProductName).Where(p => p.Discontinued == true);
            Console.Clear();

            Console.WriteLine($"{query.Count()} record(s) returned");
            Console.WriteLine("------------------");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductName}");
            }
            Console.WriteLine();
        }

        public static void DisplayProductDetails()
        {
            var db = new NorthwindContext();

            DisplayAllWithIDs();
            Console.WriteLine();
            Console.Write("Enter the ID of the product to see its details: ");
            int ProductID = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            var query = db.Products.OrderBy(p => p.ProductName).Where(p => p.ProductID == ProductID);    

            foreach (var item in query)
            {
                Console.WriteLine($"Product Name: {item.ProductName}");
                Console.WriteLine($"Quantity per Unit: {item.QuantityPerUnit}");
                Console.WriteLine($"Unit Price: ${item.UnitPrice}");
                Console.WriteLine($"Units in Stock: {item.UnitsInStock}");
                Console.WriteLine($"Units on Order: {item.UnitsOnOrder}");
                Console.WriteLine($"Reorder Level: {item.ReorderLevel}");
                Console.WriteLine($"Discontinued: {item.Discontinued}");
                Console.WriteLine($"Category ID: {item.CategoryId}");
                Console.WriteLine($"Supplier ID: {item.SupplierId}");
            }
            Console.WriteLine();
        }
    }
}
