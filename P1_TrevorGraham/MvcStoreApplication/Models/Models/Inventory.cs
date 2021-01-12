using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId{get;set;}
        public int Quantity {get;set;}
        
        //FK
        public Location Location {get; set;}
        public Product Product {get; set;}
        
    }
}