using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "Store")]
        public string StoreName { get; set; }
        [Display(Name = "Total Value")]
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }

        public List<OrderLineViewModel> orderLines { get; set; }


        [Display(Name = "ID")]
        public Guid OrderId { get; set; }
        //FK
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
        public Guid CustomerId { get; set; }
    }
}
