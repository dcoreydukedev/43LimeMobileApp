/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Admin.Controllers;

namespace _43LimeMobileApp.Admin.Services
{
    public interface IService
    {

    }

    /// <summary>
    /// Inherited by classes that provide or manipulate some piece of application data
    /// </summary>
    public interface IDataService : IService
    {

    }


    /// <summary>
    /// Implemented by classes that provide data to a controller
    /// </summary>
    public interface IControllerService<TController> : IService where TController : class
    {

    }

    /// <summary>
    /// Implemented by classes that provide data to a the admin controller
    /// </summary>
    public interface IAdminAppService : IControllerService<AdminController>
    {

    }



}