using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class InventoryViewModel
    {
        public int InventoryId { get; set; }
        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }


        //this stuff is from the product
        [Display(Name = "Product Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }


        //FK
        public int LocationId { get; set; }
        public int ProductId { get; set; }

    }
}
