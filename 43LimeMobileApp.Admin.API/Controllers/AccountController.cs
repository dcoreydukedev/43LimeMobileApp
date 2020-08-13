/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Models.Entities;
using _43LimeMobileApp.Admin.API.Services;
using System;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace _43LimeMobileApp.Admin.API.Controllers
{
    /// <summary>
    /// Account API Controller
    /// </summary>
    /// <seealso cref="_43LimeMobileApp.Admin.API.Controllers.ApiControllerBase" />
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

                //Allow only admin type user
                if (u!=null && u.RoleId  == 1)
                {
                    u = userService.LoginUser(userId);
                    //Send Json object
                    return Ok(u);
                }
                else {
                    //Handle exception with Static Code 200.If user not matched.
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


    }
}