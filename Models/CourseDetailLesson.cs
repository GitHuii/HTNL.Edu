using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTNL.Edu.Models
{
    public class CourseDetailLesson
    {
        [Key]
        public int CourseDetailLessonID { get; set; }


        public int CourseDetailID { get; set; }
        public CourseDetail CourseDetail { get; set; }


        public int LessonID { get; set; }
        public Lesson Lesson { get; set; }


        public bool IsDone { get; set; }
        public DateTime? CompletedTime { get; set; }
    }
}
