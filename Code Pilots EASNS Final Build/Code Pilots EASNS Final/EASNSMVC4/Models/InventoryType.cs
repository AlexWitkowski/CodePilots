using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EASNSMVC4.DAL;
using EASNSMVC4.Models;

namespace EASNSMVC4.Models
{
    public class InventoryType
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Inventory Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Peripheral")]
        public int PeripheralID { get; set; }

        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Description")]
        public string Desc { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity
        {
            get
            {
                DBEntity db = new DBEntity();
                var list = db.Inventories.Where(r => r.InventoryTypeID.Equals(this.ID));
                if (list == null)
                {
                    return 0;
                }
                else
                {
                    return list.Count();
                }
            }
        }
        public virtual ICollection<Disability> Disabilities { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Vendor")]
        public int VendorID { get; set; }


        [Display(Name = "Display Picture")]
        public string Display { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        public virtual ICollection<InventoryResource> InventoryResources { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual Peripheral Peripheral { get; set; }
    }
}