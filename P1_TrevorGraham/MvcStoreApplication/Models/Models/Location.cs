using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Location
    {
        [Key]
        public Guid LocationId{get;set;}

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name {get;set;}

        // [ForeignKey("LocationId")]
        //public List<Inventory> InventoryItems {get;set;} //= new List<Inventory>();
    }
}