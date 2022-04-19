namespace BlazorBeacon.Server.Models
{
    public class CachedBeacon
    {
        public DateTimeOffset TimeStamp { get; set; }
        public string Mac { get; set; }
        public string Distance { get; set; }
        public string GatewayMac { get; set; }
    }
}
