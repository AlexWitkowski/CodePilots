using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Models
{
    public class InventoryResource : Auditable
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Inventory Model")]
        public int InventoryTypeID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Upload Document")]
        [MaxLength(250)]
        public string FilePath { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Document Type")]
        public int FileTypeID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Description")]
        public string Desc { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Download #")]
        public int NumDownloads { get; set; }

        public virtual InventoryType InventoryType { get; set; }

        public virtual FileType FileType { get; set; }
    }
}