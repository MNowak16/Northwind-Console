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
                logger.Info("Validation passed");
            }
            while (isValid == false)
            {
                Console.WriteLine("Product name already exists.");
                Console.Write("Enter Product Name: ");
                product.ProductName = Console.ReadLine();
                isValid = Validate.isValidProductName(product.ProductName);
                Console.Clear();
            }

            //Get Qty per Unit
            Console.Write("Quantity per Unit: ");
            var quantityPerUnit = Console.ReadLine();

            //Get Unit Price
            Console.Write("Enter Unit Price: ");
            decimal unitPrice;
            string unitPriceStr = Console.ReadLine();
            while (!decimal.TryParse(unitPriceStr, out unitPrice))
            {
                Console.Write("Please enter valid unit price: ");
                unitPriceStr = Console.ReadLine();
            }

            //Get Category ID
            Supplier supplier = new Supplier();
            var db = new NorthwindContext();
            var categoryQuery = db.Categories.OrderBy(p => p.CategoryId);

            //ask user to select existing category
            Console.WriteLine();
            Console.WriteLine("List of Categories: ");
            foreach (var item in categoryQuery)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }
            Console.WriteLine();
            Console.Write("Select Cateogry ID from the list above: ");

            int CategoryID = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"CategoryId {CategoryID} selected");

            //Validate Category ID
            //
            //
            //
            //
            //

            //Get Supplier ID
            var supplierQuery = db.Suppliers.OrderBy(p => p.SupplierId);

            //ask user to select existing category
            Console.WriteLine();
            foreach (var item in supplierQuery)
            {
                Console.WriteLine($"{item.SupplierId}) {item.CompanyName}");
            }
            Console.WriteLine();
            Console.Write("Enter Supplier ID from the list above: ");
            int SupplierID = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"SupplierID {SupplierID} selected");

            //Validate Supplier ID
            //
            //
            //
            //
            //

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
                Console.WriteLine("Enter \"q\" to go back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine();
                logger.Info($"Option {choice} selected");

                if (choice == "1") { ProductDisplays.DisplayAll(); }
                else if (choice == "2") { ProductDisplays.DisplayActive(); }
                else if (choice == "3") { ProductDisplays.DisplayDiscontinued(); }
            } while (choice.ToLower() != "q");
        }

        public static void Edit()
        {
            string productChoice;
            string columnChoice;

            //Display all products with IDs
            ProductDisplays.DisplayAllWithIDs();

            Console.WriteLine("Enter the ID of the product you would like to edit:");
            productChoice = Console.ReadLine();
            Console.Clear();
            logger.Info($"Option {productChoice} selected");

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

            //user selects with product to update
            columnChoice = Console.ReadLine();
            Console.Clear();
            logger.Info($"Option {columnChoice} selected");

            if (columnChoice == "1") { ProductEdits.EditName(productChoice); }
            else if (columnChoice == "2") { ProductEdits.EditQtyPerUnit(productChoice); }
            else if (columnChoice == "3") { ProductEdits.EditUnitPrice(productChoice); }
            else if (columnChoice == "4") { ProductEdits.EditUnitsInStock(productChoice); }
            else if (columnChoice == "5") { ProductEdits.EditUnitsOnOrder(productChoice); }
            else if (columnChoice == "6") { ProductEdits.EditReorderLevel(productChoice); }
            else if (columnChoice == "7") { ProductEdits.EditDiscontinued(productChoice); }
            else if (columnChoice == "8") { ProductEdits.EditCategoryID(productChoice); }
            else if (columnChoice == "9") { ProductEdits.EditSupplierID(productChoice); }
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
            Console.Write("Select the product ID to be deleted: ");

            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"ProductId {id} selected");

            //get category
            Product product = db.Products.FirstOrDefault(c => c.ProductID == id);

            //delete category in database
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}