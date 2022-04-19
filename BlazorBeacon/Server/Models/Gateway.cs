using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Server.Models
{
    public class Gateway
    {
        public DateTimeOffset TimeStamp { get; set; }
        public string Topic { get; set; }
        public string Version { get; set; }
        public string Ip { get; set; }
        public string Mac { get; set; }
        public List<Beacon> Beacons { get; set; }
    }
}
