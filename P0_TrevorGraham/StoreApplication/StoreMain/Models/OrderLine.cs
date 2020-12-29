using System.ComponentModel.DataAnnotations;
using StoreApplication.Models;

namespace StoreApplication.Models
{
    public class OrderLine
    {
        [Key]
        public int OrderLineId {get;set;}
        public int Quantity {get;set;}

        //FK
        public Order Order{get;set;}
        public Inventory Inventory {get;set;}
    }
}