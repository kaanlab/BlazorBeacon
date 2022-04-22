using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Shared.Models
{
    public class ClassroomResponse
    {
        public int Id { get; set; }
        public string Cabinet { get; set; }
        public IEnumerable<LessonResponse> LessonsResponse { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
