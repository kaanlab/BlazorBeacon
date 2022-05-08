using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Server.Models
{
    public record Gateway
    {
        public DateTimeOffset TimeStamp { get; init; }
        public string Topic { get; init; }
        public string Version { get; init;  }
        public string Ip { get; init; }
        public string Mac { get; init; }
        public IEnumerable<Beacon> Beacons { get; init; }
    }
}
