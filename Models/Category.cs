using System.ComponentModel.DataAnnotations;

namespace HTNL.Edu.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
