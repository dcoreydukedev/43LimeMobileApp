/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace _43LimeMobileApp.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Add("StartTime", DateTime.Now);               
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session.Clear();
        }
    }
}
