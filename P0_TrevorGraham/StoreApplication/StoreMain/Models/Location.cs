using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class Location
    {
        [Key]
        public int LocationId{get;set;}
        public string Name {get;set;}

        public List<Inventory> InventoryItems {get;set;}
    }
}