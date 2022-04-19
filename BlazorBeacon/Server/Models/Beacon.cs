using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Server.Models
{
    public class Beacon
    {
        public string Adv { get; set; }
        public string Mac { get; set; }
        public int Rssi { get; set; }
        public string Uuid { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
        public int TxPower { get; set; }
    }
}
