using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Product
    {
        [Key]
        public Guid ProductId {get;set;}


        [Display(Name = "Product Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string ProductName {get;set;}


        [Display(Name = "Price")]
        [Range(0,double.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price {get;set;}


        [Display(Name = "Description")]
        public string Description {get;set;}
    }
}