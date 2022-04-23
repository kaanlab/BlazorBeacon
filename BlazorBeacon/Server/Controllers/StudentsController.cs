using BlazorBeacon.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBeacon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("all")]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            return Ok(GetStudents());
        }

        private static IEnumerable<Student> GetStudents()
        {

            return new List<Student>()
            {
                new Student { Id = 1, BeaconMac = "E9-E1-79-33-78-8F", Name = "Иванов И.И.", Class = "5А" },
                new Student { Id = 2, BeaconMac = "FE-BB-DE-77-08-D5", Name = "Петров П.П.", Class = "6Б" }
            };

        }
    }
}
