using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EASNSMVC4.Classes
{
    public static class FileCheck
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
    }
}