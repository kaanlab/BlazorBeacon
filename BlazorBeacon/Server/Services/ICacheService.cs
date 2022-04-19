using BlazorBeacon.Server.Models;

namespace BlazorBeacon.Server.Services
{
    public interface ICacheService
    {
        List<CachedBeacon> CachedBeacons { get; set; }
    }
}
