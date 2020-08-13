/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using System.Web.Mvc;

namespace _43LimeMobileApp.Admin.API.Controllers
{
    /// <summary>
    /// ...
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// ...
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "43 Lime Products";

            return View();
        }
    }
}
