using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class OrderLine
    {
        [Key]
        public Guid OrderLineId {get;set;}


        [Range(0,int.MaxValue)]
        public int Quantity {get;set;}

        //FK
        public Order Order{get;set;}
        public Inventory Inventory {get;set;}
    }
}