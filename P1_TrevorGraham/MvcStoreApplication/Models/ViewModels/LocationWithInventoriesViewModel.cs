using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class LocationWithInventoriesViewModel
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Store Name")]
        public string Name { get; set; }


        [Display(Name = "ID")]
        public Guid LocationId { get; set; }

        [Display(Name = "Inventory")]
        public List<InventoryViewModel> InventoryItems { get; set; }
    }
}
