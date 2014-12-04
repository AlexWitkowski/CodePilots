using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Models
{
    public class TechLevel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Tech Level")]
        public string Desc { get; set; }

        public virtual ICollection<Peripheral> Peripherals { get; set; }
    }
}