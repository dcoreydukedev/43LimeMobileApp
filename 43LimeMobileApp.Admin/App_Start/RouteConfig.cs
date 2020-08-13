/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using System.Web.Mvc;
using System.Web.Routing;

namespace _43LimeMobileApp.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Enable Attribute Routing
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index" }
            );
        }
    }
}
