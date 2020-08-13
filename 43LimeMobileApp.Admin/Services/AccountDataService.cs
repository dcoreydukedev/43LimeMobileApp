/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/
using _43LimeMobileApp.Models.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _43LimeMobileApp.Admin.Services
{
    public class AccountDataService : IDataService
    {
        private User _user;
        private readonly string _loginUrl;
        private readonly string _logoutUrl;

        public AccountDataService(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return;
            _loginUrl = $"http://104.225.140.177/api/Account/Login?userId={userId}";
            _logoutUrl = $"http://104.225.140.177/api/Account/Logout?userId={userId}";
        }

        public AccountDataService(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserId)) return;
            this._user = user;
            _loginUrl = $"http://104.225.140.177/api/Account/Login?userId={this._user.UserId}";
            _logoutUrl = $"http://104.225.140.177/api/Account/Logout?userId={this._user.UserId}";
        }

        /// <summary>
        /// Login the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public async Task<User> Login(string userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this._loginUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(this._loginUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    this._user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(data);
                }
                else
                {
                    this._user = null;
                }

            };
            return this._user;
        }

        /// <summary>
        /// Logs out the specified user
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="Exception">An Error Occurred While Attempting To Logout the User</exception>
        public async Task Logout(string userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this._logoutUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(this._logoutUrl);
                if (response.IsSuccessStatusCode)
                {
                    this._user = null;
                }
                else
                {
                    throw new Exception("An Error Occurred While Attempting To Logout the User");
                }

            };
            return;
        }
    }
}
