/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using _43LimeMobileApp.Data;
using _43LimeMobileApp.Models.Entities;
using _43LimeMobileApp.Repository;
using _43LimeMobileApp.API.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace _43LimeMobileApp.API.Services
{
    /// <summary>
    /// Service to Provide ButtonCommand Data to A Controller
    /// </summary>
    internal sealed class ButtonCommandService : IAPIControllerService<CommandsController>
    {

        private ApplicationDbContext _context;
        private ApplicationRepository<ButtonCommand> _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonCommandService"/> class.
        /// </summary>
        public ButtonCommandService()
        {
            this._context = new ApplicationDbContext();
            this._repo = new ApplicationRepository<ButtonCommand>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonCommandService"/> class.
        /// </summary>
        /// <param name="repo">The ButtonCommand Repository.</param>
        public ButtonCommandService(ApplicationRepository<ButtonCommand> repo)
        {
            this._context = new ApplicationDbContext();
            this._repo = repo;
        }


        /// <summary>
        /// Gets the commands.
        /// </summary>
        public List<ButtonCommand> GetCommands()
        {
            return (List<ButtonCommand>)_repo.GetAll();
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        public ButtonCommand GetCommand(int commandId)
        {
            return _context.Commands.Where(x => x.CommandId == commandId).SingleOrDefault();
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="command">The command.</param>
        public ButtonCommand GetCommand(string command)
        {
            return _context.Commands.Where(x => x.Command == command).SingleOrDefault();
        }

        //TODO: Add Create, CreateMany, Delete, and Update Methods
    }
}