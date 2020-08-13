/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Models.Entities;
using _43LimeMobileApp.API.Services;
using System;
using System.Web.Http;
using System.Collections.Generic;
using _43LimeMobileApp.Models.ViewModels;
using System.Web.Script.Serialization;
namespace _43LimeMobileApp.API.Controllers
{
    /// <summary>
    /// CommandLogs API Controller
    /// </summary>
    /// <seealso cref="_43LimeMobileApp.API.Controllers.ApiControllerBase" />
    //[RoutePrefix("api/logs")]
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


        #region"New APIs created by Abhi for fixing Mobile Application issues"
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


        #endregion






        /// <summary>
        /// Creates the specified command log record.
        /// </summary>
        /// <param name="log">The command log record.</param>
        /// <returns></returns>

        #region"Commented Code by Abhi for DCoreyDuke."
        //[HttpPost]
        //[Route("create")]
        //[AcceptVerbs("POST", "PUT")]
        //public IHttpActionResult Create([FromBody]CommandLogViewModel log)
        //{

        //    if (string.IsNullOrEmpty(log.UserId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(log.CommandId.ToString())) { return BadRequest("Proper CommandId Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(log.Timestamp.ToString())) { return BadRequest("Proper Timestamp Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(log.Latitude)) { return BadRequest("Proper Latitude Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(log.Longitude)) { return BadRequest("Proper Longitude Value Must Be Provided!"); }

        //    try
        //    {
        //        CommandLog cl = new CommandLog(log);
        //        commandLogService.AddCommandLog(cl);                
        //        return Ok("Command Logged Successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError("CommandLog", "GET:Create/{userId=}/{commandId=}/{timestamp=}/{latitude=}/{longitude=}", ex);
        //        return InternalServerError(ex);
        //    }

        //}





        /// <summary>
        /// Get All CommandLog Records
        /// </summary>
        //[HttpGet]
        //[Route("get")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult Get()
        //{
        //    try
        //    {
        //        List<CommandLog> l = commandLogService.GetCommandLogs();
        //        return Ok(l);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError("CommandLog", "GET:Get", ex);
        //        return InternalServerError(ex);
        //    }
        //}

        /// <summary>
        /// Get All CommandLog Records for the User
        /// </summary>
        //[HttpGet]
        //[Route("get/{userId:length(4)}")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult Get(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }

        //    try
        //    {
        //        List<CommandLog> l = commandLogService.GetCommandLogs(userId);
        //        return Ok(l);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError("CommandLog", "GET:Get(userId)", ex);
        //        return InternalServerError(ex);
        //    }
        //}

        /// <summary>
        /// Get All CommandLog Records for the User and Command
        /// </summary>
        //[HttpGet]
        //[Route("get/{userId:length(4)}/{commandId}")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult Get(string userId, string commandId)
        //{
        //    if (string.IsNullOrEmpty(userId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(commandId)) { return BadRequest("Proper CommandId Value Must Be Provided!"); }

        //    try
        //    {
        //        List<CommandLog> l = commandLogService.GetCommandLogs(userId, Int32.Parse(commandId));
        //        return Ok(l);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        /// <summary>
        /// Get All CommandLog Records for the User, Command, and Timestamp
        /// </summary>
        //[HttpGet]
        //[Route("get/{userId:length(4)}/{commandId}/{timestamp}")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult Get(string userId, string commandId, string timestamp)
        //{
        //    if (string.IsNullOrEmpty(userId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(commandId)) { return BadRequest("Proper CommandId Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(timestamp)) { return BadRequest("Proper Timestamp Value Must Be Provided!"); }

        //    try
        //    {
        //        List<CommandLog> l = commandLogService.GetCommandLogs(userId, int.Parse(commandId), long.Parse(timestamp));
        //        return Ok(l);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        /// <summary>
        /// Delete All CommandLog Records for the User
        /// </summary>
        //[HttpDelete]
        //[Route("delete/{userId:length(4)}")]
        //[AcceptVerbs("DELETE")]
        //public IHttpActionResult Delete(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }

        //    try
        //    {
        //        commandLogService.DeleteCommandLogs(userId);
        //        return Ok("Records Deleted Successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError("CommandLog", "GET:Get(userId)", ex);
        //        return InternalServerError(ex);
        //    }
        //}

        /// <summary>
        /// Delete All CommandLog Records for the User and Command
        /// </summary>
        //[HttpDelete]
        //[Route("delete/{userId:length(4)}/{commandId}")]
        //[AcceptVerbs("DELETE")]
        //public IHttpActionResult Delete(string userId, string commandId)
        //{
        //    if (string.IsNullOrEmpty(userId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(commandId)) { return BadRequest("Proper CommandId Value Must Be Provided!"); }

        //    try
        //    {
        //        commandLogService.DeleteCommandLogs(userId, Int32.Parse(commandId));
        //        return Ok("Records Deleted Successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        /// <summary>
        /// Delete All CommandLog Records for the User, Command, and Timestamp
        /// </summary>
        //[HttpDelete]
        //[Route("delete/{userId:length(4)}/{commandId}/{timestamp}")]
        //[AcceptVerbs("DELETE")]
        //public IHttpActionResult Delete(string userId, string commandId, string timestamp)
        //{
        //    if (string.IsNullOrEmpty(userId)) { return BadRequest("Proper UserId Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(commandId)) { return BadRequest("Proper CommandId Value Must Be Provided!"); }
        //    if (string.IsNullOrEmpty(timestamp)) { return BadRequest("Proper Timestamp Value Must Be Provided!"); }

        //    try
        //    {
        //        commandLogService.DeleteCommandLogs(userId, int.Parse(commandId), long.Parse(timestamp));
        //        return Ok("Records Deleted Successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
        #endregion

    }
}