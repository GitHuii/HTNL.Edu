using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTNL.Edu.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }


        public string CourseName { get; set; }
        public string Description { get; set; }

        public string? CourseImage { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }


        public ICollection<Lesson>? Lessons { get; set; } = new List<Lesson>();
        public ICollection<CourseDetail>? CourseDetails { get; set; } = new List<CourseDetail>();
    }
}
