using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;
using System.Data;

namespace NorthwindConsole
{
    class ProductUtil
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Add()
        {
            //Add  new product
            //Get product name
            Console.WriteLine("Enter Product Name:");
            var productName = Console.ReadLine();

            //Get Qty per Unit
            Console.WriteLine("Quantity per Unit:");
            var quantityPerUnit = Console.ReadLine();

            //Get Unit Price
            Console.WriteLine("Enter Unit Price:");
            int unitPrice;
            string unitPriceStr = Console.ReadLine();
            while (!int.TryParse(unitPriceStr, out unitPrice))
            {
                Console.WriteLine("Please enter valid unit price");
                unitPriceStr = Console.ReadLine();
            }

            //Get Category ID
            Product product = new Product();
            var db = new NorthwindContext();
            var categoryQuery = db.Categories.OrderBy(p => p.CategoryId);

            //ask user to select existing category
            Console.WriteLine("Select Cateogry ID from the list below:");
            foreach (var item in categoryQuery)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }

            int CategoryID = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"CategoryId {CategoryID} selected");

            //get category
            Category category = db.Categories.FirstOrDefault(c => c.CategoryId == CategoryID);

            ValidationContext CategoryContext = new ValidationContext(product, null, null);
            List<ValidationResult> CategoryResults = new List<ValidationResult>();

            var CategoryIsValid = Validator.TryValidateObject(product, CategoryContext, CategoryResults, true);
            if (CategoryIsValid)
            {
                var db2 = new NorthwindContext();
                // check for unique name
                if (db2.Products.Any(c => c.ProductName == product.ProductName))
                {
                    // generate validation error
                    CategoryIsValid = false;
                    CategoryResults.Add(new ValidationResult("Name exists", new string[] { "ProductName" }));
                }
                else
                {
                    logger.Info("Validation passed");
                }
            }
            if (!CategoryIsValid)
            {
                foreach (var result in CategoryResults)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
            }

            //Get Supplier ID
            var supplierQuery = db.Suppliers.OrderBy(p => p.SupplierId);

            //ask user to select existing category
            Console.WriteLine("Select Supplier ID from the list below:");
            foreach (var item in supplierQuery)
            {
                Console.WriteLine($"{item.SupplierId}) {item.CompanyName}");
            }

            int SupplierID = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"SupplierID {SupplierID} selected");

            //get category
            Supplier supplier = db.Suppliers.FirstOrDefault(c => c.SupplierId == SupplierID);

            ValidationContext context = new ValidationContext(supplier, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, context, results, true);
            if (isValid)
            {
                var db2 = new NorthwindContext();
                // check for unique name
                if (db2.Suppliers.Any(c => c.CompanyName == supplier.CompanyName))
                {
                    // generate validation error
                    isValid = false;
                    results.Add(new ValidationResult("Company exists", new string[] { "CompanyName" }));
                }
                else
                {
                    logger.Info("Validation passed");
                }
            }
            if (!isValid)
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
            }

            //Add New Product
            var newProduct = new Product
            {
                ProductName = productName,
                QuantityPerUnit = quantityPerUnit,
                UnitPrice = unitPrice,
                UnitsInStock = 0,
                UnitsOnOrder = 0,
                ReorderLevel = 0,
                //auto set Discontinued to 0 - Assume all new products are not discontinued
                Discontinued = Convert.ToBoolean(0),
                CategoryId = CategoryID,
                SupplierId = SupplierID
            };

            db.Products.Add(newProduct);
            db.SaveChanges();
        }

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

        public static void DisplayMenu()
        {
            string choice;
            do
            {
                Console.WriteLine("1) Display All Products");
                Console.WriteLine("2) Display Active Products");
                Console.WriteLine("3) Display Discontinued Products");
                Console.WriteLine("Enter \"q\" to go back to Main Menu");
                choice = Console.ReadLine();
                logger.Info($"Option {choice} selected");

                if (choice == "1")
                {
                    ProductUtil.DisplayAll();
                }

                else if (choice == "2")
                {
                    ProductUtil.DisplayActive();
                }

                else if (choice == "3")
                {
                    ProductUtil.DisplayDiscontinued();
                }
            } while (choice.ToLower() != "q");
        }

        public static void Edit()
        {
            string productChoice;
            string columnChoice;
            do
            {
                //Display all products with IDs
                ProductUtil.DisplayAllWithIDs();

                Console.WriteLine("Enter the ID of the product you would like to edit:");
                productChoice = Console.ReadLine();
                Console.Clear();
                logger.Info($"Option {productChoice} selected");

                //user selects with product to update
                //user selects which field to update
                //validates user input
                //update field in database
                //save record

                //user selects which field to update
                Console.WriteLine("Select which property to edit:");
                Console.WriteLine("1) Product Name");
                Console.WriteLine("2) Quantity Per Unit");
                Console.WriteLine("3) Unit Price");
                Console.WriteLine("4) Units in Stock");
                Console.WriteLine("5) Units on Order");
                Console.WriteLine("6) Reorder Level");
                Console.WriteLine("7) Discontinued");
                Console.WriteLine("8) Category ID");
                Console.WriteLine("9) Supplier ID");
                Console.WriteLine("Enter \"q\" to go back to Main Menu");

                columnChoice = Console.ReadLine();
                Console.Clear();
                logger.Info($"Option {columnChoice} selected");

                if (columnChoice == "1")
                {
                    Product product = new Product();
                    var db = new NorthwindContext();

                    int id = int.Parse(productChoice);
                    var query = db.Products.Where(p => p.ProductID == id)
                                        .Select(p => product.ProductName);

                    Console.WriteLine($"Existing name: {query.FirstOrDefault()}");
                    Console.WriteLine("Please enter the new name: ");
                    string newName = Console.ReadLine();
                    Console.WriteLine();
                    logger.Info($"User Entered: {newName}");

                    product.ProductName = newName;
                    db.SaveChanges();
                }

                else if (columnChoice == "2")
                {

                }

                else if (columnChoice == "3")
                {

                }
            } while (columnChoice.ToLower() != "q");
        }
    }
}