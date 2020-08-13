/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Models.Entities;
using _43LimeMobileApp.API.Services;
using System;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using Microsoft.Ajax.Utilities;
using System.Net.Http;
using System.Net;

namespace _43LimeMobileApp.API.Controllers
{
    /// <summary>
    /// Account API Controller
    /// </summary>
    /// <seealso cref="_43LimeMobileApp.API.Controllers.ApiControllerBase" />
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {

        private UserService userService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            this.userService = new UserService();

        }

        #region"New APIs created by Abhi for fixing Mobile Application issues"
        /// <summary>
        /// Login API for Authenticate user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public IHttpActionResult Login([FromUri] string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId)) { return BadRequest("User PIN is required!"); }
                //Get user
                User u = userService.GetUser(userId);

                //Allow only operator type user
                if (u!=null && u.RoleId  ==3)
                {
                    u = userService.LoginUser(userId);
                    //Send Json object
                    return Ok(u);
                }
                else {
                    //Handle exception with Statuc Code 200.If user not matched.
                    return new System.Web.Http.Results.ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)200,
                    new HttpError("Invalid user PIN!")
                )
            );

                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Logout API for Authenticate user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Logout")]
        public IHttpActionResult Logout([FromUri] string userId)
        {
            try
            {
                User u = userService.LogoutUser(userId);
                //Send Json object
                return Ok(u);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        #endregion

        #region"Commented Code by Abhi for DCoreyDuke."

        ///// <summary>
        ///// Login the specified user.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>The User Object</returns>
        //[HttpGet]
        //[Route("Login/{userId:length(4)}")]
        ////[AcceptVerbs("GET")]
        ////[ActionName("Login")]
        //public IHttpActionResult Login(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }

        //    try
        //    {
        //        User u = userService.LoginUser(userId);
        //        return Ok(u);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        ///// <summary>
        ///// Log out the specified user.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>The User Object</returns>
        //[HttpGet]
        //[Route("Logout/{userId:length(4)}")]
        ////[AcceptVerbs("GET")]
        ////[ActionName("Logout")]
        //public IHttpActionResult Logout(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }

        //    try
        //    {
        //        User u = userService.LogoutUser(userId);
        //        return Ok("User Successfully Logged Out!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        #endregion


    }
}