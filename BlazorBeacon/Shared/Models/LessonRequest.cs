using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Shared.Models
{
    public class LessonRequest
    {
        public int Id { get; set; }
        public IEnumerable<string> StudentMacs { get; set; }
    }
}
