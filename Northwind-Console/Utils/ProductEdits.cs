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
            string ans = Console.ReadLine();
            //validate answer is a number
            decimal newUnitPrice;
            while (!decimal.TryParse(ans, out newUnitPrice))
            {
                Console.Write("That is an invalid response. Please enter the new unit price: ");
                ans = Console.ReadLine();
                logger.Error("Invalid input (decimal): {Answer}", ans);
            }

            Console.WriteLine();
            logger.Info($"User changed price to: {newUnitPrice}");

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
            string ans = Console.ReadLine();
            //validate answer is a number
            decimal newUnitsInStock;
            while (!decimal.TryParse(ans, out newUnitsInStock))
            {
                Console.Write("That is an invalid response. Please enter the new units in stock: ");
                ans = Console.ReadLine();
                logger.Error("Invalid input (decimal): {Answer}", ans);
            }
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
            string ans = Console.ReadLine();
            //validate answer is a number
            decimal newUnitsOnOrder;
            while (!decimal.TryParse(ans, out newUnitsOnOrder))
            {
                Console.Write("That is an invalid response. Please enter the new units on order: ");
                ans = Console.ReadLine();
                logger.Error("Invalid input (decimal): {Answer}", ans);
            }
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
            string ans = Console.ReadLine();
            //validate answer is a number
            decimal newReorderLevel;
            while (!decimal.TryParse(ans, out newReorderLevel))
            {
                Console.Write("That is an invalid response. Please enter the new reorder level: ");
                ans = Console.ReadLine();
                logger.Error("Invalid input (decimal): {Answer}", ans);
            }
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

        public static void EditCategoryID(string productChoice)
        {
             var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);
            var catQuery = db.Categories;

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing Cateogry ID: {query.FirstOrDefault().CategoryId}");
            Console.WriteLine();

            //Display categories with IDs
            Console.WriteLine("List of Categories:");
            foreach (var item in catQuery)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }
            Console.WriteLine();
            Console.Write("Please enter the new Category ID from the list above: ");

            string newCategoryID = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newCategoryID}");

            product.CategoryId = Convert.ToInt32(newCategoryID);
            db.SaveChanges();
        }

        public static void EditSupplierID(string productChoice)
        {
            var db = new NorthwindContext();

            //validates user input
            int id = int.Parse(productChoice);
            var query = db.Products.Where(p => p.ProductID == id);
            var supQuery = db.Suppliers.OrderBy(p => p.CompanyName);

            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            Console.WriteLine($"Existing Supplier ID: {query.FirstOrDefault().SupplierId}");
            Console.WriteLine();

            //Display suppliers with IDs
            Console.WriteLine("List of Suppliers:");
            foreach (var item in supQuery)
            {
                Console.WriteLine($"{item.SupplierId}) {item.CompanyName}");
            }
            Console.WriteLine();
            Console.Write("Please enter the new Supplier ID from the list above: ");

            string newSupplierID = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newSupplierID}");

            product.SupplierId = Convert.ToInt32(newSupplierID);
            db.SaveChanges();
        }
    }
}
