using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId{get;set;}
        public string Username{get;set;}
        public string Password{get;set;}
        public string Fname{get;set;}
        public string LName{get;set;}
    }
}