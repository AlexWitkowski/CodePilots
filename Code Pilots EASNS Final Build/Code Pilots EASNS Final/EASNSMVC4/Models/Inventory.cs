using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EASNSMVC4.DAL;

namespace EASNSMVC4.Models
{
    public class Inventory : Auditable
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; }     

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Inventory Type")]
        public int InventoryTypeID { get; set; }

        public virtual InventoryType InventoryType { get; set; }
    }
}