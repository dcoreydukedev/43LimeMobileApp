/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using System.Web.Mvc;

namespace _43LimeMobileApp.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AddAdminHeaderAttribute());
        }
    }
}
