using System.Web;
using System.Web.Mvc;

namespace _43LimeMobileApp.Admin.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
