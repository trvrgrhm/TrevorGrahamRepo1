using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class Product
    {
        [Key]
        public int ProductId{get;set;}
        public string ProductName {get;set;}
        public double Price {get;set;}
        public string Description {get;set;}
    }
}