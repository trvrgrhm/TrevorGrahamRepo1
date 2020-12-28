using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId{get;set;}
        public int Quantity {get;set;}
        
        //FK
        public Location Location {get; set;}
        public Product Product {get; set;}
        
    }
}