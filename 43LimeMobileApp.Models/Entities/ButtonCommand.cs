/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _43LimeMobileApp.Models.Entities
{
    public interface IButtonCommand
    {
        string Command { get; set; }
        int CommandId { get; set; }
        int Id { get; set; }
        int? ParentId { get; set; }

    }

    public class ButtonCommand : IEntity<ButtonCommand>, IButtonCommand
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int CommandId { get; set; }
        [MaxLength(56)]
        /// The Full Name of the Command
        public string Command { get; set; }
        // The Id of the Parent Command
        // Parent Id is basically the Id of the Equipment Selector Command
        public int? ParentId { get; set; }
        public string Category { get; set; }

        public ButtonCommand()
        {

        }

        public ButtonCommand(int commandId, string command) : this()
        {
            this.CommandId = commandId;
            this.Command = command;
        }

        public ButtonCommand(int commandId, string command, int parentId) : this()
        {
            this.CommandId = commandId;
            this.Command = command;
            this.ParentId = parentId;
        }
        public ButtonCommand(int commandId, string command, int parentId, string category) : this()
        {
            this.CommandId = commandId;
            this.Command = command;
            this.ParentId = parentId;
            this.Category = category;
        }

        public ButtonCommand(int id, int commandId, string command, int parentId) : this()
        {
            this.Id = id;
            this.CommandId = commandId;
            this.Command = command;
            this.ParentId = parentId;
        }

        public ButtonCommand(int id, int commandId, string command, int parentId, string category) : this()
        {
            this.Id = id;
            this.CommandId = commandId;
            this.Command = command;
            this.ParentId = parentId;
            this.Category = category;
        }
    }
}
