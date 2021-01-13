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

        [Display(Name = "Store Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LocationName { get; set; }

        //this stuff is from the product
        [Display(Name = "Product Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }


        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }


        [Display(Name = "Description")]
        public string Description { get; set; }


        [Display(Name = "ID")]
        public Guid InventoryId { get; set; }

        //FK
        [Display(Name = "Store")]
        public Guid LocationId { get; set; }
        [Display(Name = "Customer")]
        public Guid ProductId { get; set; }

    }
}
