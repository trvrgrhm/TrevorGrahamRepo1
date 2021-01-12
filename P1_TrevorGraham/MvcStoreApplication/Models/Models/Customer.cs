
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Customer: IUser
    {
        [Key]
        public Guid UserId{get;set;}

        [StringLength(20, MinimumLength = 5, ErrorMessage = "The username must be between 5 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Use letters and numbers only please")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [StringLength(40, MinimumLength = 5, ErrorMessage = "The password must be between 5 and 40 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Use letters and numbers only please")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(20, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First Name")]
        public string Fname{get;set;}

        [StringLength(20, MinimumLength = 1, ErrorMessage = "The name must be between 1 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last Name")]
        public string LName{get;set;}

        public Location DefaultLocation { get; set; }

        //public List<Order> Orders {get;set;}
    }
}