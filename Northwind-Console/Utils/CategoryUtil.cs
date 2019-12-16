using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole.Utils
{
    class CategoryUtil
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void DisplayMenu()
        {
            string choice;
            do
            {
                Console.WriteLine("1) Display All Categories");
                Console.WriteLine("2) Display Category and related products");
                Console.WriteLine("3) Display all Categories and their related products");
                Console.WriteLine("Enter \"q\" to go back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine();
                Console.Clear();
                logger.Info($"Option {choice} entered");

                if (choice == "1") { CategoryDisplays.DisplayWithDescription(); }
                else if (choice == "2") { CategoryDisplays.DisplaySelectedWithRelatedProducts(); }
                else if (choice == "3") { CategoryDisplays.DisplayAllWithProducts(); }
            } while (choice.ToLower() != "q");
            Console.Clear();
        }

        public static void Add()
        {
            var db = new NorthwindContext();

            Category category = new Category();
            Console.Write("Enter a Category Name: ");
            category.CategoryName = Console.ReadLine();

            //Verify name is not a duplicate
            bool isValid = Validate.isValidCateogryName(category.CategoryName);
            if (isValid == true)
            {
                logger.Info("Category name validation passed");
            }
            while (isValid == false)
            {
                Console.WriteLine("Category name already exists.");
                Console.Write("Enter Category Name: ");
                category.CategoryName = Console.ReadLine();
                isValid = Validate.isValidCateogryName(category.CategoryName);
            }

            Console.Write("Enter the Category Description: ");
            category.Description = Console.ReadLine();

            db.Categories.Add(category);
            db.SaveChanges();

            Console.Clear();
        }

        public static void EditName()
        {
            //get context                        
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryId);

            //ask user to select existing category
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }
            Console.WriteLine();
            Console.Write("Enter the Category ID to edit: ");

            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"CategoryId {id} entered");

            //get category
            Category category = db.Categories.FirstOrDefault(c => c.CategoryId == id);

            //ask user to provide updated name
            Console.Write("Enter the new category name: ");
            var newName = Console.ReadLine();
            logger.Info($"User entered {newName}");
            
            //update category in database
            category.CategoryName = newName;
            db.SaveChanges();

            Console.Clear();
        }

        public static void Delete()
        {
            //get context                        
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryId);

            //ask user to select existing category
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }
            Console.WriteLine();
            Console.Write("Enter the Category ID to be deleted: ");

            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"CategoryId {id} entered");

            //get category
            Category category = db.Categories.FirstOrDefault(c => c.CategoryId == id);

            //delete category in database
            db.Categories.Remove(category);
            db.SaveChanges();
        }
    }
}
