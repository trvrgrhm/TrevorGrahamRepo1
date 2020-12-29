using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class Order
    {
        [Key]
        public int OrderId{get;set;}
        public DateTime Date{get;set;}

        public List<OrderLine> Lines{get;set;}

        //FK
        public Customer Customer {get;set;}
    }
}