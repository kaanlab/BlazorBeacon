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
            //var beacons = _cacheService.CachedBeacons
            //    .OrderByDescending(x => x.TimeStamp)
            //    .Take(10);

            //return Ok(ToBeaconResponseModel(beacons));
            return Ok(GetBeacons());

        }

        [HttpGet("gw/{mac}")]
        public ActionResult<IEnumerable<BeaconResponse>> GetByGwMac(string mac)
        {
            //var beacons = _cacheService.CachedBeacons
            //    .Where(x => x.GwMac.Equals(mac, StringComparison.InvariantCultureIgnoreCase))
            //    .OrderByDescending(x => x.TimeStamp)
            //    .Take(10);

            //return Ok(ToBeaconResponseModel(beacons));
            var beacons = GetBeacons();
            return Ok(beacons.Where(x => x.GwMac.Equals(mac, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(x => x.TimeStamp));

        }

        [HttpGet("beacon/{mac}")]
        public ActionResult<IEnumerable<BeaconResponse>> GetByBeaconMac(string mac)
        {
            //var beacons = _cacheService.CachedBeacons
            //    .Where(x => x.GwMac.Equals(mac, StringComparison.InvariantCultureIgnoreCase))
            //    .OrderByDescending(x => x.TimeStamp)
            //    .Take(10);

            //return Ok(ToBeaconResponseModel(beacons));
            var beacons = GetBeacons();
            return Ok(beacons.Where(x => x.Mac.Equals(mac, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(x => x.TimeStamp));

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
                    GwMac = beacon.GwMac,
                    TimeStamp = beacon.TimeStamp
                };
            }
        }

        private static IEnumerable<BeaconResponse> GetBeacons()
        {
            var timeStamp = DateTimeOffset.Now;
            var timeStamp2 = timeStamp.AddSeconds(-10);

            return new List<BeaconResponse>()
            {
                new BeaconResponse { Mac = "11-22-33-44-55-66", GwTopic = "gp1/202", Distance = "1.1", GwMac = "77-88-99-00-11-22", TimeStamp = timeStamp },
                new BeaconResponse { Mac = "66-55-44-33-22-11", GwTopic = "gp1/250", Distance = "2.0", GwMac = "33-44-55-66-77-88", TimeStamp = timeStamp },
                new BeaconResponse { Mac = "11-22-33-44-55-66", GwTopic = "gp1/202", Distance = "2.2", GwMac = "77-88-99-00-11-22", TimeStamp = timeStamp2 },
                new BeaconResponse { Mac = "66-55-44-33-22-11", GwTopic = "gp1/250", Distance = "0.5", GwMac = "33-44-55-66-77-88", TimeStamp = timeStamp2 },
            };
        }

    }
}
