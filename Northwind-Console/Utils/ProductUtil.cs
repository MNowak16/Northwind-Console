using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;
using System.Data;
using NorthwindConsole.Utils;

namespace NorthwindConsole
{
    class ProductUtil
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Add()
        {
            Product product = new Product();

            //Get product name
            Console.Write("Enter Product Name: ");
            product.ProductName = Console.ReadLine();

            //Verify name is not a duplicate
            bool isValid = Validate.isValidProductName(product.ProductName);
            if (isValid == true)
            {
                logger.Info("Duplicate name validation passed");
            }
            while (isValid == false)
            {
                Console.WriteLine("Product name already exists.");
                Console.Write("Enter Product Name: ");
                product.ProductName = Console.ReadLine();
                isValid = Validate.isValidProductName(product.ProductName);
            }

            //Get Qty per Unit
            Console.Write("Quantity per Unit: ");
            var quantityPerUnit = Console.ReadLine();

            //Get Unit Price
            Console.Write("Enter Unit Price: ");
            decimal unitPrice;
            string unitPriceStr = Console.ReadLine();
            //Validate that user input is a decimal
            while (!decimal.TryParse(unitPriceStr, out unitPrice))
            {
                Console.Write("Enter valid unit price: ");
                unitPriceStr = Console.ReadLine();
            }

            //Get Category ID
            Supplier supplier = new Supplier();
            var db = new NorthwindContext();
            var categoryQuery = db.Categories.OrderBy(p => p.CategoryName);

            //ask user to select existing category
            Console.WriteLine();
            Console.WriteLine("List of Categories: ");
            CategoryDisplays.DisplayWithIDs();
            Console.WriteLine();
            Console.Write("Enter Cateogry ID from the list above: ");

            int CategoryID = int.Parse(Console.ReadLine());

            //Validate Category ID
            bool isValidCateogryID = Validate.isValidCategoryID(CategoryID);
            if (isValidCateogryID == true)
            {
                logger.Info("Valid Category ID validation passed");
            }
            while (isValidCateogryID == false)
            {
                Console.Clear();
                Console.WriteLine("That is not a valid Category ID.");

                Console.WriteLine();
                CategoryDisplays.DisplayWithIDs();
                Console.Write("Enter Category ID from the list above: ");
                CategoryID = int.Parse(Console.ReadLine());
                isValidCateogryID = Validate.isValidCategoryID(CategoryID);
                Console.Clear();
            }
            Console.Clear();
            logger.Info($"CategoryId {CategoryID} entered");

            //Get Supplier ID
            var supplierQuery = db.Suppliers.OrderBy(p => p.CompanyName);

            //ask user to select existing supplier
            Console.WriteLine();
            Console.WriteLine("List of Suppliers: ");
            SupplierDisplay.DisplayWithIDs();
            Console.WriteLine();
            Console.Write("Enter Supplier ID from the list above: ");
            int SupplierID = int.Parse(Console.ReadLine());

            //Validate Supplier ID
            bool isValidSupplierID = Validate.isValidSupplierID(SupplierID);
            if (isValidSupplierID == true)
            {
                logger.Info("Valid Supplier ID validation passed");
            }
            while (isValidSupplierID == false)
            {
                Console.Clear();
                Console.WriteLine("That is not a valid Supplier ID.");

                Console.WriteLine();
                Console.WriteLine("List of Suppliers: ");
                SupplierDisplay.DisplayWithIDs();
                Console.WriteLine();
                Console.Write("Enter Cateogry ID from the list above: ");
                CategoryID = int.Parse(Console.ReadLine());
                isValidCateogryID = Validate.isValidCategoryID(CategoryID);
                Console.Clear();
            }
            Console.Clear();
            logger.Info($"SupplierID {SupplierID} entered");

            //Add New Product
            var newProduct = new Product
            {
                ProductName = product.ProductName,
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

        public static void DisplayMenu()
        {
            string choice;
            do
            {
                Console.WriteLine("1) Display All Products");
                Console.WriteLine("2) Display Active Products");
                Console.WriteLine("3) Display Discontinued Products");
                Console.WriteLine("4) Display Details for a Specific Product");
                Console.WriteLine("Enter \"q\" to go back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine();
                logger.Info($"Option {choice} entered");

                if (choice == "1") { ProductDisplays.DisplayAll(); }
                else if (choice == "2") { ProductDisplays.DisplayActive(); }
                else if (choice == "3") { ProductDisplays.DisplayDiscontinued(); }
                else if (choice == "4") { ProductDisplays.DisplayProductDetails(); }
            } while (choice.ToLower() != "q");
            Console.Clear();
        }

        public static void Edit()
        {
            string productChoice;
            string columnChoice;

            //Display all products with IDs
            ProductDisplays.DisplayAllWithIDs();

            Console.Write("Enter the ID of the product you would like to edit: ");
            productChoice = Console.ReadLine();
            Console.Clear();
            logger.Info($"Option {productChoice} entered");

            do
            {
                //user selects which field to update
                Console.WriteLine("1) Product Name");
                Console.WriteLine("2) Quantity Per Unit");
                Console.WriteLine("3) Unit Price");
                Console.WriteLine("4) Units in Stock");
                Console.WriteLine("5) Units on Order");
                Console.WriteLine("6) Reorder Level");
                Console.WriteLine("7) Discontinued");
                Console.WriteLine("8) Category ID");
                Console.WriteLine("9) Supplier ID");
                Console.WriteLine();
                Console.WriteLine("Enter \"q\" to go back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter the number for the corresponding field to edit: ");

                //user selects with product to update
                columnChoice = Console.ReadLine();
                Console.Clear();
                logger.Info($"Option {columnChoice} entered");

                if (columnChoice == "1") { ProductEdits.EditName(productChoice); }
                else if (columnChoice == "2") { ProductEdits.EditQtyPerUnit(productChoice); }
                else if (columnChoice == "3") { ProductEdits.EditUnitPrice(productChoice); }
                else if (columnChoice == "4") { ProductEdits.EditUnitsInStock(productChoice); }
                else if (columnChoice == "5") { ProductEdits.EditUnitsOnOrder(productChoice); }
                else if (columnChoice == "6") { ProductEdits.EditReorderLevel(productChoice); }
                else if (columnChoice == "7") { ProductEdits.EditDiscontinued(productChoice); }
                else if (columnChoice == "8") { ProductEdits.EditCategoryID(productChoice); }
                else if (columnChoice == "9") { ProductEdits.EditSupplierID(productChoice); }
                Console.Clear();
            } while (columnChoice.ToLower() != "q");
            Console.Clear();
        }

        public static void Delete()
        {
            //get context                        
            var db = new NorthwindContext();
            var query = db.Products.OrderBy(p => p.ProductName);

            //ask user to select existing category
            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductID}) {item.ProductName}");
            }
            Console.WriteLine();
            Console.Write("Enter the product ID to be deleted: ");

            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"ProductId {id} entered");

            //get category
            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            //delete category in database
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}