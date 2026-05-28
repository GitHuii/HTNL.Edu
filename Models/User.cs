using System.ComponentModel.DataAnnotations;

namespace HTNL.Edu.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string? FullName { get; set; }
        public string? Role { get; set; }
        public int? Streak { get; set; } = 0;

        public string? Email { get; set; }
        public string? UserName { get; set; }
        /// <summary>Mật khẩu được mã hóa BCrypt (luôn dài 60 ký tự).</summary>
        [MaxLength(60)]
        public string? PassWord { get; set; }


        public ICollection<CourseDetail> CourseDetails { get; set; } = new List<CourseDetail>();
    }
}
