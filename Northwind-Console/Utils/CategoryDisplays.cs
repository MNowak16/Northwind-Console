using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole
{
    class CategoryDisplays
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Display()
        {
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryName);

            Console.WriteLine($"{query.Count()} records returned");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryName} - {item.Description}");
            }
            Console.WriteLine();
        }

        public static void DisplaySelectedWithRelatedProducts()
        {
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryId);

            Console.WriteLine();
            Console.WriteLine("List of Categories: ");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }
            Console.Write("Select the category whose products you want to display from the list above: ");

            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"CategoryId {id} selected");
            Category category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
            Console.WriteLine($"{category.CategoryName} - {category.Description}");
            foreach (Product p in category.Products)
            {
                Console.WriteLine(p.ProductName);
            }
            Console.WriteLine();
        }

        public static void DisplayAllWithProducts()
        {
            var db = new NorthwindContext();
            var query = db.Categories.Include("Products").OrderBy(p => p.CategoryId);
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryName}");
                foreach (Product p in item.Products)
                {
                    Console.WriteLine($"\t{p.ProductName}");
                }
            }
            Console.WriteLine();
        }
    }
}
