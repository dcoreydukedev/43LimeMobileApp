/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/
using _43LimeMobileApp.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _43LimeMobileApp.Models.Entities
{
    public class CommandLog : IEntity<CommandLog>
    {

        public CommandLog()
        {

        }

        public CommandLog(CommandLogViewModel vm)
        {
            this.UserId = vm.UserId;
            this.CommandId = int.Parse(vm.CommandId);
            this.Timestamp = long.Parse(vm.Timestamp);
            this.Location = new LatLng(vm.Latitude, vm.Longitude);
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CommandId { get; set; }
        /// <summary>
        /// Epoch Timestamp
        /// </summary>
        public long Timestamp { get; set; }
        public LatLng Location { get; set; }




    }
}
