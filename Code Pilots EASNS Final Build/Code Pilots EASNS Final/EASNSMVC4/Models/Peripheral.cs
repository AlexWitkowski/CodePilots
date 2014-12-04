using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EASNSMVC4.DAL;

namespace EASNSMVC4.Models
{
    public class Peripheral
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Description")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Tech Level")]
        public int TechLevelID { get; set; }

        [Display(Name = "Display Picture")]
        public string Display { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity
        {
            get
            {
                DBEntity db = new DBEntity();
                var list = db.InventoryTypes.Where(r => r.PeripheralID.Equals(this.ID));
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

        public virtual ICollection<InventoryType> InventoryTypes { get; set; }

        public virtual TechLevel TechLevel { get; set; }
    }
}