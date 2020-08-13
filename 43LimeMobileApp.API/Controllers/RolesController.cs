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
    /// Roles Api Controller
    /// </summary>
    /// <seealso cref="_43LimeMobileApp.API.Controllers.ApiControllerBase" />
    //[RoutePrefix("/api/roles")]
    public class RolesController : ApiControllerBase
    {

        private readonly RoleService roleService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        public RolesController()
        {
            this.roleService = new RoleService();
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        //[HttpGet]
        //[Route("get")]
        //[AcceptVerbs("GET")]
        public IHttpActionResult Get()
        {
            try
            {
                List<Role> l = roleService.GetAllRoles();
                return Ok(l);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Gets the specified role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        //[HttpGet]
        //[Route("get/{roleName}")]
        //[AcceptVerbs("GET")]
        public IHttpActionResult Get(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("A Valid RoleName Must Be Provided!");
            }

            try
            {
                Role r = roleService.GetRole(roleName);
                return Ok(r);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Creates the specified role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        //[HttpPost]
        //[Route("create")]
        //[AcceptVerbs("POST")]
        public IHttpActionResult Create([FromBody] string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("A Valid Role Name Must Be Provided!");
            }
            try
            {
                Role role = roleService.CreateRole(roleName);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes the specified role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        //[HttpDelete]
        //[Route("delete/{roleName}")]
        //[AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("A Valid RoleName Must Be Provided!");
            }

            try
            {
                roleService.DeleteRole(roleName);
                return Ok("Role Deleted Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}