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
            // C++ cơ bản
            new Lesson { LessonID = 1, LessonName = "[ Bài 1 ] : Giới Thiệu Về C++", LessonImage = "/img/lesson_img/1/1.jpg" , LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw" ,CourseID = 1},
            new Lesson { LessonID = 2, LessonName = "[ Bài 2 ] :Khái niệm biến? | Nhập xuất dữ liệu trong C++", LessonImage = "/img/lesson_img/1/2.jpg", LinkVideo = "https://www.youtube.com/embed/Z5O6pxQm6II", CourseID = 1 },
            new Lesson { LessonID = 3, LessonName = "[ Bài 3 ] : Kiểu dữ liệu thường gặp trong C++", LessonImage = "/img/lesson_img/1/3.jpg", LinkVideo = "https://www.youtube.com/embed/qpIautEyv2s", CourseID = 1 },
            new Lesson { LessonID = 4, LessonName = "[ Bài 4 ] : Biến cục bộ và biến toàn cục trong C++", LessonImage = "/img/lesson_img/1/4.jpg", LinkVideo = "https://www.youtube.com/embed/79mzaFPLEz8", CourseID = 1 },
            new Lesson { LessonID = 5, LessonName = "[ Bài 5 ] : Hằng số trong C++ | Cách sử dụng hằng số", LessonImage = "/img/lesson_img/1/5.jpg", LinkVideo = "https://www.youtube.com/embed/zccrOA-00lM", CourseID = 1 },
            new Lesson { LessonID = 6, LessonName = "[ Bài 6 ] : Toán tử gán và toán tử số học trong C++", LessonImage = "/img/lesson_img/1/6.jpg", LinkVideo = "https://www.youtube.com/embed/THAJMtm53ZQ", CourseID = 1 },
            new Lesson { LessonID = 7, LessonName = "[ Bài 7 ] : Toán tử quan hệ và toán tử logic trong C++", LessonImage = "/img/lesson_img/1/7.jpg", LinkVideo = "https://www.youtube.com/embed/RX8tkygyHPU", CourseID = 1 },
            new Lesson { LessonID = 8, LessonName = "[ Bài 8 ] : Ép kiểu dữ liệu và bảng mã ASCII trong C++", LessonImage = "/img/lesson_img/1/8.jpg", LinkVideo = "https://www.youtube.com/embed/MTbZLshZg0U", CourseID = 1 },
            new Lesson { LessonID = 9, LessonName = "[ Bài 9 ] : Cấu trúc if else | Cấu trúc rẽ nhánh trong C++", LessonImage = "/img/lesson_img/1/9.jpg", LinkVideo = "https://www.youtube.com/embed/1ppDCzoB03k", CourseID = 1 },
            new Lesson { LessonID = 10, LessonName = "[ Bài 10 ] : Cấu trúc switch case | Cấu trúc rẽ nhánh trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/W3k6lrN0qG4", CourseID = 1 },
            new Lesson { LessonID = 11, LessonName = "[ Bài 11 ] : Toán tử 3 ngôi trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/YKeKmpcMcQY", CourseID = 1 },
            new Lesson { LessonID = 12, LessonName = "[ Bài 12 ] : Vòng lặp trong C++ | Các dạng vòng lặp C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/7uHfTAj3Vao", CourseID = 1 },
            new Lesson { LessonID = 13, LessonName = "[ Bài 13 ] : Câu lệnh break, continue, goto | Câu lệnh trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/r2FMycOy_2Y", CourseID = 1 },
            new Lesson { LessonID = 14, LessonName = "[ Bài 14 ] : Mảng một chiều | Cách khai báo mảng trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/89W1oyXfqgo", CourseID = 1 },
            new Lesson { LessonID = 15, LessonName = "[ Bài 15 ] : Mảng 2 chiều | Khai báo mảng 2 chiều trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/xGpB07JzrQ8", CourseID = 1 },
            new Lesson { LessonID = 16, LessonName = "[ Bài 16 ] : String C++ | Xử lý chuỗi trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/Q06peb_sH6k", CourseID = 1 },
            new Lesson { LessonID = 17, LessonName = "[ Bài 17 ] : Tham số và đối số trong C++ | Function C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/ATAoEb-ZXKI", CourseID = 1 },
            new Lesson { LessonID = 18, LessonName = "[ Bài 18 ] : Tham trị và tham chiếu trong C++ | Call by value & call by reference", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/OQfEPrsWYlY", CourseID = 1 },
            new Lesson { LessonID = 19, LessonName = "[ Bài 19 ] : Đệ quy là gì? | Hàm đệ quy trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/Kuw9OOhEuCw", CourseID = 1 },
            new Lesson { LessonID = 20, LessonName = "[ Bài 20 ] : Con trỏ trong C++ | Pointer C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/uBfsM5RJWSI", CourseID = 1 },
            new Lesson { LessonID = 21, LessonName = "[ Bài 21 ] : Cấp phát động | Cú pháp cấp phát động trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/OIU55ogb26M", CourseID = 1 },
            new Lesson { LessonID = 22, LessonName = "[ Bài 22 ] : Struct là gì? | Struct trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/ZbVO_4jH60k", CourseID = 1 },
            new Lesson { LessonID = 23, LessonName = "[ Bài 23 ] : Nạp chồng toán tử trong C++ | Operator overLoading", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/tNlCid6mQ3E", CourseID = 1 },
            new Lesson { LessonID = 24, LessonName = "[ Bài 24 ] : Làm việc với file text trong C++ | Thư viện fstream", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/LekUWlASyMY", CourseID = 1 },
            // OOP C++
            new Lesson { LessonID = 25, LessonName = "[ Bài 1 ] : Giới thiệu lập trình OOP với C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/5bKotIbNTz0", CourseID = 2 },
            new Lesson { LessonID = 26, LessonName = "[ Bài 2 ] : Các mở rộng trong C++ (phần 1)", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/HUfOgiMepn4", CourseID = 2 },
            new Lesson { LessonID = 27, LessonName = "[ Bài 3 ] : Các mở rộng C++ (phần 2)", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/kAoPGcdjeCg", CourseID = 2 },
            new Lesson { LessonID = 28, LessonName = "[ Bài 4 ] : Lớp và đối tượng (phần 1)", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/X9TBpAOp4ko", CourseID = 2 },
            new Lesson { LessonID = 29, LessonName = "[ Bài 5 ] : Lớp và đối tượng (phần 2)", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/r9Atm0r1Lcs", CourseID = 2 },
            new Lesson { LessonID = 30, LessonName = "[ Bài 6 ] : Lớp và đối tượng (phần 3)", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/lA8Ou11p2IE", CourseID = 2 },
            new Lesson { LessonID = 31, LessonName = "[ Bài 7 ] : Đa năng hoá toán tử C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/YLnpKpeSvFQ", CourseID = 2 },
            new Lesson { LessonID = 32, LessonName = "[ Bài 8 ] : Tính thừa kế trong C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/oerUPXkrVSE", CourseID = 2 },
            new Lesson { LessonID = 33, LessonName = "[ Bài 9 ] : Hàm và lớp template C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/SWpYWWoBVNE", CourseID = 2 },
            new Lesson { LessonID = 34, LessonName = "[ Bài 10 ] : Thao tác với project Dev C++", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/IvjkifaUfmY", CourseID = 2 },
            //C#

            new Lesson { LessonID = 35, LessonName = "[ Bài 1 ] : Cấu trúc lệnh cơ bản", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/FhAIc0tlyaQ", CourseID = 3 },
            new Lesson { LessonID = 36, LessonName = "[ Bài 2 ] : Nhập xuất cơ bản", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/BAscPWPtCD8", CourseID = 3 },
            new Lesson { LessonID = 37, LessonName = "[ Bài 3 ] : Biến trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/IEz7uMSHitM", CourseID = 3 },
            new Lesson { LessonID = 38, LessonName = "[ Bài 4 ] : Kiểu dữ liệu trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/yrH7Qe8FXqE", CourseID = 3 },
            new Lesson { LessonID = 39, LessonName = "[ Bài 5 ] : Toán tử trong C# ", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/niz7Gg8uB-k", CourseID = 3 },
            new Lesson { LessonID = 40, LessonName = "[ Bài 6 ] : Hằng ", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/13NRSYgKh0o", CourseID = 3 },
            new Lesson { LessonID = 41, LessonName = "[ Bài 7 ] : Ép kiểu trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/YmF2kTg0ajU", CourseID = 3 },
            new Lesson { LessonID = 42, LessonName = "[ Bài 8 ] : If else trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/O3ijcGpEgSY", CourseID = 3 },
            new Lesson { LessonID = 43, LessonName = "[ Bài 9 ] : Switch case trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/0NYj4QkJx4U", CourseID = 3 },
            new Lesson { LessonID = 44, LessonName = "[ Bài 10 ] : Kiểu dữ liệu object và từ khóa var", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/SkxQlfdhVko", CourseID = 3 },
            new Lesson { LessonID = 45, LessonName = "[ Bài 11 ] : Kiểu dữ liệu dynamic", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/lM-7tv768XA", CourseID = 3 },
            new Lesson { LessonID = 46, LessonName = "[ Bài 12 ] : Giới thiệu cấu trúc lặp", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/mKLRETK9slQ", CourseID = 3 },
            new Lesson { LessonID = 47, LessonName = "[ Bài 13 ] : Goto", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/4yVPY-Hyg1o", CourseID = 3 },
            new Lesson { LessonID = 48, LessonName = "[ Bài 14 ] : Vòng lặp For", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/QW-1sWoK3Bo", CourseID = 3 },
            new Lesson { LessonID = 49, LessonName = "[ Bài 15 ] : Vòng lặp while", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/06rWN7e55Ic", CourseID = 3 },
            new Lesson { LessonID = 50, LessonName = "[ Bài 16 ] : Vòng lặp do - while", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/0YzMWQ_Tgyw", CourseID = 3 },
            new Lesson { LessonID = 51, LessonName = "[ Bài 17 ] : Cấu trúc của hàm cơ bản", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/EJWEUUNtC4c", CourseID = 3 },
            new Lesson { LessonID = 52, LessonName = "[ Bài 18 ] : Biến toàn cục và biến cục bộ", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/RhVa3B0hFI0", CourseID = 3 },
            new Lesson { LessonID = 53, LessonName = "[ Bài 19 ] : Từ khóa ref và out", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/ciY7Ge4klYE", CourseID = 3 },
            new Lesson { LessonID = 54, LessonName = "[ Bài 20 ] : Mảng 1 chiều", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/UHs1GJ-Ms0k", CourseID = 3 },
            new Lesson { LessonID = 55, LessonName = "[ Bài 21 ] : Mảng 2 chiều trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/4IRA9t1cyWQ", CourseID = 3 },
            new Lesson { LessonID = 56, LessonName = "[ Bài 22 ] : Mảng nhiều chiều trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/bPFa4PHrhaE", CourseID = 3 },
            new Lesson { LessonID = 57, LessonName = "[ Bài 23 ] : Vòng lặp foreach trong C# ", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/M-pJz3jhioU", CourseID = 3 },
            new Lesson { LessonID = 58, LessonName = "[ Bài 24 ] : Lớp String trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/Eizo3hFDAcw", CourseID = 3 },
            new Lesson { LessonID = 59, LessonName = "[ Bài 25 ] : Struct trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/QwzOHtuFuIQ", CourseID = 3 },
            new Lesson { LessonID = 60, LessonName = "[ Bài 26 ] : Regular Expression trong C#", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/bEVigTm5dAo", CourseID = 3 },
            // WINFORM
            new Lesson { LessonID = 61, LessonName = "[ Bài 1 ] : Tổng quan lập trình Winform", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/dtYVRWfGhzI", CourseID = 4 },
            new Lesson { LessonID = 62, LessonName = "[ Bài 2 ] : Tổng quan Form", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/YDbbrrHEnfM", CourseID = 4 },
            new Lesson { LessonID = 63, LessonName = "[ Bài 3 ] : Label", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/hzmW2PlNQ5c", CourseID = 4 },
            new Lesson { LessonID = 64, LessonName = "[ Bài 4 ] : Button", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/hzbvLiCrg-A", CourseID = 4 },
            new Lesson { LessonID = 65, LessonName = "[ Bài 5 ] : Textbox", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/MsSds2bDqKA", CourseID = 4 },
            new Lesson { LessonID = 66, LessonName = "[ Bài 6 ] : Checkbox", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/7mTR8t9qFKI", CourseID = 4 },
            new Lesson { LessonID = 67, LessonName = "[ Bài 7 ] : essagebox", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/2cfnkRI2E8Y", CourseID = 4 },
            new Lesson { LessonID = 68, LessonName = "[ Bài 8 ] : Panel và FlowLayoutPanel", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/Cljvl3ur1wg", CourseID = 4 },
            new Lesson { LessonID = 69, LessonName = "[ Bài 9 ] : RadioButton", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/dnY3DchYdwI", CourseID = 4 },
            new Lesson { LessonID = 70, LessonName = "[ Bài 10 ] : Combobox", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/Uw3vymbSPO0", CourseID = 4 },
            new Lesson { LessonID = 71, LessonName = "[ Bài 11 ] : Picturebox", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/57QobqDr8HM", CourseID = 4 },
            new Lesson { LessonID = 72, LessonName = "[ Bài 12 ] : ListView", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/OLZW6SLYp1Q", CourseID = 4 },
            new Lesson { LessonID = 73, LessonName = "[ Bài 13 ] : TreeView", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/3OEBp3tTt9E", CourseID = 4 },
            new Lesson { LessonID = 74, LessonName = "[ Bài 14 ] : MenuStrip", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/-7B1y9KgxyY", CourseID = 4 },
            new Lesson { LessonID = 75, LessonName = "[ Bài 15 ] : ToolTip", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/zTS4hkYzW_U", CourseID = 4 },
            new Lesson { LessonID = 76, LessonName = "[ Bài 16 ] : StatusBar", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/UGMroKrSb-A", CourseID = 4 },
            new Lesson { LessonID = 77, LessonName = "[ Bài 17 ] : ContextMenu", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/auswGkH9Q-I", CourseID = 4 },
            new Lesson { LessonID = 78, LessonName = "[ Bài 18 ] : NotifyIcon", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/9KIHUn9i--w", CourseID = 4 },
            new Lesson { LessonID = 79, LessonName = "[ Bài 19 ] : Thread trong Winform", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/IJE-eYo983M", CourseID = 4 },
            new Lesson { LessonID = 80, LessonName = "[ Bài 20 ] : Timer", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/NzCE2QComvc", CourseID = 4 },
            new Lesson { LessonID = 81, LessonName = "[ Bài 21 ] : ProcessBar", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/G9t5PLcCydk", CourseID = 4 },
            new Lesson { LessonID = 82, LessonName = "[ Bài 22 ] : NumericUpDown", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/gNrWJB8YQ1c", CourseID = 4 },
            new Lesson { LessonID = 83, LessonName = "[ Bài 23 ] : Process", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/epec4etkaW4", CourseID = 4 },
            new Lesson { LessonID = 84, LessonName = "[ Bài 24 ] : DateTimePicker", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/xDlfghOEMQA", CourseID = 4 },
            new Lesson { LessonID = 85, LessonName = "[ Bài 25 ] : LinQ", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/3YFM0rPsc7o", CourseID = 4 },
            new Lesson { LessonID = 86, LessonName = "[ Bài 26 ] : EntityFrameWork", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/9-hAhYMWj9I", CourseID = 4 },
            // JAVA
            new Lesson { LessonID = 87, LessonName = "[ Bài 1 ] :Giới thiệu Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/3gtOAlcovoQ", CourseID = 5 },
            new Lesson { LessonID = 88, LessonName = "[ Bài 2 ] : Cài đặt môi trường Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/KjMRn1YQcLc", CourseID = 5 },
            new Lesson { LessonID = 89, LessonName = "[ Bài 3 ] : Chương trình Java đầu tiên", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/jIQmebw9VaA", CourseID = 5 },
            new Lesson { LessonID = 90, LessonName = "[ Bài 4 ] : Biến trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/G2mCSTtBojM", CourseID = 5 },
            new Lesson { LessonID = 91, LessonName = "[ Bài 5 ] : Kiểu dữ liệu trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/4k_5vWY2wps", CourseID = 5 },
            new Lesson { LessonID = 92, LessonName = "[ Bài 6 ] : Toán tử trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/H9FmP010A_Q", CourseID = 5 },
            new Lesson { LessonID = 93, LessonName = "[ Bài 7 ] : Hằng trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/dqybUkGCaVw", CourseID = 5 },
            new Lesson { LessonID = 94, LessonName = "[ Bài 8 ] : Ép kiểu trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/kOMiIKLCK34", CourseID = 5 },
            new Lesson { LessonID = 95, LessonName = "[ Bài 9 ] : Cấu trúc rẽ nhánh trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/vradAZcby8I", CourseID = 5 },
            new Lesson { LessonID = 96, LessonName = "[ Bài 10 ] : Vòng lặp While trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/tDfQ33fmmvs", CourseID = 5 },
            new Lesson { LessonID = 97, LessonName = "[ Bài 11 ] : Vòng lặp For trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/1QVfZFOt7uI", CourseID = 5 },
            new Lesson { LessonID = 98, LessonName = "[ Bài 12 ] : Mảng trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/0LX_B3-0XuU", CourseID = 5 },
            new Lesson { LessonID = 99, LessonName = "[ Bài 13 ] : Foreach trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/SVnPYiHS68U", CourseID = 5 },
            new Lesson { LessonID = 100, LessonName = "[ Bài 14 ] : Break và Continue trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/DrFwmhHqiA8", CourseID = 5 },
            new Lesson { LessonID = 101, LessonName = "[ Bài 15 ] : Switch trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/P90f0KWQtv0", CourseID = 5 },
            new Lesson { LessonID = 102, LessonName = "[ Bài 16 ] : OOP trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/8vOnoUZNtCA", CourseID = 5 },
            new Lesson { LessonID = 103, LessonName = "[ Bài 17 ] : Class trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/j_zeaTrH0cU", CourseID = 5 },
            new Lesson { LessonID = 104, LessonName = "[ Bài 18 ] : Phạm vi truy cập trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/s6-UDkzggDk", CourseID = 5 },
            new Lesson { LessonID = 105, LessonName = "[ Bài 19 ] : Static trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/I3tlj977x08", CourseID = 5 },
            new Lesson { LessonID = 106, LessonName = "[ Bài 20 ] : This trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/gIWwValOk0w", CourseID = 5 },
            new Lesson { LessonID = 107, LessonName = "[ Bài 21 ] : Kế thừa trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/FscxYO3s6Go", CourseID = 5 },
            new Lesson { LessonID = 108, LessonName = "[ Bài 22 ] : Setter và Getter trong Java ", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/POoxuSKIP4I", CourseID = 5 },
            new Lesson { LessonID = 109, LessonName = "[ Bài 23 ] : Overriding và Overloading trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/HIz83AG7lYE", CourseID = 5 },
            new Lesson { LessonID = 110, LessonName = "[ Bài 24 ] : Trừu tượng trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/9jlUoO3e2GY", CourseID = 5 },
            new Lesson { LessonID = 111, LessonName = "[ Bài 25 ] : Interface Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/BgNcBfqOLQk", CourseID = 5 },
            new Lesson { LessonID = 112, LessonName = "[ Bài 26 ] : Giải thích hàm main trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/nzurRJhbFl8", CourseID = 5 },
            new Lesson { LessonID = 113, LessonName = "[ Bài 27 ] : Try catch trong Java", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/zI9uen-vRa4", CourseID = 5 },
            new Lesson { LessonID = 114, LessonName = "[ Bài 28 ] : 4 tính chất của OOP", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/u-yPTK1pHQM", CourseID = 5 },
            // JAVA SWING
            new Lesson { LessonID = 115, LessonName = "[ Bài 1 ] :Tạo frame bằng kéo thả với Netbeans.", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/Mkgo_9ZgWJs", CourseID = 6 },
            new Lesson { LessonID = 116, LessonName = "[ Bài 2 ] :Button và xử lí sự kiện đơn giản cho button", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/GrYN299AytE", CourseID = 6 },
            new Lesson { LessonID = 117, LessonName = "[ Bài 3 ] :Xử lí sự kiện đơn giản cho button (code tay)", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/ox-tsrRXGec", CourseID = 6 },
            new Lesson { LessonID = 118, LessonName = "[ Bài 4 ] :Thiết lập phím tắt và chú thích chức năng cho Button.", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/YaG4L712CwM", CourseID = 6 },
            new Lesson { LessonID = 119, LessonName = "[ Bài 5 ] :Giới thiệu Bảng và thao tác với bảng: thêm và xóa hàng", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/UT_qIE0bJ7c", CourseID = 6 },
            new Lesson { LessonID = 120, LessonName = "[ Bài 6 ] :Cách sử dụng label-nhãn", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/6FbhKQWkFZE", CourseID = 6 },
            new Lesson { LessonID = 121, LessonName = "[ Bài 7 ] :Cách sử dụng Text Field", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/4UkDu38oW8U", CourseID = 6 },
            new Lesson { LessonID = 122, LessonName = "[ Bài 8 ] :Tạo bảng và chèn dữ liệu vào bảng", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/RhF2ivqCw94", CourseID = 6 },
            new Lesson { LessonID = 123, LessonName = "[ Bài 9 ] :Tạo tên các cột cho bảng có thể dùng chung", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/2s8Kh3RZYuQ", CourseID = 6 },
            new Lesson { LessonID = 124, LessonName = "[ Bài 10 ] :Chèn dữ liệu vào bảng và đọc dữ liệu từ bảng CSDL", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/Zox6MMNEmJw", CourseID = 6 },
            new Lesson { LessonID = 125, LessonName = "[ Bài 11 ] :Hướng dẫn đọc ghi thông tin vào file text", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/7FLXaEi__9U", CourseID = 6 },
            new Lesson { LessonID = 126, LessonName = "[ Bài 12 ] :Giới thiệu ứng dụng quản lý bạn đọc thư viện.", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/EKo6qMyz02M", CourseID = 6 },
            new Lesson { LessonID = 127, LessonName = "[ Bài 13 ] :Vẽ sơ đồ tuần tự chức năng thêm mới bạn đọc", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/B9wucDnekvA", CourseID = 6 },
            new Lesson { LessonID = 128, LessonName = "[ Bài 14 ] :Cài đặt chức năng thêm mới bạn đọc ", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/M63VZ2c0mVQ", CourseID = 6 },
            new Lesson { LessonID = 129, LessonName = "[ Bài 15 ] :Cài đặt chức năng cho frame nhập thông tin bạn đọc", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/MmvaO45rkD4", CourseID = 6 },
            new Lesson { LessonID = 130, LessonName = "[ Bài 16 ] :Kết nối và cập nhật cơ sở dữ liệu.", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/LnSqeDXuPOY", CourseID = 6 },
            new Lesson { LessonID = 131, LessonName = "[ Bài 17 ] :Thêm Dữ Liệu Vào Bảng từ JDialog Form - P1", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/uwZTOQFqJbw", CourseID = 6 },
            new Lesson { LessonID = 132, LessonName = "[ Bài 18 ] :Thêm Dữ Liệu Vào Bảng từ JDialog Form - P2", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/qmld4g2xihQ", CourseID = 6 },
            new Lesson { LessonID = 133, LessonName = "[ Bài 19 ] :Thêm Dữ Liệu Vào Bảng từ JDialog Form - P3", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/BNb0NwV0Bes", CourseID = 6 },
            new Lesson { LessonID = 134, LessonName = "[ Bài 20 ] :Sửa dữ liệu từ bảng với JDialog Form - P1", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/EdhtzM57M_E", CourseID = 6 },
            new Lesson { LessonID = 135, LessonName = "[ Bài 21 ] :Sửa dữ liệu từ bảng với JDialog Form - P2", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/yI67t0-5Lfs", CourseID = 6 },
            new Lesson { LessonID = 136, LessonName = "[ Bài 22 ] :Xóa dữ liệu khỏi bảng qua Default Table Model", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/vWOYWYrv6-4", CourseID = 6 },
            new Lesson { LessonID = 137, LessonName = "[ Bài 23 ] :Thêm các tab mới vào cho ứng dụng", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/jv2M5vmusmw", CourseID = 6 },
            new Lesson { LessonID = 138, LessonName = "[ Bài 24 ] :Thêm và căn chỉnh hàng cột cho bảng", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/VwHJkjvgrOY", CourseID = 6 },
            new Lesson { LessonID = 139, LessonName = "[ Bài 25 ] :Thêm/sửa các nhãn tùy chọn trong Combo box", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/dgBrZCwoR8w", CourseID = 6 },
            new Lesson { LessonID = 140, LessonName = "[ Bài 26 ] :Lấy dữ liệu đã chọn từ Combo box", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/C25n5Do0PvI", CourseID = 6 },
            new Lesson { LessonID = 141, LessonName = "[ Bài 27 ] : Đẩy dữ liệu vào combo box", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/qaX1i1rFajU", CourseID = 6 },
            new Lesson { LessonID = 142, LessonName = "[ Bài 28 ] :Xử lý ngoại lệ khi nhập sai/để trống thông tin bắt buộc", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/CaLEJEtnBN0", CourseID = 6 },
            // PYTHON 
            new Lesson { LessonID = 143, LessonName = "[ Bài 1 ] :Giới thiệu ngôn ngữ lập trình Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/NZj6LI5a9vc", CourseID = 7 },
            new Lesson { LessonID = 144, LessonName = "[ Bài 2 ] :Cài đặt môi trường Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/jf-q_dG8WzI", CourseID = 7 },
            new Lesson { LessonID = 145, LessonName = "[ Bài 3 ] : Chạy file Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/QFxqY8qv42E", CourseID = 7 },
            new Lesson { LessonID = 146, LessonName = "[ Bài 4 ] :Comment trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/t3dERE9T5yg", CourseID = 7 },
            new Lesson { LessonID = 147, LessonName = "[ Bài 5 ] :Biến(Variable) trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/nclE18Yl-kA", CourseID = 7 },
            new Lesson { LessonID = 148, LessonName = "[ Bài 6 ] :Kiểu số trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/IAVvgqDBiv0", CourseID = 7 },
            new Lesson { LessonID = 149, LessonName = "[ Bài 7 ] :Kiểu chuỗi trong Python - Phần 1", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/Vb6XWSLPQfg", CourseID = 7 },
            new Lesson { LessonID = 150, LessonName = "[ Bài 8 ] :Kiểu chuỗi trong Python - Phần 2", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/gzWriEOVjU0", CourseID = 7 },
            new Lesson { LessonID = 151, LessonName = "[ Bài 9 ] : Kiểu chuỗi trong Python p3", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/LRUHnuHljPQ", CourseID = 7 },
            new Lesson { LessonID = 152, LessonName = "[ Bài 10 ] : Kiểu chuỗi trong Python - Phần 4", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/q2TNJMBx6GE", CourseID = 7 },
            new Lesson { LessonID = 153, LessonName = "[ Bài 11 ] : Kiểu chuỗi trong Python - Phần 5", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/u2Kd3weqPZE", CourseID = 7 },
            new Lesson { LessonID = 154, LessonName = "[ Bài 12 ] : List trong Python - Phần 1", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/UzTE665WXb8", CourseID = 7 },
            new Lesson { LessonID = 155, LessonName = "[ Bài 13 ] :List trong Python - Phần 2", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/9IH3EynbcpU", CourseID = 7 },
            new Lesson { LessonID = 156, LessonName = "[ Bài 14 ] :Tuple trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/dDFFCbRGm3o", CourseID = 7 },
            new Lesson { LessonID = 157, LessonName = "[ Bài 15 ] :Hashable và Unhashable trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/gw9zbl2Q7r4", CourseID = 7 },
            new Lesson { LessonID = 158, LessonName = "[ Bài 16 ] : Set trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/S-CWHkKiOBs", CourseID = 7 },
            new Lesson { LessonID = 159, LessonName = "[ Bài 17 ] :Dict trong Python - Phần 1", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/zFDTmZjJFws", CourseID = 7 },
            new Lesson { LessonID = 160, LessonName = "[ Bài 18 ] :Dict trong Python - Phần 2", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/jmwBKuJl2Zg", CourseID = 7 },
            new Lesson { LessonID = 161, LessonName = "[ Bài 19 ] : Xử lý File trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/6J8-jkoRBXw", CourseID = 7 },
            new Lesson { LessonID = 162, LessonName = "[ Bài 20 ] :Iteration trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/GSUwh958k_A", CourseID = 7 },
            new Lesson { LessonID = 163, LessonName = "[ Bài 21 ] :Hàm xuất trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/rhOyCSIf1is", CourseID = 7 },
            new Lesson { LessonID = 164, LessonName = "[ Bài 22 ] : Hàm nhập trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/rK4MphZVhDM", CourseID = 7 },
            new Lesson { LessonID = 165, LessonName = "[ Bài 23 ] :Kiểu Boolean trong python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/iB9EhSZvfFk", CourseID = 7 },
            new Lesson { LessonID = 166, LessonName = "[ Bài 24 ] :Cấu trúc rẽ nhánh trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/4_Jb1xZsDJ8", CourseID = 7 },
            new Lesson { LessonID = 167, LessonName = "[ Bài 25 ] :While Loop trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/wq7Th3nXyCQ", CourseID = 7 },
            new Lesson { LessonID = 168, LessonName = "[ Bài 26 ] : For Loop trong Python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/9TxJ71NNO64", CourseID = 7 },
            new Lesson { LessonID = 169, LessonName = "[ Bài 27 ] : Function trong python", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/a6FnNvt3Fw4", CourseID = 7 },
            new Lesson { LessonID = 170, LessonName = "[ Bài 28 ] :Function trong python - Positional", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/M77p3PB-qzM", CourseID = 7 },
            new Lesson { LessonID = 171, LessonName = "[ Bài 29 ] : Function trong Python - Packing và unpacking", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/0Gf5MVTWuCY", CourseID = 7 },
            new Lesson { LessonID = 172, LessonName = "[ Bài 30 ] :Function trong Python - Locals và globals", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/w7qnt6iIakM", CourseID = 7 },
            new Lesson { LessonID = 173, LessonName = "[ Bài 31 ] :Function trong Python - Return", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/3bdMH8z50zE", CourseID = 7 },
            new Lesson { LessonID = 174, LessonName = "[ Bài 32 ] :Function trong Python - Yield", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "http://youtube.com/embed/aChGfj5h3UQ", CourseID = 7 },
            new Lesson { LessonID = 175, LessonName = "[ Bài 33 ] :Function trong Python - Lambda", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/7YTL1u5Ja5A", CourseID = 7 },
            new Lesson { LessonID = 176, LessonName = "[ Bài 34 ] :Function trong Python - Functional tools", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/W5Xvw_2WPeg", CourseID = 7 },
            new Lesson { LessonID = 177, LessonName = "[ Bài 35 ] :Function trong Python - Đệ Quy", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/AFNMgGmWcdQ", CourseID = 7 },
            // PYTHON MACHINE LEARNING
            new Lesson { LessonID = 178, LessonName = "[ Bài 1 ] :Giới thiệu Machine learning", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/_ZjIv2D6T40", CourseID = 8 },
            new Lesson { LessonID = 179, LessonName = "[ Bài 2 ] :Ma trận và vector với NumPy", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/oykpEGJrf20", CourseID = 8 },
            new Lesson { LessonID = 180, LessonName = "[ Bài 3 ] :Giới thiệu Linear Regression và hàm hθ(x)", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/_KfLLePcgCs", CourseID = 8 },
            new Lesson { LessonID = 181, LessonName = "[ Bài 4 ] :Hàm J(θ) cho Linear Regression", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/sLC88fIG9Q8", CourseID = 8 },
            new Lesson { LessonID = 182, LessonName = "[ Bài 5 ] :Gradient Descent cho Linear Regression", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/VwIjFJ-5Zbk", CourseID = 8 },
            new Lesson { LessonID = 183, LessonName = "[ Bài 6 ] :Feature Nomalize", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/qPWbs7t4FVI", CourseID = 8 },
            new Lesson { LessonID = 184, LessonName = "[ Bài 7 ] :Normal Equation cho Linear Regression", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/eDdxtTcLBRM", CourseID = 8 },
            new Lesson { LessonID = 185, LessonName = "[ Bài 8 ] :Hồi quy tuyến tính Linear Regression trong Học máy Machine Learning", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/aeDzuGQ6DDk", CourseID = 8 },
            new Lesson { LessonID = 186, LessonName = "[ Bài 9 ] :Logistic Regression phân loại chữ số viết tay | Phân tích toán học", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/wNUJM4vQ1z8", CourseID = 8 },
            new Lesson { LessonID = 187, LessonName = "[ Bài 10 ] :Logistic Regression phân loại chữ số viết tay | Hướng dẫn lập trình", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/n0k26uJR09U", CourseID = 8 },
            new Lesson { LessonID = 188, LessonName = "[ Bài 11 ] :Deep Learning - Tổng quan và giải thích về mạng học sâu| Mạng nơron tích chập| CNN dễ hiểu nhất", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/NL6eCtMjikQ", CourseID = 8 },
            new Lesson { LessonID = 189, LessonName = "[ Bài 12 ] :Machine Learning - Ứng dụng web python flask nhận dạng chữ số viết tay | Phần 1", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/joIGaPC2G-w", CourseID = 8 },
            new Lesson { LessonID = 190, LessonName = "[ Bài 13 ] :Machine Learning - Ứng dụng web python flask nhận dạng chữ số viết tay | Phần 2", LessonImage = "/img/lesson_img/1/10.jpg", LinkVideo = "https://www.youtube.com/embed/glTMMbnzHXA", CourseID = 8 }
            );

    }

        public DbSet<HTNL.Edu.Models.User> User { get; set; } = default!;
        public DbSet<HTNL.Edu.Models.Category> Categories { get; set; } = default!;
        public DbSet<HTNL.Edu.Models.Course> Courses { get; set; } = default!;
}
