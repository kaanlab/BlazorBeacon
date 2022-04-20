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
        public ActionResult<IEnumerable<BeaconResponse>> GetAll()
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
            List<CachedBeacon> cachedList = new List<CachedBeacon>();
            var cachedBeacons = _cacheService.CachedBeacons; //GetBeacons();
            var groupedBeacons = cachedBeacons.Where(x => x.GwMac.Equals(mac, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(x => x.TimeStamp).GroupBy(x => x.Mac).ToArray();
            foreach (var beacons in groupedBeacons)
            {
                var aver = beacons.Average(x => x.Distance);
                var beacon = beacons.FirstOrDefault();
                beacon.Distance = aver;
                cachedList.Add(beacon);
            }

            var students = GetStudents();
            var response = cachedList.Join(students, b => b.Mac, s => s.BeaconMac, (b, s) => new BeaconResponse { TimeStamp = b.TimeStamp, Distance = b.Distance.ToString("F"), GwMac = b.GwMac, Mac = b.Mac, GwTopic = b.GwTopic, StudentClass = s.Class, StudentName = s.Name });
            
            return Ok(response);
        }

        [HttpGet("beacon/{mac}")]
        public ActionResult<IEnumerable<BeaconResponse>> GetByBeaconMac(string mac)
        {
            //var beacons = _cacheService.CachedBeacons
            //    .Where(x => x.GwMac.Equals(mac, StringComparison.InvariantCultureIgnoreCase))
            //    .OrderByDescending(x => x.TimeStamp)
            //    .Take(10);

            //return Ok(ToBeaconResponseModel(beacons));
            var cachedBeacons = _cacheService.CachedBeacons; // GetBeacons();
            var filteredBeacons =  cachedBeacons.Where(x => x.Mac.Equals(mac, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(x => x.TimeStamp);

            var students = GetStudents();
            var response = filteredBeacons.Join(students, b => b.Mac, s => s.BeaconMac, (b, s) => new BeaconResponse { TimeStamp = b.TimeStamp, Distance = b.Distance.ToString("F"), GwMac = b.GwMac, Mac = b.Mac, GwTopic = b.GwTopic, StudentClass = s.Class, StudentName = s.Name });

            return Ok(response);

        }

        private static IEnumerable<BeaconResponse> ToBeaconResponseModel(IEnumerable<CachedBeacon> cachedBeacons)
        {
            foreach (var beacon in cachedBeacons)
            {
                yield return new BeaconResponse
                {
                    Mac = beacon.Mac,
                    Distance = beacon.Distance.ToString("F"),
                    GwTopic = beacon.GwTopic,
                    GwMac = beacon.GwMac,
                    TimeStamp = beacon.TimeStamp
                };
            }
        }

        private static IEnumerable<CachedBeacon> GetBeacons()
        {
            var timeStamp = DateTimeOffset.Now;
            var timeStamp2 = timeStamp.AddSeconds(-10);

            return new List<CachedBeacon>()
            {
                new CachedBeacon { Mac = "E9-E1-79-33-78-8F", GwTopic = "gp1/202", Distance = 1.1, GwMac = "8C-AA-B5-97-E1-FC", TimeStamp = timeStamp },
                new CachedBeacon { Mac = "FE-BB-DE-77-08-D5", GwTopic = "gp1/250", Distance = 2.0, GwMac = "33-44-55-66-77-88", TimeStamp = timeStamp },
                new CachedBeacon { Mac = "E9-E1-79-33-78-8F", GwTopic = "gp1/202", Distance = 2.2, GwMac = "8C-AA-B5-97-E1-FC", TimeStamp = timeStamp2 },
                new CachedBeacon { Mac = "FE-BB-DE-77-08-D5", GwTopic = "gp1/250", Distance = 0.5, GwMac = "33-44-55-66-77-88", TimeStamp = timeStamp2 },
            };
        }

        private static IEnumerable<Student> GetStudents()
        {

            return new List<Student>()
            {
                new Student { BeaconMac = "E9-E1-79-33-78-8F", Name = "Иванов И.И.", Class = "5А" },
                new Student { BeaconMac = "FE-BB-DE-77-08-D5", Name = "Петров П.П.", Class = "6Б" },
            };
        }
    }
}
