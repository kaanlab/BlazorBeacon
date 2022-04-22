using BlazorBeacon.Shared.Extensions;
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
        public ActionResult<Classroom> GetByNumber(string number)
        {
            var classroom = GetClassrooms().FirstOrDefault(x => x.Cabinet.Equals(number, StringComparison.InvariantCultureIgnoreCase));
            //var classroomRespose = new ClassroomResponse()
            //{
            //    Id = classroom.Id,
            //    Cabinet = classroom.Cabinet,
            //    Date = classroom.Date,
            //    LessonsResponse = classroom.Lessons.ToLessonResponseModel()
            //};

            return Ok(classroom);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Lesson>> Add(ClassroomResponse lesson)
        {


            return Ok(lesson);

        }


        private static IEnumerable<Classroom> GetClassrooms()
        {
            var date = DateTimeOffset.Now;
            return new List<Classroom>()
            {
                new Classroom
                {
                    Id = 1,
                    Cabinet = "202",
                    Date = date,
                    Lessons = new List<Lesson>()
                    {
                        new Lesson { Id = 1, Title = "Первый урок", Topic = "Математика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-3) },
                        new Lesson { Id = 2, Title = "Второй урок", Topic = "Физика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-2) },
                        new Lesson { Id = 3, Title = "Третий урок", Topic = "Математика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-1) },
                        new Lesson { Id = 4, Title = "Четвертый урок", Topic = "Математика", TeacherName = "Сидоров С.С.", Time = date, Students = new List<Student>()
                            {
                                new Student { BeaconMac = "E9-E1-79-33-78-8F", Name = "Иванов И.И.", Class = "5А" },
                                new Student { BeaconMac = "FE-BB-DE-77-08-D5", Name = "Петров П.П.", Class = "6Б" },
                            }
                        }
                    }
                },
                new Classroom
                {
                    Id = 2,
                    Cabinet = "250",
                    Date= date,
                    Lessons = new List<Lesson>()
                    {
                        new Lesson { Id = 5, Title = "Первый урок", Topic = "Русский язык", TeacherName = "Васечкин В.В.", Time = date.AddHours(-3) },
                        new Lesson { Id = 6, Title = "Второй урок", Topic = "Лмтература", TeacherName = "Васечкин В.В.", Time = date.AddHours(-2) },
                        new Lesson { Id = 7, Title = "Третий урок", Topic = "Литература", TeacherName = "Васечкин В.В.", Time = date },
                        new Lesson { Id = 8, Title = "Четвертый урок", Topic = "Русский язык", TeacherName = "Васечкин В.В.", Time = date.AddHours(1) }
                    }
                }
            };
        }
    }
}
