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
            Console.Write("Enter the new name: ");
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
            Console.Write("Enter the new quantity per unit: ");
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
            Console.Write("Enter the new unit price: ");
            string ans = Console.ReadLine();
            //validate answer is a number
            decimal newUnitPrice;
            while (!decimal.TryParse(ans, out newUnitPrice))
            {
                Console.Write("That is an invalid response. Enter the new unit price: ");
                ans = Console.ReadLine();
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
            Console.Write("Enter the new units in stock: ");
            string ans = Console.ReadLine();
            //validate answer is a number
            decimal newUnitsInStock;
            while (!decimal.TryParse(ans, out newUnitsInStock))
            {
                Console.WriteLine("That is an invalid response.");
                Console.Write("Enter the new units in stock: ");
                ans = Console.ReadLine();
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
            Console.Write("Enter the new units on order: ");
            string ans = Console.ReadLine();
            //validate answer is a number
            decimal newUnitsOnOrder;
            while (!decimal.TryParse(ans, out newUnitsOnOrder))
            {
                Console.WriteLine("That is an invalid response.");
                Console.Write("Enter the new units on order: ");
                ans = Console.ReadLine();
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
            Console.Write("Enter the new reorder level: ");
            string ans = Console.ReadLine();
            //validate answer is a number
            decimal newReorderLevel;
            while (!decimal.TryParse(ans, out newReorderLevel))
            {
                Console.WriteLine("That is an invalid response.");
                Console.Write("Enter the new reorder level: ");
                ans = Console.ReadLine();
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
            Console.Write("Enter the new discontinued flag (True/False): ");
            string ans = Console.ReadLine();
            //validate answer is a bool
            bool newDiscontinued;
            while (!bool.TryParse(ans, out newDiscontinued))
            {
                Console.WriteLine("That is an invalid response.");
                Console.Write("Enter the new discontinued flag (True/False): ");
                ans = Console.ReadLine();
            }
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
            CategoryDisplays.DisplayWithIDs();
            Console.WriteLine();
            Console.Write("Enter the new Category ID from the list above: ");

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
            SupplierDisplay.DisplayWithIDs();
            Console.WriteLine();
            Console.Write("Enter the new Supplier ID from the list above: ");

            string newSupplierID = Console.ReadLine();
            Console.WriteLine();
            logger.Info($"User Entered: {newSupplierID}");

            product.SupplierId = Convert.ToInt32(newSupplierID);
            db.SaveChanges();
        }
    }
}
