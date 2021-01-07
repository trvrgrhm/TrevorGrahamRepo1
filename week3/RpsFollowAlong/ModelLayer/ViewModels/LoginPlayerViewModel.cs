using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class LoginPlayerViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First Name")]
        public string Fname { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }
    }
}
