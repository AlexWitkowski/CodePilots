using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Models
{
    public class Resource : Auditable
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "File Path")]
        [MaxLength(250)]
        public string FilePath { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "File Type")]
        public int FileTypeID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Active State")]
        public int ActiveStateID { get; set; }

        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Display(Name = "Description")]
        public string Desc { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Download #")]
        public int NumDownloads { get; set; }

        public virtual ICollection<Stakeholder> Stakeholders { get; set; }
        public virtual ICollection<Disability> Disabilities { get; set; }
        public virtual FileType FileType { get; set; }
        public virtual ActiveState ActiveState { get; set; }
    }
}