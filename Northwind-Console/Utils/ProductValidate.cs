using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole.Utils
{
    class ProductValidate
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        public static int ValidateSupplier()
        {
            Product product = new Product();
            var db = new NorthwindContext();
            var query = db.Suppliers.OrderBy(p => p.SupplierId);

            //ask user to select existing category
            Console.WriteLine("Select Supplier ID from the list below:");
            foreach (var item in query)
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

            return product.SupplierId;
        }
    }
}
