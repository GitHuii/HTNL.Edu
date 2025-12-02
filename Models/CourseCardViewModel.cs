namespace HTNL.Edu.Models
{
    public class CourseCardViewModel
    {
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; } = "Chưa phân loại";
        public int LessonCount { get; set; }
        public int EnrolledCount { get; set; }
    }
}
