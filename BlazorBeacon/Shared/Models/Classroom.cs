using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Shared.Models
{
    public class Classroom
    {
        public string Cabinet { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
