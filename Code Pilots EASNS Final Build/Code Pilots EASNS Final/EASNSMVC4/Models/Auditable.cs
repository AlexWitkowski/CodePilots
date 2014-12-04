using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Models
{
    internal interface IAuditable
    {
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }
        DateTime UpdatedOn { get; set; }
        string UpdatedBy { get; set; }
    }
    public abstract class Auditable : IAuditable
    {
        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }

        [ScaffoldColumn(false)]
        [StringLength(256)]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedOn { get; set; }

        [ScaffoldColumn(false)]
        [StringLength(256)]
        public string UpdatedBy { get; set; }

        [Timestamp]
        public Byte[] RowVersion { get; set; }

    }
}