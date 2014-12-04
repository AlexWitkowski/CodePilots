using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Models
{
    public class Vendor
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Province/State")]
        public string Province { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        public virtual ICollection<InventoryType> Inventories { get; set; }
        
    }
}