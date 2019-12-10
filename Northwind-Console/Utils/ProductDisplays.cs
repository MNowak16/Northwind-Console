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

            Console.WriteLine($"{query.Count()} records returned");
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

            Console.WriteLine($"{query.Count()} records returned");
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

            Console.WriteLine($"{query.Count()} records returned");
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

            Console.WriteLine($"{query.Count()} records returned");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductName}");
            }
            Console.WriteLine();
        }




    }
}
