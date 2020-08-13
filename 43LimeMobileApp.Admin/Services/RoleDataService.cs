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
    internal sealed class RoleDataService : IDataService
    {
        private const string GetUrl =    @"http://104.225.140.177/api/Roles/Get";
        private const string CreateUrl = @"http://104.225.140.177/api/Roles/Create";
        private const string DeleteUrl = @"http://104.225.140.177/api/Roles/Delete";

        private ICollection<Role> _Roles;

        public RoleDataService()
        {


        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="Exception">An Error Occurred While Getting the Role Data</exception>
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
                    this._Roles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Role>>(data);
                }
                else
                {
                    this._Roles = null;
                    throw new Exception("An Error Occurred While Getting the Roles Data");
                }
            };
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">No Role Data Was Found</exception>
        public ICollection<Role> Get()
        {
            if (_Roles != null)
            {
                return _Roles;
            }
            else
            {
                throw new InvalidOperationException("No Role Data Was Found");
            }
        }

        /// <summary>
        /// Gets the specified role identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">roleId - RoleId Must Be Provided</exception>
        public Role Get(int roleId)
        {
            if (!string.IsNullOrEmpty(roleId.ToString()))
            {
                return _Roles.Single(x => x.Id == roleId);
            }
            else
            {
                throw new ArgumentNullException(nameof(roleId), "RoleId Must Be Provided");
            }
        }

        /// <summary>
        /// Gets the specified role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">roleName - Role Name Must Be Provided</exception>
        public Role Get(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                return _Roles.Single(x => x.RoleName == roleName);
            }
            else
            {
                throw new ArgumentNullException(nameof(roleName), "Role Name Must Be Provided");
            }
        }


        /// <summary>
        /// Creates the specified
        /// </summary>
        /// <param name="roleName">The Role Name.</param>
        public async Task Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var role = new Role(roleName);
                using (var client = new HttpClient())
                {
                    await client.PostAsync(CreateUrl, new StringContent(
                        new JavaScriptSerializer().Serialize(role), Encoding.UTF8, "application/json"));
                }
            }
        }

        /// <summary>
        /// Deletes the specified Role
        /// </summary>
        /// <param name="roleName">The Role name.</param>
        public async Task Delete(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var role = new Role(roleName);
                using (var client = new HttpClient())
                {
                    await client.PostAsync(DeleteUrl, new StringContent(
                        new JavaScriptSerializer().Serialize(role), Encoding.UTF8, "application/json"));
                }
            }
        }
    }
}
