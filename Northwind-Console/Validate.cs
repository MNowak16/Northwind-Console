using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole
{
    class Validate
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static bool isValidCateogryName(string CategoryName)
        {
            Category category = new Category();
            ValidationContext context = new ValidationContext(category, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(category, context, results, true);

            var db = new NorthwindContext();
            // check for unique name
            if (db.Categories.Any(c => c.CategoryName == CategoryName))
            {
                isValid = false;
                logger.Error($"Category name {CategoryName} already exists.");
            }
            else isValid = true;
            return isValid;
        }

        public static bool isValidProductName(string ProductName)
        {
            Product product = new Product();
            ValidationContext context = new ValidationContext(product, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, context, results, true);

            var db = new NorthwindContext();
            // check for unique name
            if (db.Products.Any(c => c.ProductName == ProductName))
            {
                isValid = false;
                logger.Error($"Category name {ProductName} already exists.");
            }
            else isValid = true;
            return isValid;
        }
    }
}
