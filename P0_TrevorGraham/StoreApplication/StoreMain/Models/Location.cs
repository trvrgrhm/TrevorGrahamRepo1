using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApplication.Models
{
    public class Location
    {
        [Key]
        public int LocationId{get;set;}
        public string Name {get;set;}

        [ForeignKey("LocationId")]
        public List<Inventory> Inventorys {get;set;} //= new List<Inventory>();
    }
}