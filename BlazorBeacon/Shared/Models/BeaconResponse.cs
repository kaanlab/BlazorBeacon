using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Shared.Models
{
    public class BeaconResponse
    {
        public string Mac { get; set; }
        public string Distance { get; set; }
        public string GwTopic { get; set; }
        public string GwMac { get; set; }
    }
}
