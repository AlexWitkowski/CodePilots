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
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Description")]
        public string Desc { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity
        {
            get
            {
                DBEntity db = new DBEntity();
                var list = db.InventoryModels.Where(r => r.InventoryTypeID.Equals(this.ID));
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

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Tech Level")]
        public int TechLevelID { get; set; }

        [Display(Name = "Display Picture")]
        public string Display { get; set; }

        public virtual ICollection<InventoryModel> InventoryModels { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual TechLevel TechLevel { get; set; }
    }
}