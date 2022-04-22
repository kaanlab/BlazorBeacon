using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Shared.Models
{
    public class LessonResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string TeacherName { get; set; }
        public string Time { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
