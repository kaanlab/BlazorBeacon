using BlazorBeacon.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBeacon.Shared.Extensions
{
    public static class ModelMappingExtentions
    {
        public static IEnumerable<LessonResponse> ToLessonResponseModel(this IEnumerable<Lesson> lessons)
        {
            foreach (var lesson in lessons)
            {
                yield return new LessonResponse
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    TeacherName = lesson.TeacherName,
                    Topic = lesson.Topic,
                    Time = lesson.Time.AddHours(lesson.Time.Offset.TotalHours).ToString("HH:mm")                 

                };
            }
        }
    }
}
