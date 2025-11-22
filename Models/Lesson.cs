using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTNL.Edu.Models
{
    public class Lesson
    {
        [Key]
        public int LessonID { get; set; }
        public string LessonName { get; set; }
        public string LessonImage { get; set; }
        public string LinkVideo { get; set; }


        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }


        public ICollection<CourseDetailLesson> CourseDetailLessons { get; set; } = new List<CourseDetailLesson>();
    }
}
