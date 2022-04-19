using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Server.Models
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class MpGateway
    {
        public string v { get; set; }
        public int mid { get; set; }
        public int time { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
        public IEnumerable<byte[]> devices { get; set; }
    }
}
