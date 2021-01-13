using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class OrderLineViewModel
    {
        //[Display(Name="Customer")]
        //public string CustomerName { get; set; }
        [Display(Name = "Store")]
        public string StoreName { get; set; }
        [Display(Name = "Product")]
        public string ProductName { get; set; }
        [Display(Name = "Individual Price")]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }



        [Display(Name = "ID")]
        public Guid OrderLineId { get; set; }
        //FK
        [Display(Name = "Order")]
        public Guid OrderId { get; set; }
        
        public Guid InventoryId { get; set; }
    }
}
