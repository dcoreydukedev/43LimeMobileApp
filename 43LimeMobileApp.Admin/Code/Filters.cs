/*************************************************************************
 * Author: DCoreyDuke
 * Custom Filters
 ************************************************************************/

using System.Web.Mvc;

namespace _43LimeMobileApp.Admin
{

    /// <summary>
    /// Add Custom Headers To Every Request
    /// Sender Header andf Token Header Together provide strong response validation
    /// </summary>
    public class AddAdminHeaderAttribute : FilterAttribute, System.Web.Mvc.IResultFilter
    {
        internal string Token { get; } = @"juy934-_43LIME-0ab345";

        public AddAdminHeaderAttribute()
        {
        }

        public void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext)
        {

        }


        public void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("43LimeMobileApp-Sender", "Hwy 43 Lime Admin App");
            filterContext.HttpContext.Response.Headers.Add("43LimeMobileApp-Token", this.Token);
        }
    }


}
