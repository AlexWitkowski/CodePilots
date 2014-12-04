using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;


namespace EASNSMVC4.Models
{
    public class About
    {
        public string Mission { get; set; }
        public string Staff { get; set; }
    }

}