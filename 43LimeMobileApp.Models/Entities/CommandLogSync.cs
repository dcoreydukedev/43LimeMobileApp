using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _43LimeMobileApp.Models.Entities
{
    public class CommandLogSync
    {
        public string UserId { get; set; }
        public int CommandId { get; set; }
        public long Timestamp { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
    }
}
