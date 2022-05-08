using BlazorBeacon.Server.Models;
using Microsoft.Extensions.Caching.Memory;

namespace BlazorBeacon.Server.Services
{
    public class CacheService : ICacheService
    {
        public List<CachedBeacon> CachedBeacons { get; set; } = new List<CachedBeacon>();
    }
}
