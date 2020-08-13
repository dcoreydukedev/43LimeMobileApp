/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Data;
using _43LimeMobileApp.Models.Entities;
using _43LimeMobileApp.Repository;
using _43LimeMobileApp.API.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace _43LimeMobileApp.API.Services
{
    /// <summary>
    /// Data Service For Command Log
    /// </summary>
    internal sealed class CommandLogService : IAPIControllerService<CommandLogController>
    {

        private ApplicationDbContext _context;
        private ApplicationRepository<CommandLog> _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLogService"/> class.
        /// </summary>
        public CommandLogService()
        {
            this._context = new ApplicationDbContext();
            this._repo = new ApplicationRepository<CommandLog>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLogService"/> class.
        /// </summary>
        /// <param name="repo">The CommandLog Repository</param>
        public CommandLogService(ApplicationRepository<CommandLog> repo)
        {
            this._context = new ApplicationDbContext();
            this._repo = repo;
        }


        /// <summary>
        /// Gets the CommandLogs.
        /// </summary>
        /// <returns>List of CommandLogs</returns>
        public List<CommandLog> GetCommandLogs()
        {
            return (List<CommandLog>)_repo.GetAll();
        }

        /// <summary>
        /// Gets the CommandLog.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The Specified CommandLog</returns>
        public CommandLog GetCommandLog(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Gets the command log records.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<CommandLog> GetCommandLogs(string userId)
        {
            return _context.CommandLogs.Where(u => u.UserId == userId).ToList();

        }

        /// <summary>
        /// Gets the command log.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <returns></returns>
        public List<CommandLog> GetCommandLogs(string userId, int commandId)
        {
            return _context.CommandLogs.Where(u => u.UserId == userId && u.CommandId == commandId).ToList();

        }

        /// <summary>
        /// Gets the command log.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns></returns>
        public List<CommandLog> GetCommandLogs(string userId, int commandId, long timestamp)
        {
            return _context.CommandLogs.Where(u => u.UserId == userId && u.CommandId == commandId && u.Timestamp == timestamp).ToList();

        }
        /// <summary>
        /// Add commands via stored procedure
        /// </summary>
        /// <param name="jsonStr"></param>
        public void AddCommandLogSyncMobileAppTOServer(string jsonStr)
        {
            var jsonStrParam = new SqlParameter("@json", jsonStr);
            _context.Database.ExecuteSqlCommand("exec SPStoreSyncedCommands @json", jsonStrParam);

        }

        /// <summary>
        /// Adds the command log.
        /// </summary>
        /// <param name="log">The log.</param>
        public void AddCommandLog(CommandLog log)
        {
            _repo.Add(log);
            //Add new log
            _context.CommandLogs.Add(log);
            _context.SaveChanges();  
        }

        /// <summary>
        /// Updates the CommandLog.
        /// </summary>
        /// <param name="u">The CommandLog</param>
        public void UpdateCommandLog(CommandLog u)
        {
            _repo.Update(u);
        }

        /// <summary>
        /// Deletes the CommandLog.
        /// </summary>
        /// <param name="id">The id of the CommandLog</param>
        public void DeleteCommandLog(int id)
        {
            _repo.Remove(id);
        }

        /// <summary>
        /// Deletes the command logs.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void DeleteCommandLogs(string userId)
        {
            List<CommandLog> CommandLogList = _context.CommandLogs.Where(u => u.UserId == userId).ToList(); 
            
            if (CommandLogList.Count > 0)
            {
                foreach (CommandLog commandLog in CommandLogList)
                {
                    DeleteCommandLog(commandLog.Id);
                }
            }
        }

        /// <summary>
        /// Deletes the command logs.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        public void DeleteCommandLogs(string userId, int commandId)
        {
            List<CommandLog> CommandLogList = _context.CommandLogs.Where(u => u.UserId == userId && u.CommandId == commandId).ToList();

            if (CommandLogList.Count > 0)
            {
                foreach (CommandLog commandLog in CommandLogList)
                {
                    DeleteCommandLog(commandLog.Id);
                }
            }
        }

        /// <summary>
        /// Deletes the command logs.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="timestamp">The timestamp.</param>
        public void DeleteCommandLogs(string userId, int commandId, long timestamp)
        {
            List<CommandLog> CommandLogList = _context.CommandLogs.Where(u => u.UserId == userId && u.CommandId == commandId && u.Timestamp == timestamp).ToList();

            if (CommandLogList.Count > 0)
            {
                foreach (CommandLog commandLog in CommandLogList)
                {
                    DeleteCommandLog(commandLog.Id);
                }
            }
        }

    }
}