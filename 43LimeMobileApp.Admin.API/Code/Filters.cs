/*************************************************************************
 * Author: DCoreyDuke
 * Custom Filters
 ************************************************************************/

using System.Web.Mvc;

namespace _43LimeMobileApp.Admin.API
{

    /// <summary>
    /// Add Custom Headers To Every Request
    /// Sender Header and Token Header Together provide strong response validation
    /// </summary>
    public class AddAPIHeaderAttribute : FilterAttribute, System.Web.Mvc.IResultFilter
    {
        internal string Token { get; } = @"ibj904-_43LIME-0ab325";

        /// <summary>
        /// ...
        /// </summary>
        public AddAPIHeaderAttribute()
        {
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext)
        {

        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("43LimeMobileApp-Sender", "Hwy 43 Lime API");
            filterContext.HttpContext.Response.Headers.Add("43LimeMobileApp-Token", this.Token);
        }
    }


}
