using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Shared.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string TeacherName { get; set; }
        public DateTimeOffset Time { get; set; }
        public List<Student> Students { get; set; }
    }
}
