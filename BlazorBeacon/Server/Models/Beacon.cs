using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Server.Models
{
    public record struct Beacon
    {
        public string Adv { get; init; }
        public string Mac { get; init; }
        public int Rssi { get; init; }
        public string Uuid { get; init; }
        public string Major { get; init; }
        public string Minor { get; init; }
        public int TxPower { get; init; }
    }
}
