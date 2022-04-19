using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Core.Models
{
    public class Beacon
    {
        public string Adv { get; set; }
        public string Mac { get; set; }
        public int Rssi { get; set; }
        public byte[] Advertisment { get; set; }
    }
}
