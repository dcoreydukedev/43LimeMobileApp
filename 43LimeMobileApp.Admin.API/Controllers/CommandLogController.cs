/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Models.Entities;
using _43LimeMobileApp.Admin.API.Services;
using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace _43LimeMobileApp.Admin.API.Controllers
{
    /// <summary>
    /// CommandLogs API Controller
    /// </summary>
    /// <seealso cref="_43LimeMobileApp.Admin.API.Controllers.ApiControllerBase" />
    [RoutePrefix("api/CommandLog")]
    public class CommandLogController : ApiControllerBase
    {

        private CommandLogService commandLogService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLogController"/> class.
        /// </summary>
        public CommandLogController()
        {
            this.commandLogService = new CommandLogService();
        }


        /// <summary>
        /// Creates the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="CommandId">The command identifier.</param>
        /// <param name="Timestamp">The timestamp.</param>
        /// <param name="Lattitude">The lattitude.</param>
        /// <param name="Longitude">The longitude.</param>
        /// <returns></returns>
        [Route("Create")]
        [HttpGet]
        public IHttpActionResult Create([FromUri] string UserId, string CommandId, long Timestamp, string Lattitude, string Longitude)
        {

            // TODO: Verify Data
            CommandLog log = new CommandLog();
            log.CommandId = Convert.ToInt32(CommandId);

            LatLng _LatLng = new LatLng(Lattitude, Longitude);
            log.Location = _LatLng;
            log.Timestamp = Timestamp;
            log.UserId = UserId;
            try
            {
                commandLogService.AddCommandLog(log);
                return Ok(log);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Synchronizes the log.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        [Route("SyncLog")]
        [HttpPost]
        public IHttpActionResult SyncLog([FromBody] List<CommandLogSync> items)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(items);

            try
            {
                commandLogService.AddCommandLogSyncMobileAppTOServer(json);

                return Ok(items);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }


        /// <summary>
        /// Get All CommandLog Records
        /// </summary>
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                List<CommandLog> l = commandLogService.GetCommandLogs();
                return Ok(l);
            }
            catch (Exception ex)
            {
                LogError("CommandLog", "GET:Get", ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                commandLogService.DeleteCommandLog(id);
                return Ok("Record Deleted Successfully");
            }
            catch (Exception ex)
            {
                LogError("CommandLog", "Delete", ex);
                return InternalServerError(ex);
            }
        }

    }
}