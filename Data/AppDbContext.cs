using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HTNL.Edu.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CourseDetailLesson>()
            .HasOne(cdl => cdl.CourseDetail)
            .WithMany(cd => cd.CourseDetailLessons)
            .HasForeignKey(cdl => cdl.CourseDetailID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CourseDetailLesson>()
            .HasOne(cdl => cdl.Lesson)
            .WithMany(l => l.CourseDetailLessons)
            .HasForeignKey(cdl => cdl.LessonID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>().HasData(
            new User { UserID = 1, FullName = "Admin", Role = "Admin", Streak = 0, Email = "admin@htnl.edu" , UserName = "admin", PassWord = "admin" },
            new User { UserID = 2, FullName = "Nam", Role = "Admin", Streak = 0, Email = "nam@htnl.edu", UserName = "nam", PassWord = "123" },
            new User { UserID = 3, FullName = "Tai", Role = "Admin", Streak = 0, Email = "tai@htnl.edu", UserName = "tai", PassWord = "123" },
            new User { UserID = 4, FullName = "Luong", Role = "Admin", Streak = 0, Email = "luong@htnl.edu", UserName = "luong", PassWord = "123" },
            new User { UserID = 5, FullName = "Huy", Role = "Admin", Streak = 0, Email = "huy@htnl.edu", UserName = "huy", PassWord = "123" }
            );

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryID = 1, CategoryName = "Khóa Học C++" },
            new Category { CategoryID = 2, CategoryName = "Khóa Học C#" },
            new Category { CategoryID = 3, CategoryName = "Khóa Học Java" },
            new Category { CategoryID = 4, CategoryName = "Khóa Học Python" }
            );

        modelBuilder.Entity<Course>().HasData(
            new Course { CourseID = 1, CourseName = "Lập Trình C++ Cơ Bản", Description = "Khóa học lập trình C++ từ cơ bản đến nâng cao", CourseImage = "/img/course_img/1.jpg", CategoryID = 1 },
            new Course { CourseID = 2, CourseName = "Lập Trình C++ Hướng Đối Tượng", Description = "Khóa học lập trình C++ hướng đối tượng", CourseImage = "/img/course_img/2.jpg", CategoryID = 1 },
            new Course { CourseID = 3, CourseName = "Lập Trình C# Cơ Bản", Description = "Khóa học lập trình C# từ cơ bản đến nâng cao", CourseImage = "/img/course_img/3.jpg", CategoryID = 2 },
            new Course { CourseID = 4, CourseName = "Lập Trình C# Winform", Description = "Khóa học lập trình C# Winform", CourseImage = "/img/course_img/4.jpg", CategoryID = 2 },
            new Course { CourseID = 5, CourseName = "Lập Trình Java Cơ Bản", Description = "Khóa học lập trình Java từ cơ bản đến nâng cao", CourseImage = "/img/course_img/5.jpg", CategoryID = 3 },
            new Course { CourseID = 6, CourseName = "Lập Trình Java Swing", Description = "Khóa học lập trình Java Swing", CourseImage = "/img/course_img/6.jpg", CategoryID = 3 },
            new Course { CourseID = 7, CourseName = "Lập Trình Python Cơ Bản", Description = "Khóa học lập trình Python từ cơ bản đến nâng cao", CourseImage = "/img/course_img/7.jpg", CategoryID = 4 },
            new Course { CourseID = 8, CourseName = "Lập Trình Python AI", Description = "Khóa học lập trình Python AI", CourseImage = "/img/course_img/8.jpg", CategoryID = 4 }
            );

        modelBuilder.Entity<Lesson>().HasData(
            new Lesson { LessonID = 1, LessonName = "[ Bài 1 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/1.jpg" , LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw" ,CourseID = 1},
            new Lesson { LessonID = 2, LessonName = "[ Bài 2 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/2.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw", CourseID = 1 },
            new Lesson { LessonID = 3, LessonName = "[ Bài 3 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/3.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw", CourseID = 1 },
            new Lesson { LessonID = 4, LessonName = "[ Bài 4 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/4.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw", CourseID = 1 },
            new Lesson { LessonID = 5, LessonName = "[ Bài 5 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/5.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw" , CourseID = 1 },
            new Lesson { LessonID = 6, LessonName = "[ Bài 6 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/6.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw" , CourseID = 1 },
            new Lesson { LessonID = 7, LessonName = "[ Bài 7 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/7.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw" , CourseID = 1 },
            new Lesson { LessonID = 8, LessonName = "[ Bài 8 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/8.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw" , CourseID = 1 },
            new Lesson { LessonID = 9, LessonName = "[ Bài 9 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/9.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw" , CourseID = 1 },
            new Lesson { LessonID = 10, LessonName = "[ Bài 10 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw", CourseID = 1 }
            );

    }

        public DbSet<HTNL.Edu.Models.User> User { get; set; } = default!;
        public DbSet<HTNL.Edu.Models.Category> Categories { get; set; } = default!;
        public DbSet<HTNL.Edu.Models.Course> Courses { get; set; } = default!;
}
