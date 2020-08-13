/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Models.Entities;
using _43LimeMobileApp.Admin.API.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace _43LimeMobileApp.Admin.API.Controllers
{
    /// <summary>
    /// ButtonCommands API Controller
    /// </summary>
    /// <seealso cref="_43LimeMobileApp.Admin.API.Controllers.ApiControllerBase" />
    [RoutePrefix("api/Commands")]
    public class CommandsController : ApiControllerBase
    {

        private ButtonCommandService buttonCommandService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsController"/> class.
        /// </summary>
        public CommandsController()
        {
            this.buttonCommandService = new ButtonCommandService();
        }

        /// <summary>
        /// Gets the button command list.
        /// </summary>
        /// <returns>List of ButtonCommand Objects</returns>
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get()
        {
            try
            {
                List<ButtonCommand> l = buttonCommandService.GetCommands();
                return Ok(l);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Gets the button command;
        /// </summary>
        /// <returns>ButtonCommand Object</returns>
        [HttpGet]
        [Route("Get/{commandId:int}")]
        public IHttpActionResult Get(int commandId)
        {
            if (string.IsNullOrEmpty(commandId.ToString())) { return BadRequest("Proper CommandId Value Must Be Provided!"); }

            try
            {
                ButtonCommand bc = buttonCommandService.GetCommand(commandId);
                return Ok(bc);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}