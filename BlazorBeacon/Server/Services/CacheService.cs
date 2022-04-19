using BlazorBeacon.Server.Models;

namespace BlazorBeacon.Server.Services
{
    public class CacheService : ICacheService
    {
        public List<CachedBeacon> CachedBeacons { get; set; } = new List<CachedBeacon>();
    }
}
