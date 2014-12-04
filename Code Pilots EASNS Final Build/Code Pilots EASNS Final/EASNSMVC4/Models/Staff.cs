using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Models
{
    public class Staff
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string First { get; set; }

        [Display(Name = "Middle Name")]
        public string Middle { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string Last { get; set; }
        
        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; }
        
        [Required]
        [Display(Name = "Campus")]
        public string Campus { get; set; }
        
        [Display(Name = "Avatar")]
        public string Avatar { get; set; }
        
        [Required]
        [Display(Name = "Description")]
        public string Desc { get; set; }
    }
}