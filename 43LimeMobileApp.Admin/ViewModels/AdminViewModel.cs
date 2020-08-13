/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Models.ViewModels;
using System.Collections.Generic;

namespace _43LimeMobileApp.Admin.ViewModels
{
    public interface IAdminViewModel : IViewModel
    {       
        AppAuthToken Token{get;set;}        
        Dictionary<string, object> Data { get; set; }
        AdminViewModel Init();
    }


    /// <summary>
    /// Main VM For SPA
    /// </summary>
    public class AdminViewModel : IAdminViewModel
    {
        public AppAuthToken Token{get;set;}        
        public Dictionary<string, object> Data { get; set; }

        public AdminViewModel()
        {

        }

        public AdminViewModel Init()
        {
            this.Token = new AppAuthToken().CreateEmptyToken();           
            this.Data = new Dictionary<string, object>();

            return this;
        }

    }

}
