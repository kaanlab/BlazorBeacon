using BlazorBeacon.Server.Models;
using BlazorBeacon.Server.Services;
using BlazorBeacon.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBeacon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public GatewaysController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<BeaconResponse>> GetFromAll()
        {           
            var beacons = _cacheService.CachedBeacons
                .OrderByDescending(x => x.TimeStamp)
                .Take(10);

            return Ok(ToBeaconResponseModel(beacons));    
        }

        [HttpGet("bymac/{mac}")]
        public ActionResult<IEnumerable<BeaconResponse>> GetByMac(string mac)
        {
            var beacons = _cacheService.CachedBeacons
                .Where(x => x.GwMac.Equals(mac, StringComparison.InvariantCultureIgnoreCase))
                .OrderByDescending(x => x.TimeStamp)
                .Take(10);

            return Ok(ToBeaconResponseModel(beacons));
        }

        private static IEnumerable<BeaconResponse> ToBeaconResponseModel(IEnumerable<CachedBeacon> cachedBeacons)
        {
            foreach (var beacon in cachedBeacons)
            {
                yield return new BeaconResponse
                {
                    Mac = beacon.Mac,
                    Distance = beacon.Distance,
                    GwTopic = beacon.GwTopic,
                    GwMac = beacon.GwMac
                };
            }
        }

    }
}
