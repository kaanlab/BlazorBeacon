using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Server.Models
{
    [MessagePackObject(keyAsPropertyName: true)]
    public record MpGateway
    {
        public string v { get; init; }
        public int mid { get; init; }
        public int time { get; init; }
        public string ip { get; init; }
        public string mac { get; init; }
        public IEnumerable<byte[]> devices { get; init; }
    }
}
