using System;
using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class Order
    {
        [Key]
        public int OrderId{get;set;}
        public int Quantity {get;set;}
        public DateTime Date{get;set;}

        //FK
        public Customer Customer {get;set;}
        public Inventory Inventory {get;set;}

        
    }
}