/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.API.Services;
using _43LimeMobileApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace _43LimeMobileApp.API.Controllers
{
    /// <summary>
    /// User API Controller
    /// </summary>
    /// <seealso cref="_43LimeMobileApp.API.Controllers.ApiControllerBase" />
    //[RoutePrefix("/api/users")]
    public class UsersController : ApiControllerBase
    {

        private readonly UserService userService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        public UsersController()
        {
            this.userService = new UserService();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        //[HttpGet]
        //[Route("Get")]
        //[AcceptVerbs("GET")]
        public IHttpActionResult Get()
        {
            try
            {
                List<User> l = userService.GetUsers();
                return Ok(l);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        //[HttpGet]
        //[Route("get/{userId:length(4)}")]
        //[AcceptVerbs("GET")]
        public IHttpActionResult Get(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("A Valid UserId Must Be Provided!");
            }

            try
            {
                User u = userService.GetUser(userId);
                return Ok(u);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user</param>
        //[HttpPost]
        //[Route("create")]
        //[AcceptVerbs("POST", "PUT")]
        public IHttpActionResult Create([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserId))
            {
                return BadRequest("A Valid User Object Must Be Provided!");
            }
            try
            {
                User newUser = userService.CreateUser(user);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        //[HttpDelete]
        //[Route("delete/{userId:length(4)}")]
        //[AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("A Valid UserId Must Be Provided!");
            }

            try
            {
                userService.DeleteUser(userId);
                return Ok("User Deleted Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
