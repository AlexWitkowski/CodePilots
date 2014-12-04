using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Models
{
    public class ActiveState
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Is Active")]
        [Required]
        public string Desc { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}