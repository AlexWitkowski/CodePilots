using EASNSMVC4.Filters;
using System.Web;
using System.Web.Mvc;

namespace EASNSMVC4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new InitializeSimpleMembershipAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}