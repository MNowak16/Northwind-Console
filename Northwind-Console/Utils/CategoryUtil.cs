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
                logger.Info($"Option {choice} selected");

                if (choice == "1") { CategoryDisplays.Display(); }
                else if (choice == "2") { CategoryDisplays.DisplaySelectedWithRelatedProducts(); }
                else if (choice == "3") { CategoryDisplays.DisplayAllWithProducts(); }
            } while (choice.ToLower() != "q");
        }

        public static void Add()
        {
            Category category = new Category();
            Console.Write("Enter Category Name: ");
            category.CategoryName = Console.ReadLine();
            Console.Write("Enter the Category Description: ");
            category.Description = Console.ReadLine();

            ValidationContext context = new ValidationContext(category, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(category, context, results, true);
            if (isValid)
            {
                var db = new NorthwindContext();
                // check for unique name
                if (db.Categories.Any(c => c.CategoryName == category.CategoryName))
                {
                    // generate validation error
                    isValid = false;
                    results.Add(new ValidationResult("Name exists", new string[] { "CategoryName" }));
                }
                else
                {
                    logger.Info("Validation passed");
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
            }
            if (!isValid)
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
            }
        }

        public static void EditName()
        {
            //get context                        
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryId);

            //ask user to select existing category
            Console.WriteLine("Select the category ID you want to edit:");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }

            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"CategoryId {id} selected");

            //get category
            Category category = db.Categories.FirstOrDefault(c => c.CategoryId == id);

            //ask user to provide updated name
            Console.WriteLine("Enter the new Category Name");
            var newName = Console.ReadLine();
            logger.Info($"User entered {newName}");

            //update category in database
            category.CategoryName = newName;
            db.SaveChanges();
        }

        public static void Delete()
        {
            //get context                        
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryId);

            //ask user to select existing category
            Console.WriteLine("Select the category ID to be deleted:");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }

            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            logger.Info($"CategoryId {id} selected");

            //get category
            Category category = db.Categories.FirstOrDefault(c => c.CategoryId == id);

            //delete category in database
            db.Categories.Remove(category);
            db.SaveChanges();
        }
    }
}
