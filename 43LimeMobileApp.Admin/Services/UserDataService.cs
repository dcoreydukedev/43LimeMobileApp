/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace _43LimeMobileApp.Admin.Services
{
    internal sealed class UserDataService : IDataService
    {
        private const string GetUrl = @"http://104.225.140.177/api/Users/Get";
        private const string CreateUrl = @"http://104.225.140.177/api/Users/Create";
        private ICollection<User> _users;

        public UserDataService()
        {


        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="Exception">An Error Occurred While Getting the User Data</exception>
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
                    this._users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(data);
                }
                else
                {
                    this._users = null;
                    throw new Exception("An Error Occurred While Getting the User Data");
                }
            };
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">No User Data Was Found</exception>
        public ICollection<User> Get()
        {
            if (_users != null)
            {
                return _users;
            }
            else
            {
                throw new InvalidOperationException("No User Data Was Found");
            }
        }


        /// <summary>
        /// Gets the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">userId - User Id Must Be Provided</exception>
        public User Get(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return _users.Single(x => x.UserId == userId);
            }
            else
            {
                throw new ArgumentNullException(nameof(userId), "User Id Must Be Provided");
            }
        }


        /// <summary>
        /// Creates the specified user; Default Role = Operator
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        public async Task Create(string userId, string username)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(username))
            {
                var user = new User(userId, username, 3, true, false);
                using (var client = new HttpClient())
                {

                    await client.PostAsync(CreateUrl, new StringContent(
                        new JavaScriptSerializer().Serialize(user), Encoding.UTF8, "application/json"));
                }
            }
        }

        /// <summary>
        /// Creates the specified user
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="roleId">The role identifier.</param>
        public async Task Create(string userId, string username, int roleId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(username))
            {
                var user = new User(userId, username, roleId, true, false);
                using (var client = new HttpClient())
                {

                    await client.PostAsync(CreateUrl, new StringContent(
                        new JavaScriptSerializer().Serialize(user), Encoding.UTF8, "application/json"));
                }
            }
        }
    }
}
