using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Models
{
    public class FileType
    {
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "You have to select a file type")]
        [Display(Name = "File Type")]
        public string Desc { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<InventoryResource> InventoryResources { get; set; }
    }
}