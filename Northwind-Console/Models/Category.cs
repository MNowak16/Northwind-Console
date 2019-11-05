using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindConsole.Models
{
    public class Category
    {
        //[Key]  //optional; VS will assume it's a key because the name includes "ID"
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required!")]  //attribute applied to property below
        public string CategoryName { get; set; }

        //Example: [StringLength(500)] //would restrict description to 500 characters
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
