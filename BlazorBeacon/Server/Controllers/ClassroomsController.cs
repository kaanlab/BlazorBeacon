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
                    GatewayMac = "8C-AA-B5-97-E1-FC",
                    Lessons = new List<Lesson>()
                    {
                        new Lesson { Id = 1, Title = "Первый урок", Topic = "Математика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-3) },
                        new Lesson { Id = 2, Title = "Второй урок", Topic = "Физика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-2) },
                        new Lesson { Id = 3, Title = "Третий урок", Topic = "Математика", TeacherName = "Сидоров С.С.", Time = date.AddHours(-1) },
                        new Lesson { Id = 4, Title = "Четвертый урок", Topic = "Математика", TeacherName = "Андреева Т.В.", Time = date, Students = new List<Student>()
                            {
                                new Student { Id = 1, BeaconMac = "11-E1-79-33-78-8F", Name = "Никитин М.М.", Class = "6Б" },
                                new Student { Id = 2, BeaconMac = "22-BB-DE-77-08-D5", Name = "Смирнов М.П.", Class = "6Б" },
                                new Student { Id = 3, BeaconMac = "33-BB-DE-77-08-D5", Name = "Захаров С.Д.", Class = "6Б" },
                                new Student { Id = 4, BeaconMac = "44-BB-DE-77-08-D5", Name = "Малкин А.Е.", Class = "6Б" },
                                new Student { Id = 5, BeaconMac = "55-BB-DE-77-08-D5", Name = "Матвеев В.Т.", Class = "6Б" },
                                new Student { Id = 6, BeaconMac = "66-BB-DE-77-08-D5", Name = "Быков Д.В.", Class = "6Б" },
                                new Student { Id = 7, BeaconMac = "77-BB-DE-77-08-D5", Name = "Платонов О.Н.", Class = "6Б" },
                                new Student { Id = 8, BeaconMac = "88-BB-DE-77-08-D5", Name = "Мингазов А.В.", Class = "6Б" },
                                new Student { Id = 9, BeaconMac = "99-BB-DE-77-08-D5", Name = "Меркулов С.А.", Class = "6Б" },
                            }
                        }
                    }
                },
                new Classroom
                {
                    Id = 2,
                    Cabinet = "250",
                    Date= date,
                    GatewayMac = "C4-4F-33-6B-DD-99",
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
