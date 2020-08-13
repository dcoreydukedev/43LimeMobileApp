/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Models.DomainModels;
using _43LimeMobileApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace _43LimeMobileApp.Admin.Services
{
    internal sealed class CommandLogDataService : IDataService
    {
        private const string GetUrl = @"http://104.225.140.177/api/CommandLog/Get";
        private ICollection<CommandLog> _l;
        private readonly Timestamp _timeStamp;

        public CommandLogDataService()
        {
            _timeStamp = new Timestamp();
        }

        public async Task Init()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GetUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(GetUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    _l = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<CommandLog>>(data);
                }
                else
                {
                    _l = null;
                    throw new Exception("An Error Occurred While Getting the Command Log Data");
                }

            };
        }

       /// <summary>
        /// Get All Command Logs
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ICollection<CommandLog> Get()
        {
            var query = _l.ToList();
            return query;
        }

        /// <summary>
        /// Get All Command Logs For The Specified User
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ICollection<CommandLog> Get(string userId)
        {
            var query = _l.Where(x => x.UserId == userId).ToList();
            return query;
        }

        /// <summary>
        /// Get All Command Logs For The Specified User That Occurred Between The Two Times
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="fromTime">From time.</param>
        /// <param name="toTime">To time.</param>
        public ICollection<CommandLog> Get(string userId, DateTime fromTime, DateTime toTime)
        {

            var query = _l.Where(x => x.UserId == userId)
                .Where(x => _timeStamp.GetDateTimeFromEpoch(x.Timestamp) >= fromTime)
                .Where(x => _timeStamp.GetDateTimeFromEpoch(x.Timestamp) <= toTime)
                .ToList();
            return query;
        }

        /// <summary>
        /// Gets All of The Specified Command Logs for the Specified User
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        public ICollection<CommandLog> Get(string userId, int commandId)
        {
            var query = _l.Where(x => x.UserId == userId)
                                         .Where(x => x.CommandId == commandId).ToList();
            return query;
        }

        /// <summary>
        ///  Get All of the Specified Command Logs For The Specified User That Occurred Between The Two Times
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="fromTime">From time.</param>
        /// <param name="toTime">To time.</param>
        public ICollection<CommandLog> Get(string userId, int commandId, DateTime fromTime, DateTime toTime)
        {
            var query = _l.Where(x => x.UserId == userId)
                .Where(x => x.CommandId == commandId)
                .Where(x => _timeStamp.GetDateTimeFromEpoch(x.Timestamp) >= fromTime)
                .Where(x => _timeStamp.GetDateTimeFromEpoch(x.Timestamp) <= toTime)
                .ToList();
            return query;
        }

    }
}
