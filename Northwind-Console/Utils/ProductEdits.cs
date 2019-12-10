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
    class ProductEdits
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void EditName(string productChoice)
        {
            var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing name: {query.FirstOrDefault().ProductName}");
            Console.Write("Please enter the new name: ");
            string newName = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newName}");

            product.ProductName = newName;
            db.SaveChanges();
        }

        public static void EditQtyPerUnit(string productChoice)
        {
            var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing quantity per unit: {query.FirstOrDefault().QuantityPerUnit}");
            Console.Write("Please enter the new quantity per unit: ");
            string newQtyPerUnit = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newQtyPerUnit}");

            product.QuantityPerUnit = newQtyPerUnit;
            db.SaveChanges();
        }

        public static void EditUnitPrice(string productChoice)
        {
            var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing unit price: {query.FirstOrDefault().UnitPrice}");
            Console.Write("Please enter the new unit price: ");
            string newUnitPrice = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newUnitPrice}");

            product.UnitPrice = Convert.ToDecimal(newUnitPrice);
            db.SaveChanges();
        }

        public static void EditUnitsInStock(string productChoice)
        {
            var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing units in stock: {query.FirstOrDefault().UnitsInStock}");
            Console.Write("Please enter the new units in stock: ");
            string newUnitsInStock = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newUnitsInStock}");

            product.UnitsInStock = Convert.ToInt16(newUnitsInStock);
            db.SaveChanges();
        }

        public static void EditUnitsOnOrder(string productChoice)
        {
            var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing units on order: {query.FirstOrDefault().UnitsOnOrder}");
            Console.Write("Please enter the new units on order: ");
            string newUnitsOnOrder = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newUnitsOnOrder}");

            product.UnitsOnOrder = Convert.ToInt16(newUnitsOnOrder);
            db.SaveChanges();
        }

        public static void EditReorderLevel(string productChoice)
        {
            var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing reorder level: {query.FirstOrDefault().ReorderLevel}");
            Console.Write("Please enter the new reorder level: ");
            string newReorderLevel = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newReorderLevel}");

            product.ReorderLevel = Convert.ToInt16(newReorderLevel);
            db.SaveChanges();
        }

        public static void EditDiscontinued(string productChoice)
        {
            var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing discontinued flag: {query.FirstOrDefault().Discontinued}");
            Console.Write("Please enter the new discontinued flag: ");
            string newDiscontinued = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newDiscontinued}");

            product.Discontinued = Convert.ToBoolean(newDiscontinued);
            db.SaveChanges();
        }
    }
}
