/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Logging;
using System;
using System.Web.Mvc;

namespace _43LimeMobileApp.Admin
{
    public class FortyThreeLimeErrorHandler : HandleErrorAttribute
    {
        /// <summary>
        /// Private Instance of ErrorLogger
        /// </summary>
        private readonly ErrorLogger _errorLogger;

        public FortyThreeLimeErrorHandler()
        {
            this._errorLogger = new ErrorLogger();
        }

        public override void OnException(ExceptionContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var ex = filterContext.Exception;

            LogError(controllerName, actionName, ex);

            base.OnException(filterContext);
        }

        private void LogError(string controllerName, string actionName, Exception ex)
        {
            var info = new HandleErrorInfo(ex, controllerName, actionName);
            _errorLogger.Log(info);
        }
    }
}
