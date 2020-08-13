/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/
using _43LimeMobileApp.Admin.Services;
using _43LimeMobileApp.Admin.ViewModels;
using _43LimeMobileApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _43LimeMobileApp.Admin.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Required)]
    [RoutePrefix("Admin")]
    public class AdminController : WebControllerBase
    {
        private AccountDataService _accountDataService;
        private CommandLogDataService _commandLogDataService;
        private UserDataService _userDataService;
        private CommandDataService _commandDataService;
        private RoleDataService _roleDataService;

        public AdminController()
        {
            // Initialize Data Services
            _commandLogDataService = new CommandLogDataService();
            _userDataService = new UserDataService();
            _commandDataService = new CommandDataService();
            _roleDataService = new RoleDataService();
        }        

        [HttpGet, AddAdminHeader]
        [Route("Index")]
        public async Task<ActionResult> Index(AdminViewModel? vm)
        {
            if(vm == null || vm.Token == null)
            {
                return await Task.Run(() => RedirectToAction("Login"));
            }
            if (vm.Token != null && vm.Token.TokenType == AppAuthTokenType.Empty)
            {
                return await Task.Run(() => RedirectToAction("Login"));
            }           

            SetRequestData(this.Request, this.HttpContext.ApplicationInstance.Context, vm);

            return await Task.Run(() => View("~/Views/Admin/Index.cshtml", vm));
        }

        [HttpGet, AddAdminHeader]
        [Route("Login")]
        public async Task<ActionResult> Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            lvm.IsError = false;
            lvm.UserId = string.Empty;
            lvm.ErrorMessage = string.Empty;
            return await Task.Run(() => View("~/Views/Admin/Login.cshtml", lvm));
        }

        [HttpPost, AddAdminHeader]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel vm)
        {
            try
            {
                _accountDataService = new AccountDataService(vm.UserId);

                User user = await _accountDataService.Login(vm.UserId);
                if (user == null)
                {
                    LoginViewModel lvm = new LoginViewModel();
                    lvm.IsError = true;
                    lvm.UserId = string.Empty;
                    lvm.ErrorMessage = "Invalid Pin, Please Try Again!";
                    return await Task.Run(() => View("~/Views/Admin/Login.cshtml", lvm));
                }
                else
                {
                    // Initialize VM with default values
                    // Creates Empty AppAuthToken
                    AdminViewModel _vm = new AdminViewModel().Init();
                    AppAuthToken _emptyToken = _vm.Token;
                    AppAuthToken _authenticatedToken = new AppAuthToken().CreateAuthenticatedUserToken(_emptyToken, vm.UserId, DateTime.Now);
                    _vm.Token = _authenticatedToken;
                    return await Task.Run(() => RedirectToAction("Index", _vm));
                }
            }
            catch (Exception ex)
            {
                LogError("Admin", "Login", ex);
                return ShowErrorView("Admin", "Login", ex);
            }
        }

        [HttpPost, AddAdminHeader]
        [Route("Logout")]
        public async Task<ActionResult> Logout(AdminViewModel vm)
        {
            try
            {
                _accountDataService = new AccountDataService(vm.Token.User.UserId);

                await _accountDataService.Logout(vm.Token.User.UserId);

                // CAncel AppAuthToken
                AppAuthToken _oldToken = vm.Token;
                AppAuthToken _cancelledToken = new AppAuthToken().CancelAuthenticatedUserToken(_oldToken);
                vm.Token = _cancelledToken;

                //TODO: Add Data from AdminViewModel.Data to SessionLog Table in SQLite DB

                // Clear Session
                Session.Clear();

                return await Task.Run(() => RedirectToAction("Index", vm));

            }
            catch (Exception ex)
            {
                LogError("Admin", "Logout", ex);
                return ShowErrorView("Admin", "Logout", ex);
            }
        }


        #region JSON Methods

        /*
         * JSON Methods for AJAX Data Requests From App
         */

        /// <summary>
        /// Return Users As Json Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUsers()
        {
            List<User> _l = _userDataService.Get().ToList();
            return Json(_l, JsonRequestBehavior.AllowGet);
        }

        //ToDo: Continue Here

        #endregion

        #region Helpers

        /// <summary>
        /// Add Some Request Data To The View Model Data Dictionary
        /// </summary>
        private void SetRequestData(HttpRequestBase request, HttpContext context, AdminViewModel _vm)
        {
            _vm.Data.Add("Browser-UserAgent", request.UserAgent.ToString());
            _vm.Data.Add("Request-AcceptTypes", string.Join(",", request.AcceptTypes));
            _vm.Data.Add("Request-ContentType", request.ContentType.ToString());
            _vm.Data.Add("Request-IsSecureConnection", request.IsSecureConnection.ToString());
            _vm.Data.Add("Request-IsAjaxRequest", request.IsSecureConnection.ToString());
            _vm.Data.Add("Request-UserHostAddress", request.UserHostAddress.ToString());
            _vm.Data.Add("Request-IsMobileDevice", request.Browser.IsMobileDevice.ToString());
            if (request.Browser.IsMobileDevice)
            {
                _vm.Data.Add("Request-MobileDeviceMfg", request.Browser.MobileDeviceManufacturer.ToString());
                _vm.Data.Add("Request-MobileDeviceModel", request.Browser.MobileDeviceModel.ToString());
            }
            var bc = context.Request.Browser;
            _vm.Data.Add("Browser-JavaScript", (bc.EcmaScriptVersion.Major > 1));
            _vm.Data.Add("Browser-JavaScriptVersion", bc.EcmaScriptVersion.ToString());
            _vm.Data.Add("Browser-Name", (bc.Browser.ToString()));
            _vm.Data.Add("Session-Start-Time", Session["StartTime"]);

            return;
        }

        #endregion

    }
}
