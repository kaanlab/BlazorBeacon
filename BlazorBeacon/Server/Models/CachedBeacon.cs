namespace BlazorBeacon.Server.Models
{
    public class CachedBeacon
    {
        public DateTimeOffset TimeStamp { get; set; }
        public string Mac { get; set; }
        public double Distance { get; set; }
        public string GwMac { get; set; }
        public string GwTopic { get; set; }
    }
}
