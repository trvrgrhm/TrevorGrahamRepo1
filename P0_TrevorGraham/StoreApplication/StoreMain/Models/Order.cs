using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApplication.Models
{
    public class Order
    {
        [Key]
        public int OrderId{get;set;}
        public DateTime Date{get;set;}


        [ForeignKey("OrderRefId")]
        public List<OrderLine> OrderLines{get;set;}

        //FK
        public Customer Customer {get;set;}
    }
}