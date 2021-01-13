using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Order
    {
        [Key]
        public Guid OrderId{get;set;}

        public bool OrderIsComplete { get; set; }
        public DateTime Date{get;set;}


        // [ForeignKey("OrderRefId")]
        //public List<OrderLine> OrderLines { get; set; }

        //FK
        public Customer Customer {get;set;}
    }
}