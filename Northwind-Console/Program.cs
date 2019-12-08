using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using NLog;
using NorthwindConsole.Models;
using NorthwindConsole.Utils;

namespace NorthwindConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                string choice;
                do
                {
                    Console.WriteLine("1) Display Categories");
                    Console.WriteLine("2) Add Category");
                    Console.WriteLine("3) Display Category and related products");
                    Console.WriteLine("4) Display all Categories and their related products");
                    Console.WriteLine("5) Edit Category Name");
                    Console.WriteLine("6) Delete Category");
                    Console.WriteLine("7) Display Products");
                    Console.WriteLine("8) Add New Product");
                    Console.WriteLine("9) Edit Product");
                    Console.WriteLine("\"q\" to quit");
                    choice = Console.ReadLine();
                    Console.Clear();
                    logger.Info($"Option {choice} selected");
                    if (choice == "1")
                    {
                        CategoryUtil.Display();
                    } 
                    else if (choice == "2")
                    {
                        CategoryUtil.Add();
                    }
                    else if (choice == "3")
                    {
                        CategoryUtil.DisplaySelectedWithRelatedProducts();
                    }
                    else if (choice == "4")
                    {
                        CategoryUtil.DisplayAllWithProducts();
                    }
                    else if (choice == "5")
                    {
                        CategoryUtil.EditName();
                    }
                    else if (choice == "6")
                    {
                        CategoryUtil.Delete();
                    }
                    else if (choice == "7")
                    {
                        ProductUtil.DisplayMenu();
                    }
                    else if (choice == "8")
                    {
                        ProductUtil.Add();
                    }
                    else if (choice == "9")
                    {
                        ProductUtil.Edit();
                    }
                    Console.WriteLine();

                } while (choice.ToLower() != "q");
            }

            catch (DbEntityValidationException e)
            {
                logger.Error(e.Message);
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Error("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", 
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }

            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }
    }
}