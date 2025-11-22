using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTNL.Edu.Models
{
    public class CourseDetail
    {
        [Key]
        public int CourseDetailID { get; set; }


        public int UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }


        public DateTime LastTime { get; set; }


        public ICollection<CourseDetailLesson> CourseDetailLessons { get; set; } = new List<CourseDetailLesson>();
    }
}
