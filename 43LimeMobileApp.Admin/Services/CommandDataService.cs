using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using _43LimeMobileApp.Models.Entities;

namespace _43LimeMobileApp.Admin.Services
{
    internal sealed class CommandDataService : IDataService
    {
        private const string GetUrl = @"http://104.225.140.177/api/Commands/Get";
        private ICollection<ButtonCommand> _commands;

        public CommandDataService()
        {


        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="Exception">An Error Occurred While Getting the Button Command Data</exception>
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
                    this._commands = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ButtonCommand>>(data);
                }
                else
                {
                    this._commands = null;
                    throw new Exception("An Error Occurred While Getting the Button Command Data");
                }
            };
        }

        /// <summary>
        /// Gets All Button Commands
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">No Button Command Data Was Found</exception>
        public ICollection<ButtonCommand> Get()
        {
            if (_commands != null)
            {
                return _commands;
            }
            else
            {
                throw new InvalidOperationException("No Button Command Data Was Found");
            }
        }


        /// <summary>
        /// Gets the specified button command
        /// </summary>
        /// <param name="commandId">The button command identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">commandId - Command Id Must Be Provided</exception>
        public ButtonCommand Get(int commandId)
        {
            if (!string.IsNullOrEmpty(commandId.ToString()))
            {
                return _commands.Single(x => x.CommandId == commandId);
            }
            else
            {
                throw new ArgumentNullException(nameof(commandId), "Command Id Must Be Provided");
            }
        }

    }
}