using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class PlayerViewModel
    {
        //public PlayerViewModel(string fname = "null", string lname = "null")
        //{
        //    this.Fname = fname;
        //    this.Lname = lname;
        //}

        //[Key]
        public Guid playerId { get; set; } = Guid.NewGuid();
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

        [Display(Name = "Number of Wins")]
        [Range(0, int.MaxValue)]
        public int numWins { get; set; }

        [Display(Name = "Number of Losses")]
        [Range(0, int.MaxValue)]
        public int numLosses { get; set; }

        //[Display(Name = "Player Image")]
        public string JpegStringImage { get; set; }

        public IFormFile IFormFileImage { get; set; }
    }
}
