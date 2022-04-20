using BlazorBeacon.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBeacon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        [HttpGet("all")]
        public ActionResult<IEnumerable<Classroom>> GetAll()
        {
            return Ok(GetClassrooms());
        }

        [HttpGet("number/{number}")]
        public ActionResult<IEnumerable<Classroom>> GetByNumber(string number)
        {
            return Ok(GetClassrooms().Where(x => x.Cabinet.Equals(number, StringComparison.InvariantCultureIgnoreCase)));
        }

        private static IEnumerable<Classroom> GetClassrooms()
        {
            var date = DateTimeOffset.Now;
            return new List<Classroom>()
            {
                new Classroom
                {
                    Cabinet = "202",
                    Date = date,
                    Lessons = new List<Lesson>()
                    {
                        new Lesson { Topic = "Математика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-3) },
                        new Lesson { Topic = "Физика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-2) },
                        new Lesson { Topic = "Математика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-1) },
                        new Lesson { Topic = "Математика", TeacherName = "Сидоров С.С.", Time = date }
                    }
                },
                new Classroom
                {
                    Cabinet = "250",
                    Date= date,
                    Lessons = new List<Lesson>()
                    {
                        new Lesson { Topic = "Русский язык", TeacherName = "Васечкин В.В.", Time = date.AddHours(-3) },
                        new Lesson { Topic = "Лмтература", TeacherName = "Васечкин В.В.", Time = date.AddHours(-2) },
                        new Lesson { Topic = "Литература", TeacherName = "Васечкин В.В.", Time = date },
                        new Lesson { Topic = "Русский язык", TeacherName = "Васечкин В.В.", Time = date.AddHours(1) }
                    }
                }
            };
        }
    }
}
