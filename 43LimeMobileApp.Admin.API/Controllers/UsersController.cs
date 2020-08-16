/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Admin.API.Services;
using _43LimeMobileApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace _43LimeMobileApp.Admin.API.Controllers
{
    /// <summary>
    /// User API Controller
    /// </summary>
    /// <seealso cref="_43LimeMobileApp.Admin.API.Controllers.ApiControllerBase" />
    [RoutePrefix("api/Users")]
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
        /// Get All or a Single User
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        [HttpGet]
        [Route("Get/{userId:length(4)?}")]
        public IHttpActionResult Get(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                try
                {
                    List<User> users = userService.GetUsers();
                    return Ok(users);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            else
            {
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
            
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        [HttpGet]
        [Route("Create")]
        public IHttpActionResult Create([FromUri] string userId, string username, string roleName)
        {
            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
            {
                return BadRequest("Username and UserId Must Be Valid!");
            }

            RoleService roleService = new RoleService();
            Role r = roleService.GetRole(roleName);

            try
            {
                User u = new User(userId, username, r.Id, true, false);
                User newUser = userService.CreateUser(u);
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
        [HttpDelete]
        [Route("Delete/{userId:length(4)}")]
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
