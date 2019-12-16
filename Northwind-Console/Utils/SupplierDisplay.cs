using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole.Utils
{
    class SupplierDisplay
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void DisplayWithIDs()
        {
            Supplier supplier = new Supplier();
            var db = new NorthwindContext();
            var query = db.Suppliers.OrderBy(p => p.CompanyName);

            Console.WriteLine("List of Suppliers: ");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.SupplierId}) {item.CompanyName}");
            }
            Console.WriteLine();
        }
    }
}
