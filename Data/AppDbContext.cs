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
            new Course { CourseID = 1, CourseName = "Lập Trình C++ Cơ Bản", Description = "Khóa học lập trình C++ từ cơ bản đến nâng cao", CourseImage = "/img/course_img/thumbnail.jpg", CategoryID = 1 },
            new Course { CourseID = 2, CourseName = "Lập Trình C++ Hướng Đối Tượng", Description = "Khóa học lập trình C++ hướng đối tượng", CourseImage = "/img/course_img/thumbnail2.jpg", CategoryID = 1 },
            new Course { CourseID = 3, CourseName = "Lập Trình C# Cơ Bản", Description = "Khóa học lập trình C# từ cơ bản đến nâng cao", CourseImage = "/img/course_img/thumbnail5.jpg", CategoryID = 2 },
            new Course { CourseID = 4, CourseName = "Lập Trình C# Winform", Description = "Khóa học lập trình C# Winform", CourseImage = "/img/course_img/thumbnail6.jpg", CategoryID = 2 },
            new Course { CourseID = 5, CourseName = "Lập Trình Java Cơ Bản", Description = "Khóa học lập trình Java từ cơ bản đến nâng cao", CourseImage = "/img/course_img/thumbnail3.jpg", CategoryID = 3 },
            new Course { CourseID = 6, CourseName = "Lập Trình Java Swing", Description = "Khóa học lập trình Java Swing", CourseImage = "/img/course_img/thumbnail4.jpg", CategoryID = 3 },
            new Course { CourseID = 7, CourseName = "Lập Trình Python Cơ Bản", Description = "Khóa học lập trình Python từ cơ bản đến nâng cao", CourseImage = "/img/course_img/thumbnail7.jpg", CategoryID = 4 },
            new Course { CourseID = 8, CourseName = "Lập Trình Python AI", Description = "Khóa học lập trình Python AI", CourseImage = "/img/course_img/thumbnail8.jpg", CategoryID = 4 }
            );

        modelBuilder.Entity<Lesson>().HasData(
            // C++ cơ bản
            new Lesson { LessonID = 1, LessonName = "[ Bài 1 ] : Giới Thiệu Về C++", LinkVideo = "https://www.youtube.com/embed/74B6PXO97Tw"  ,Duration = 7,CourseID = 1},
            new Lesson { LessonID = 2, LessonName = "[ Bài 2 ] :Khái niệm biến? | Nhập xuất dữ liệu trong C++", LinkVideo = "https://www.youtube.com/embed/Z5O6pxQm6II", Duration = 5, CourseID = 1 },
            new Lesson { LessonID = 3, LessonName = "[ Bài 3 ] : Kiểu dữ liệu thường gặp trong C++", LinkVideo = "https://www.youtube.com/embed/qpIautEyv2s", Duration = 6, CourseID = 1 },
            new Lesson { LessonID = 4, LessonName = "[ Bài 4 ] : Biến cục bộ và biến toàn cục trong C++", LinkVideo = "https://www.youtube.com/embed/79mzaFPLEz8", Duration = 3, CourseID = 1 },
            new Lesson { LessonID = 5, LessonName = "[ Bài 5 ] : Hằng số trong C++ | Cách sử dụng hằng số", LinkVideo = "https://www.youtube.com/embed/zccrOA-00lM", Duration = 10, CourseID = 1 },
            new Lesson { LessonID = 6, LessonName = "[ Bài 6 ] : Toán tử gán và toán tử số học trong C++", LinkVideo = "https://www.youtube.com/embed/THAJMtm53ZQ", Duration = 12, CourseID = 1 },
            new Lesson { LessonID = 7, LessonName = "[ Bài 7 ] : Toán tử quan hệ và toán tử logic trong C++", LinkVideo = "https://www.youtube.com/embed/RX8tkygyHPU", Duration = 9, CourseID = 1 },
            new Lesson { LessonID = 8, LessonName = "[ Bài 8 ] : Ép kiểu dữ liệu và bảng mã ASCII trong C++", LinkVideo = "https://www.youtube.com/embed/MTbZLshZg0U", Duration =9, CourseID = 1 },
            new Lesson { LessonID = 9, LessonName = "[ Bài 9 ] : Cấu trúc if else | Cấu trúc rẽ nhánh trong C++", LinkVideo = "https://www.youtube.com/embed/1ppDCzoB03k", Duration = 11, CourseID = 1 },
            new Lesson { LessonID = 10, LessonName = "[ Bài 10 ] : Cấu trúc switch case | Cấu trúc rẽ nhánh trong C++", LinkVideo = "https://www.youtube.com/embed/W3k6lrN0qG4", Duration = 5, CourseID = 1 },
            new Lesson { LessonID = 11, LessonName = "[ Bài 11 ] : Toán tử 3 ngôi trong C++", LinkVideo = "https://www.youtube.com/embed/YKeKmpcMcQY", Duration = 12, CourseID = 1 },
            new Lesson { LessonID = 12, LessonName = "[ Bài 12 ] : Vòng lặp trong C++ | Các dạng vòng lặp C++", LinkVideo = "https://www.youtube.com/embed/7uHfTAj3Vao", Duration = 13, CourseID = 1 },
            new Lesson { LessonID = 13, LessonName = "[ Bài 13 ] : Câu lệnh break, continue, goto | Câu lệnh trong C++", LinkVideo = "https://www.youtube.com/embed/r2FMycOy_2Y", Duration = 9, CourseID = 1 },
            new Lesson { LessonID = 14, LessonName = "[ Bài 14 ] : Mảng một chiều | Cách khai báo mảng trong C++", LinkVideo = "https://www.youtube.com/embed/89W1oyXfqgo", Duration = 7, CourseID = 1 },
            new Lesson { LessonID = 15, LessonName = "[ Bài 15 ] : Mảng 2 chiều | Khai báo mảng 2 chiều trong C++", LinkVideo = "https://www.youtube.com/embed/xGpB07JzrQ8", Duration = 19, CourseID = 1 },
            new Lesson { LessonID = 16, LessonName = "[ Bài 16 ] : String C++ | Xử lý chuỗi trong C++", LinkVideo = "https://www.youtube.com/embed/Q06peb_sH6k", Duration = 7, CourseID = 1 },
            new Lesson { LessonID = 17, LessonName = "[ Bài 17 ] : Tham số và đối số trong C++ | Function C++", LinkVideo = "https://www.youtube.com/embed/ATAoEb-ZXKI", Duration = 8, CourseID = 1 },
            new Lesson { LessonID = 18, LessonName = "[ Bài 18 ] : Tham trị và tham chiếu trong C++ | Call by value & call by reference", LinkVideo = "https://www.youtube.com/embed/OQfEPrsWYlY", Duration = 12, CourseID = 1 },
            new Lesson { LessonID = 19, LessonName = "[ Bài 19 ] : Đệ quy là gì? | Hàm đệ quy trong C++", LinkVideo = "https://www.youtube.com/embed/Kuw9OOhEuCw", Duration = 7, CourseID = 1 },
            new Lesson { LessonID = 20, LessonName = "[ Bài 20 ] : Con trỏ trong C++ | Pointer C++", LinkVideo = "https://www.youtube.com/embed/uBfsM5RJWSI", Duration = 9, CourseID = 1 },
            new Lesson { LessonID = 21, LessonName = "[ Bài 21 ] : Cấp phát động | Cú pháp cấp phát động trong C++", LinkVideo = "https://www.youtube.com/embed/OIU55ogb26M", Duration = 6, CourseID = 1 },
            new Lesson { LessonID = 22, LessonName = "[ Bài 22 ] : Struct là gì? | Struct trong C++", LinkVideo = "https://www.youtube.com/embed/ZbVO_4jH60k", Duration = 16, CourseID = 1 },
            new Lesson { LessonID = 23, LessonName = "[ Bài 23 ] : Nạp chồng toán tử trong C++ | Operator overLoading", LinkVideo = "https://www.youtube.com/embed/tNlCid6mQ3E", Duration = 11, CourseID = 1 },
            new Lesson { LessonID = 24, LessonName = "[ Bài 24 ] : Làm việc với file text trong C++ | Thư viện fstream", LinkVideo = "https://www.youtube.com/embed/LekUWlASyMY", Duration = 19, CourseID = 1 },
            // OOP C++
            new Lesson { LessonID = 25, LessonName = "[ Bài 1 ] : Giới thiệu lập trình OOP với C++" , LinkVideo = "https://www.youtube.com/embed/5bKotIbNTz0", Duration = 16, CourseID = 2 },
            new Lesson { LessonID = 26, LessonName = "[ Bài 2 ] : Các mở rộng trong C++ (phần 1)" , LinkVideo = "https://www.youtube.com/embed/HUfOgiMepn4", Duration = 31, CourseID = 2 },
            new Lesson { LessonID = 27, LessonName = "[ Bài 3 ] : Các mở rộng C++ (phần 2)" , LinkVideo = "https://www.youtube.com/embed/kAoPGcdjeCg", Duration = 23, CourseID = 2 },
            new Lesson { LessonID = 28, LessonName = "[ Bài 4 ] : Lớp và đối tượng (phần 1)" , LinkVideo = "https://www.youtube.com/embed/X9TBpAOp4ko", Duration = 40, CourseID = 2 },
            new Lesson { LessonID = 29, LessonName = "[ Bài 5 ] : Lớp và đối tượng (phần 2)" , LinkVideo = "https://www.youtube.com/embed/r9Atm0r1Lcs", Duration = 43, CourseID = 2 },
            new Lesson { LessonID = 30, LessonName = "[ Bài 6 ] : Lớp và đối tượng (phần 3)" , LinkVideo = "https://www.youtube.com/embed/lA8Ou11p2IE", Duration = 44, CourseID = 2 },
            new Lesson { LessonID = 31, LessonName = "[ Bài 7 ] : Đa năng hoá toán tử C++" , LinkVideo = "https://www.youtube.com/embed/YLnpKpeSvFQ", Duration = 35, CourseID = 2 },
            new Lesson { LessonID = 32, LessonName = "[ Bài 8 ] : Tính thừa kế trong C++" , LinkVideo = "https://www.youtube.com/embed/oerUPXkrVSE", Duration = 25, CourseID = 2 },
            new Lesson { LessonID = 33, LessonName = "[ Bài 9 ] : Hàm và lớp template C++" , LinkVideo = "https://www.youtube.com/embed/SWpYWWoBVNE", Duration = 19, CourseID = 2 },
            new Lesson { LessonID = 34, LessonName = "[ Bài 10 ] : Thao tác với project Dev C++" , LinkVideo = "https://www.youtube.com/embed/IvjkifaUfmY", Duration = 31, CourseID = 2 },
            //C#

            new Lesson { LessonID = 35, LessonName = "[ Bài 1 ] : Cấu trúc lệnh cơ bản" , LinkVideo = "https://www.youtube.com/embed/FhAIc0tlyaQ", Duration = 23, CourseID = 3 },
            new Lesson { LessonID = 36, LessonName = "[ Bài 2 ] : Nhập xuất cơ bản" , LinkVideo = "https://www.youtube.com/embed/BAscPWPtCD8", Duration = 30, CourseID = 3 },
            new Lesson { LessonID = 37, LessonName = "[ Bài 3 ] : Biến trong C#" , LinkVideo = "https://www.youtube.com/embed/IEz7uMSHitM", Duration = 25, CourseID = 3 },
            new Lesson { LessonID = 38, LessonName = "[ Bài 4 ] : Kiểu dữ liệu trong C#" , LinkVideo = "https://www.youtube.com/embed/yrH7Qe8FXqE", Duration = 20, CourseID = 3 },
            new Lesson { LessonID = 39, LessonName = "[ Bài 5 ] : Toán tử trong C# " , LinkVideo = "https://www.youtube.com/embed/niz7Gg8uB-k", Duration = 20, CourseID = 3 },
            new Lesson { LessonID = 40, LessonName = "[ Bài 6 ] : Hằng " , LinkVideo = "https://www.youtube.com/embed/13NRSYgKh0o", Duration = 11, CourseID = 3 },
            new Lesson { LessonID = 41, LessonName = "[ Bài 7 ] : Ép kiểu trong C#" , LinkVideo = "https://www.youtube.com/embed/YmF2kTg0ajU", Duration = 22, CourseID = 3 },
            new Lesson { LessonID = 42, LessonName = "[ Bài 8 ] : If else trong C#" , LinkVideo = "https://www.youtube.com/embed/O3ijcGpEgSY", Duration = 17, CourseID = 3 },
            new Lesson { LessonID = 43, LessonName = "[ Bài 9 ] : Switch case trong C#" , LinkVideo = "https://www.youtube.com/embed/0NYj4QkJx4U", Duration = 13, CourseID = 3 },
            new Lesson { LessonID = 44, LessonName = "[ Bài 10 ] : Kiểu dữ liệu object và từ khóa var" , LinkVideo = "https://www.youtube.com/embed/SkxQlfdhVko", Duration = 12, CourseID = 3 },
            new Lesson { LessonID = 45, LessonName = "[ Bài 11 ] : Kiểu dữ liệu dynamic" , LinkVideo = "http://youtube.com/embed/lM-7tv768XA", Duration = 7, CourseID = 3 },
            new Lesson { LessonID = 46, LessonName = "[ Bài 12 ] : Giới thiệu cấu trúc lặp" , LinkVideo = "https://www.youtube.com/embed/mKLRETK9slQ", Duration = 6, CourseID = 3 },
            new Lesson { LessonID = 47, LessonName = "[ Bài 13 ] : Goto" , LinkVideo = "https://www.youtube.com/embed/4yVPY-Hyg1o", Duration = 16, CourseID = 3 },
            new Lesson { LessonID = 48, LessonName = "[ Bài 14 ] : Vòng lặp For" , LinkVideo = "https://www.youtube.com/embed/QW-1sWoK3Bo", Duration = 30, CourseID = 3 },
            new Lesson { LessonID = 49, LessonName = "[ Bài 15 ] : Vòng lặp while" , LinkVideo = "https://www.youtube.com/embed/06rWN7e55Ic", Duration = 13, CourseID = 3 },
            new Lesson { LessonID = 50, LessonName = "[ Bài 16 ] : Vòng lặp do - while" , LinkVideo = "https://www.youtube.com/embed/0YzMWQ_Tgyw", Duration = 6, CourseID = 3 },
            new Lesson { LessonID = 51, LessonName = "[ Bài 17 ] : Cấu trúc của hàm cơ bản" , LinkVideo = "https://www.youtube.com/embed/EJWEUUNtC4c", Duration = 20, CourseID = 3 },
            new Lesson { LessonID = 52, LessonName = "[ Bài 18 ] : Biến toàn cục và biến cục bộ" , LinkVideo = "https://www.youtube.com/embed/RhVa3B0hFI0", Duration = 10, CourseID = 3 },
            new Lesson { LessonID = 53, LessonName = "[ Bài 19 ] : Từ khóa ref và out" , LinkVideo = "https://www.youtube.com/embed/ciY7Ge4klYE", Duration = 10, CourseID = 3 },
            new Lesson { LessonID = 54, LessonName = "[ Bài 20 ] : Mảng 1 chiều" , LinkVideo = "https://www.youtube.com/embed/UHs1GJ-Ms0k", Duration = 24, CourseID = 3 },
            new Lesson { LessonID = 55, LessonName = "[ Bài 21 ] : Mảng 2 chiều trong C#" , LinkVideo = "https://www.youtube.com/embed/4IRA9t1cyWQ", Duration = 28, CourseID = 3 },
            new Lesson { LessonID = 56, LessonName = "[ Bài 22 ] : Mảng nhiều chiều trong C#" , LinkVideo = "https://www.youtube.com/embed/bPFa4PHrhaE", Duration = 17, CourseID = 3 },
            new Lesson { LessonID = 57, LessonName = "[ Bài 23 ] : Vòng lặp foreach trong C# " , LinkVideo = "https://www.youtube.com/embed/M-pJz3jhioU", Duration = 17, CourseID = 3 },
            new Lesson { LessonID = 58, LessonName = "[ Bài 24 ] : Lớp String trong C#" , LinkVideo = "https://www.youtube.com/embed/Eizo3hFDAcw", Duration = 13, CourseID = 3 },
            new Lesson { LessonID = 59, LessonName = "[ Bài 25 ] : Struct trong C#" , LinkVideo = "https://www.youtube.com/embed/QwzOHtuFuIQ", Duration = 12, CourseID = 3 },
            new Lesson { LessonID = 60, LessonName = "[ Bài 26 ] : Regular Expression trong C#" , LinkVideo = "http://youtube.com/embed/bEVigTm5dAo", Duration = 40, CourseID = 3 },
            // WINFORM
            new Lesson { LessonID = 61, LessonName = "[ Bài 1 ] : Tổng quan lập trình Winform" , LinkVideo = "https://www.youtube.com/embed/dtYVRWfGhzI", Duration = 5, CourseID = 4 },
            new Lesson { LessonID = 62, LessonName = "[ Bài 2 ] : Tổng quan Form" , LinkVideo = "https://www.youtube.com/embed/YDbbrrHEnfM", Duration = 47, CourseID = 4 },
            new Lesson { LessonID = 63, LessonName = "[ Bài 3 ] : Label" , LinkVideo = "https://www.youtube.com/embed/hzmW2PlNQ5c", Duration = 26, CourseID = 4 },
            new Lesson { LessonID = 64, LessonName = "[ Bài 4 ] : Button" , LinkVideo = "https://www.youtube.com/embed/hzbvLiCrg-A", Duration = 12, CourseID = 4 },
            new Lesson { LessonID = 65, LessonName = "[ Bài 5 ] : Textbox" , LinkVideo = "https://www.youtube.com/embed/MsSds2bDqKA", Duration = 11, CourseID = 4 },
            new Lesson { LessonID = 66, LessonName = "[ Bài 6 ] : Checkbox" , LinkVideo = "https://www.youtube.com/embed/7mTR8t9qFKI", Duration = 9, CourseID = 4 },
            new Lesson { LessonID = 67, LessonName = "[ Bài 7 ] : essagebox" , LinkVideo = "https://www.youtube.com/embed/2cfnkRI2E8Y", Duration = 9, CourseID = 4 },
            new Lesson { LessonID = 68, LessonName = "[ Bài 8 ] : Panel và FlowLayoutPanel" , LinkVideo = "http://youtube.com/embed/Cljvl3ur1wg", Duration = 11, CourseID = 4 },
            new Lesson { LessonID = 69, LessonName = "[ Bài 9 ] : RadioButton" , LinkVideo = "https://www.youtube.com/embed/dnY3DchYdwI", Duration = 16, CourseID = 4 },
            new Lesson { LessonID = 70, LessonName = "[ Bài 10 ] : Combobox" , LinkVideo = "https://www.youtube.com/embed/Uw3vymbSPO0", Duration = 35, CourseID = 4 },
            new Lesson { LessonID = 71, LessonName = "[ Bài 11 ] : Picturebox" , LinkVideo = "https://www.youtube.com/embed/57QobqDr8HM", Duration = 16, CourseID = 4 },
            new Lesson { LessonID = 72, LessonName = "[ Bài 12 ] : ListView" , LinkVideo = "https://www.youtube.com/embed/OLZW6SLYp1Q", Duration = 28, CourseID = 4 },
            new Lesson { LessonID = 73, LessonName = "[ Bài 13 ] : TreeView" , LinkVideo = "https://www.youtube.com/embed/3OEBp3tTt9E", Duration = 25, CourseID = 4 },
            new Lesson { LessonID = 74, LessonName = "[ Bài 14 ] : MenuStrip" , LinkVideo = "https://www.youtube.com/embed/-7B1y9KgxyY", Duration = 11, CourseID = 4 },
            new Lesson { LessonID = 75, LessonName = "[ Bài 15 ] : ToolTip" , LinkVideo = "https://www.youtube.com/embed/zTS4hkYzW_U", Duration = 8, CourseID = 4 },
            new Lesson { LessonID = 76, LessonName = "[ Bài 16 ] : StatusBar" , LinkVideo = "https://www.youtube.com/embed/UGMroKrSb-A", Duration = 14, CourseID = 4 },
            new Lesson { LessonID = 77, LessonName = "[ Bài 17 ] : ContextMenu" , LinkVideo = "https://www.youtube.com/embed/auswGkH9Q-I", Duration = 17, CourseID = 4 },
            new Lesson { LessonID = 78, LessonName = "[ Bài 18 ] : NotifyIcon" , LinkVideo = "https://www.youtube.com/embed/9KIHUn9i--w", Duration = 6, CourseID = 4 },
            new Lesson { LessonID = 79, LessonName = "[ Bài 19 ] : Thread trong Winform" , LinkVideo = "https://www.youtube.com/embed/IJE-eYo983M", Duration = 28, CourseID = 4 },
            new Lesson { LessonID = 80, LessonName = "[ Bài 20 ] : Timer" , LinkVideo = "http://youtube.com/embed/NzCE2QComvc", Duration = 11, CourseID = 4 },
            new Lesson { LessonID = 81, LessonName = "[ Bài 21 ] : ProcessBar" , LinkVideo = "https://www.youtube.com/embed/G9t5PLcCydk", Duration = 8, CourseID = 4 },
            new Lesson { LessonID = 82, LessonName = "[ Bài 22 ] : NumericUpDown" , LinkVideo = "https://www.youtube.com/embed/gNrWJB8YQ1c", Duration = 7, CourseID = 4 },
            new Lesson { LessonID = 83, LessonName = "[ Bài 23 ] : Process" , LinkVideo = "https://www.youtube.com/embed/epec4etkaW4", Duration = 22, CourseID = 4 },
            new Lesson { LessonID = 84, LessonName = "[ Bài 24 ] : DateTimePicker" , LinkVideo = "https://www.youtube.com/embed/xDlfghOEMQA", Duration = 11, CourseID = 4 },
            new Lesson { LessonID = 85, LessonName = "[ Bài 25 ] : LinQ" , LinkVideo = "https://www.youtube.com/embed/3YFM0rPsc7o", Duration = 24, CourseID = 4 },
            new Lesson { LessonID = 86, LessonName = "[ Bài 26 ] : EntityFrameWork" , LinkVideo = "https://www.youtube.com/embed/9-hAhYMWj9I", Duration = 55, CourseID = 4 },
            // JAVA
            new Lesson { LessonID = 87, LessonName = "[ Bài 1 ] :Giới thiệu Java" , LinkVideo = "https://www.youtube.com/embed/3gtOAlcovoQ", Duration = 9, CourseID = 5 },
            new Lesson { LessonID = 88, LessonName = "[ Bài 2 ] : Cài đặt môi trường Java" , LinkVideo = "https://www.youtube.com/embed/KjMRn1YQcLc", Duration = 14, CourseID = 5 },
            new Lesson { LessonID = 89, LessonName = "[ Bài 3 ] : Chương trình Java đầu tiên" , LinkVideo = "https://www.youtube.com/embed/jIQmebw9VaA", Duration = 10, CourseID = 5 },
            new Lesson { LessonID = 90, LessonName = "[ Bài 4 ] : Biến trong Java" , LinkVideo = "https://www.youtube.com/embed/G2mCSTtBojM", Duration = 15, CourseID = 5 },
            new Lesson { LessonID = 91, LessonName = "[ Bài 5 ] : Kiểu dữ liệu trong Java" , LinkVideo = "http://youtube.com/embed/4k_5vWY2wps", Duration = 9, CourseID = 5 },
            new Lesson { LessonID = 92, LessonName = "[ Bài 6 ] : Toán tử trong Java" , LinkVideo = "https://www.youtube.com/embed/H9FmP010A_Q", Duration = 11, CourseID = 5 },
            new Lesson { LessonID = 93, LessonName = "[ Bài 7 ] : Hằng trong Java" , LinkVideo = "https://www.youtube.com/embed/dqybUkGCaVw", Duration = 8, CourseID = 5 },
            new Lesson { LessonID = 94, LessonName = "[ Bài 8 ] : Ép kiểu trong Java" , LinkVideo = "https://www.youtube.com/embed/kOMiIKLCK34", Duration = 11, CourseID = 5 },
            new Lesson { LessonID = 95, LessonName = "[ Bài 9 ] : Cấu trúc rẽ nhánh trong Java" , LinkVideo = "https://www.youtube.com/embed/vradAZcby8I", Duration = 13, CourseID = 5 },
            new Lesson { LessonID = 96, LessonName = "[ Bài 10 ] : Vòng lặp While trong Java" , LinkVideo = "https://www.youtube.com/embed/tDfQ33fmmvs", Duration = 9, CourseID = 5 },
            new Lesson { LessonID = 97, LessonName = "[ Bài 11 ] : Vòng lặp For trong Java" , LinkVideo = "https://www.youtube.com/embed/1QVfZFOt7uI", Duration = 10, CourseID = 5 },
            new Lesson { LessonID = 98, LessonName = "[ Bài 12 ] : Mảng trong Java" , LinkVideo = "https://www.youtube.com/embed/0LX_B3-0XuU", Duration = 14, CourseID = 5 },
            new Lesson { LessonID = 99, LessonName = "[ Bài 13 ] : Foreach trong Java" , LinkVideo = "https://www.youtube.com/embed/SVnPYiHS68U", Duration = 6, CourseID = 5 },
            new Lesson { LessonID = 100, LessonName = "[ Bài 14 ] : Break và Continue trong Java" , LinkVideo = "https://www.youtube.com/embed/DrFwmhHqiA8", Duration = 6, CourseID = 5 },
            new Lesson { LessonID = 101, LessonName = "[ Bài 15 ] : Switch trong Java" , LinkVideo = "https://www.youtube.com/embed/P90f0KWQtv0", Duration = 6, CourseID = 5 },
            new Lesson { LessonID = 102, LessonName = "[ Bài 16 ] : OOP trong Java" , LinkVideo = "https://www.youtube.com/embed/8vOnoUZNtCA", Duration = 24, CourseID = 5 },
            new Lesson { LessonID = 103, LessonName = "[ Bài 17 ] : Class trong Java" , LinkVideo = "http://youtube.com/embed/j_zeaTrH0cU", Duration = 18, CourseID = 5 },
            new Lesson { LessonID = 104, LessonName = "[ Bài 18 ] : Phạm vi truy cập trong Java" , LinkVideo = "https://www.youtube.com/embed/s6-UDkzggDk", Duration = 12, CourseID = 5 },
            new Lesson { LessonID = 105, LessonName = "[ Bài 19 ] : Static trong Java" , LinkVideo = "https://www.youtube.com/embed/I3tlj977x08", Duration = 12, CourseID = 5 },
            new Lesson { LessonID = 106, LessonName = "[ Bài 20 ] : This trong Java" , LinkVideo = "https://www.youtube.com/embed/gIWwValOk0w", Duration = 9, CourseID = 5 },
            new Lesson { LessonID = 107, LessonName = "[ Bài 21 ] : Kế thừa trong Java" , LinkVideo = "https://www.youtube.com/embed/FscxYO3s6Go", Duration = 10, CourseID = 5 },
            new Lesson { LessonID = 108, LessonName = "[ Bài 22 ] : Setter và Getter trong Java " , LinkVideo = "https://www.youtube.com/embed/POoxuSKIP4I", Duration = 10, CourseID = 5 },
            new Lesson { LessonID = 109, LessonName = "[ Bài 23 ] : Overriding và Overloading trong Java" , LinkVideo = "https://www.youtube.com/embed/HIz83AG7lYE", Duration = 7, CourseID = 5 },
            new Lesson { LessonID = 110, LessonName = "[ Bài 24 ] : Trừu tượng trong Java" , LinkVideo = "https://www.youtube.com/embed/9jlUoO3e2GY", Duration = 17, CourseID = 5 },
            new Lesson { LessonID = 111, LessonName = "[ Bài 25 ] : Interface Java" , LinkVideo = "https://www.youtube.com/embed/BgNcBfqOLQk", Duration = 9, CourseID = 5 },
            new Lesson { LessonID = 112, LessonName = "[ Bài 26 ] : Giải thích hàm main trong Java" , LinkVideo = "https://www.youtube.com/embed/nzurRJhbFl8", Duration = 7, CourseID = 5 },
            new Lesson { LessonID = 113, LessonName = "[ Bài 27 ] : Try catch trong Java" , LinkVideo = "https://www.youtube.com/embed/zI9uen-vRa4", Duration = 12, CourseID = 5 },
            new Lesson { LessonID = 114, LessonName = "[ Bài 28 ] : 4 tính chất của OOP" , LinkVideo = "https://www.youtube.com/embed/u-yPTK1pHQM", Duration = 20, CourseID = 5 },
            // JAVA SWING
            new Lesson { LessonID = 115, LessonName = "[ Bài 1 ] :Tạo frame bằng kéo thả với Netbeans." , LinkVideo = "https://www.youtube.com/embed/Mkgo_9ZgWJs", Duration = 6, CourseID = 6 },
            new Lesson { LessonID = 116, LessonName = "[ Bài 2 ] :Button và xử lí sự kiện đơn giản cho button" , LinkVideo = "https://www.youtube.com/embed/GrYN299AytE", Duration = 9, CourseID = 6 },
            new Lesson { LessonID = 117, LessonName = "[ Bài 3 ] :Xử lí sự kiện đơn giản cho button (code tay)" , LinkVideo = "https://www.youtube.com/embed/ox-tsrRXGec", Duration = 10, CourseID = 6 },
            new Lesson { LessonID = 118, LessonName = "[ Bài 4 ] :Thiết lập phím tắt và chú thích chức năng cho Button." , LinkVideo = "https://www.youtube.com/embed/YaG4L712CwM", Duration = 6, CourseID = 6 },
            new Lesson { LessonID = 119, LessonName = "[ Bài 5 ] :Giới thiệu Bảng và thao tác với bảng: thêm và xóa hàng" , LinkVideo = "https://www.youtube.com/embed/UT_qIE0bJ7c", Duration = 5, CourseID = 6 },
            new Lesson { LessonID = 120, LessonName = "[ Bài 6 ] :Cách sử dụng label-nhãn" , LinkVideo = "https://www.youtube.com/embed/6FbhKQWkFZE", Duration = 12, CourseID = 6 },
            new Lesson { LessonID = 121, LessonName = "[ Bài 7 ] :Cách sử dụng Text Field" , LinkVideo = "https://www.youtube.com/embed/4UkDu38oW8U", Duration = 9, CourseID = 6 },
            new Lesson { LessonID = 122, LessonName = "[ Bài 8 ] :Tạo bảng và chèn dữ liệu vào bảng" , LinkVideo = "https://www.youtube.com/embed/RhF2ivqCw94", Duration = 14, CourseID = 6 },
            new Lesson { LessonID = 123, LessonName = "[ Bài 9 ] :Tạo tên các cột cho bảng có thể dùng chung" , LinkVideo = "https://www.youtube.com/embed/2s8Kh3RZYuQ", Duration = 8, CourseID = 6 },
            new Lesson { LessonID = 124, LessonName = "[ Bài 10 ] :Chèn dữ liệu vào bảng và đọc dữ liệu từ bảng CSDL" , LinkVideo = "https://www.youtube.com/embed/Zox6MMNEmJw", Duration = 35, CourseID = 6 },
            new Lesson { LessonID = 125, LessonName = "[ Bài 11 ] :Hướng dẫn đọc ghi thông tin vào file text" , LinkVideo = "https://www.youtube.com/embed/7FLXaEi__9U", Duration = 13, CourseID = 6 },
            new Lesson { LessonID = 126, LessonName = "[ Bài 12 ] :Giới thiệu ứng dụng quản lý bạn đọc thư viện." , LinkVideo = "https://www.youtube.com/embed/EKo6qMyz02M", Duration = 4, CourseID = 6 },
            new Lesson { LessonID = 127, LessonName = "[ Bài 13 ] :Vẽ sơ đồ tuần tự chức năng thêm mới bạn đọc" , LinkVideo = "https://www.youtube.com/embed/B9wucDnekvA", Duration = 15, CourseID = 6 },
            new Lesson { LessonID = 128, LessonName = "[ Bài 14 ] :Cài đặt chức năng thêm mới bạn đọc " , LinkVideo = "https://www.youtube.com/embed/M63VZ2c0mVQ", Duration = 9, CourseID = 6 },
            new Lesson { LessonID = 129, LessonName = "[ Bài 15 ] :Cài đặt chức năng cho frame nhập thông tin bạn đọc" , LinkVideo = "https://www.youtube.com/embed/MmvaO45rkD4", Duration = 22, CourseID = 6 },
            new Lesson { LessonID = 130, LessonName = "[ Bài 16 ] :Kết nối và cập nhật cơ sở dữ liệu." , LinkVideo = "https://www.youtube.com/embed/LnSqeDXuPOY", Duration = 24, CourseID = 6 },
            new Lesson { LessonID = 131, LessonName = "[ Bài 17 ] :Thêm Dữ Liệu Vào Bảng từ JDialog Form - P1" , LinkVideo = "https://www.youtube.com/embed/uwZTOQFqJbw", Duration = 24, CourseID = 6 },
            new Lesson { LessonID = 132, LessonName = "[ Bài 18 ] :Thêm Dữ Liệu Vào Bảng từ JDialog Form - P2" , LinkVideo = "https://www.youtube.com/embed/qmld4g2xihQ", Duration = 18, CourseID = 6 },
            new Lesson { LessonID = 133, LessonName = "[ Bài 19 ] :Thêm Dữ Liệu Vào Bảng từ JDialog Form - P3" , LinkVideo = "https://www.youtube.com/embed/BNb0NwV0Bes", Duration = 16, CourseID = 6 },
            new Lesson { LessonID = 134, LessonName = "[ Bài 20 ] :Sửa dữ liệu từ bảng với JDialog Form - P1" , LinkVideo = "https://www.youtube.com/embed/EdhtzM57M_E", Duration = 27, CourseID = 6 },
            new Lesson { LessonID = 135, LessonName = "[ Bài 21 ] :Sửa dữ liệu từ bảng với JDialog Form - P2" , LinkVideo = "https://www.youtube.com/embed/yI67t0-5Lfs", Duration = 19, CourseID = 6 },
            new Lesson { LessonID = 136, LessonName = "[ Bài 22 ] :Xóa dữ liệu khỏi bảng qua Default Table Model" , LinkVideo = "https://www.youtube.com/embed/vWOYWYrv6-4", Duration = 13, CourseID = 6 },
            new Lesson { LessonID = 137, LessonName = "[ Bài 23 ] :Thêm các tab mới vào cho ứng dụng" , LinkVideo = "https://www.youtube.com/embed/jv2M5vmusmw", Duration = 11, CourseID = 6 },
            new Lesson { LessonID = 138, LessonName = "[ Bài 24 ] :Thêm và căn chỉnh hàng cột cho bảng" , LinkVideo = "https://www.youtube.com/embed/VwHJkjvgrOY", Duration = 5, CourseID = 6 },
            new Lesson { LessonID = 139, LessonName = "[ Bài 25 ] :Thêm/sửa các nhãn tùy chọn trong Combo box" , LinkVideo = "https://www.youtube.com/embed/dgBrZCwoR8w", Duration = 6, CourseID = 6 },
            new Lesson { LessonID = 140, LessonName = "[ Bài 26 ] :Lấy dữ liệu đã chọn từ Combo box" , LinkVideo = "https://www.youtube.com/embed/C25n5Do0PvI", Duration = 5, CourseID = 6 },
            new Lesson { LessonID = 141, LessonName = "[ Bài 27 ] : Đẩy dữ liệu vào combo box" , LinkVideo = "https://www.youtube.com/embed/qaX1i1rFajU", Duration = 6, CourseID = 6 },
            new Lesson { LessonID = 142, LessonName = "[ Bài 28 ] :Xử lý ngoại lệ khi nhập sai/để trống thông tin bắt buộc" , LinkVideo = "https://www.youtube.com/embed/CaLEJEtnBN0", Duration = 9, CourseID = 6 },
            // PYTHON 
            new Lesson { LessonID = 143, LessonName = "[ Bài 1 ] :Giới thiệu ngôn ngữ lập trình Python" , LinkVideo = "https://www.youtube.com/embed/NZj6LI5a9vc", Duration = 12, CourseID = 7 },
            new Lesson { LessonID = 144, LessonName = "[ Bài 2 ] :Cài đặt môi trường Python" , LinkVideo = "https://www.youtube.com/embed/jf-q_dG8WzI", Duration = 6, CourseID = 7 },
            new Lesson { LessonID = 145, LessonName = "[ Bài 3 ] : Chạy file Python" , LinkVideo = "https://www.youtube.com/embed/QFxqY8qv42E", Duration = 13, CourseID = 7 },
            new Lesson { LessonID = 146, LessonName = "[ Bài 4 ] :Comment trong Python" , LinkVideo = "http://youtube.com/embed/t3dERE9T5yg", Duration = 10, CourseID = 7 },
            new Lesson { LessonID = 147, LessonName = "[ Bài 5 ] :Biến(Variable) trong Python" , LinkVideo = "https://www.youtube.com/embed/nclE18Yl-kA", Duration = 16, CourseID = 7 },
            new Lesson { LessonID = 148, LessonName = "[ Bài 6 ] :Kiểu số trong Python" , LinkVideo = "https://www.youtube.com/embed/IAVvgqDBiv0", Duration = 38, CourseID = 7 },
            new Lesson { LessonID = 149, LessonName = "[ Bài 7 ] :Kiểu chuỗi trong Python - Phần 1" , LinkVideo = "https://www.youtube.com/embed/Vb6XWSLPQfg", Duration = 26, CourseID = 7 },
            new Lesson { LessonID = 150, LessonName = "[ Bài 8 ] :Kiểu chuỗi trong Python - Phần 2" , LinkVideo = "https://www.youtube.com/embed/gzWriEOVjU0", Duration = 29, CourseID = 7 },
            new Lesson { LessonID = 151, LessonName = "[ Bài 9 ] : Kiểu chuỗi trong Python p3" , LinkVideo = "https://www.youtube.com/embed/LRUHnuHljPQ", Duration = 40, CourseID = 7 },
            new Lesson { LessonID = 152, LessonName = "[ Bài 10 ] : Kiểu chuỗi trong Python - Phần 4" , LinkVideo = "https://www.youtube.com/embed/q2TNJMBx6GE", Duration = 29, CourseID = 7 },
            new Lesson { LessonID = 153, LessonName = "[ Bài 11 ] : Kiểu chuỗi trong Python - Phần 5" , LinkVideo = "https://www.youtube.com/embed/u2Kd3weqPZE", Duration = 16, CourseID = 7 },
            new Lesson { LessonID = 154, LessonName = "[ Bài 12 ] : List trong Python - Phần 1" , LinkVideo = "https://www.youtube.com/embed/UzTE665WXb8", Duration = 43, CourseID = 7 },
            new Lesson { LessonID = 155, LessonName = "[ Bài 13 ] :List trong Python - Phần 2" , LinkVideo = "https://www.youtube.com/embed/9IH3EynbcpU", Duration = 18, CourseID = 7 },
            new Lesson { LessonID = 156, LessonName = "[ Bài 14 ] :Tuple trong Python" , LinkVideo = "https://www.youtube.com/embed/dDFFCbRGm3o", Duration = 20, CourseID = 7 },
            new Lesson { LessonID = 157, LessonName = "[ Bài 15 ] :Hashable và Unhashable trong Python" , LinkVideo = "https://www.youtube.com/embed/gw9zbl2Q7r4", Duration = 24, CourseID = 7 },
            new Lesson { LessonID = 158, LessonName = "[ Bài 16 ] : Set trong Python" , LinkVideo = "https://www.youtube.com/embed/S-CWHkKiOBs", Duration = 31, CourseID = 7 },
            new Lesson { LessonID = 159, LessonName = "[ Bài 17 ] :Dict trong Python - Phần 1" , LinkVideo = "https://www.youtube.com/embed/zFDTmZjJFws", Duration = 22, CourseID = 7 },
            new Lesson { LessonID = 160, LessonName = "[ Bài 18 ] :Dict trong Python - Phần 2" , LinkVideo = "https://www.youtube.com/embed/jmwBKuJl2Zg", Duration = 18, CourseID = 7 },
            new Lesson { LessonID = 161, LessonName = "[ Bài 19 ] : Xử lý File trong Python" , LinkVideo = "https://www.youtube.com/embed/6J8-jkoRBXw", Duration = 24, CourseID = 7 },
            new Lesson { LessonID = 162, LessonName = "[ Bài 20 ] :Iteration trong Python" , LinkVideo = "https://www.youtube.com/embed/GSUwh958k_A", Duration = 21, CourseID = 7 },
            new Lesson { LessonID = 163, LessonName = "[ Bài 21 ] :Hàm xuất trong Python" , LinkVideo = "http://youtube.com/embed/rhOyCSIf1is", Duration = 22, CourseID = 7 },
            new Lesson { LessonID = 164, LessonName = "[ Bài 22 ] : Hàm nhập trong Python" , LinkVideo = "https://www.youtube.com/embed/rK4MphZVhDM", Duration = 17, CourseID = 7 },
            new Lesson { LessonID = 165, LessonName = "[ Bài 23 ] :Kiểu Boolean trong python" , LinkVideo = "https://www.youtube.com/embed/iB9EhSZvfFk", Duration = 26, CourseID = 7 },
            new Lesson { LessonID = 166, LessonName = "[ Bài 24 ] :Cấu trúc rẽ nhánh trong Python" , LinkVideo = "https://www.youtube.com/embed/4_Jb1xZsDJ8", Duration = 23, CourseID = 7 },
            new Lesson { LessonID = 167, LessonName = "[ Bài 25 ] :While Loop trong Python" , LinkVideo = "https://www.youtube.com/embed/wq7Th3nXyCQ", Duration = 18, CourseID = 7 },
            new Lesson { LessonID = 168, LessonName = "[ Bài 26 ] : For Loop trong Python" , LinkVideo = "https://www.youtube.com/embed/9TxJ71NNO64", Duration = 38, CourseID = 7 },
            new Lesson { LessonID = 169, LessonName = "[ Bài 27 ] : Function trong python" , LinkVideo = "https://www.youtube.com/embed/a6FnNvt3Fw4", Duration = 23, CourseID = 7 },
            new Lesson { LessonID = 170, LessonName = "[ Bài 28 ] :Function trong python - Positional" , LinkVideo = "https://www.youtube.com/embed/M77p3PB-qzM", Duration = 11, CourseID = 7 },
            new Lesson { LessonID = 171, LessonName = "[ Bài 29 ] : Function trong Python - Packing và unpacking" , LinkVideo = "https://www.youtube.com/embed/0Gf5MVTWuCY", Duration = 14, CourseID = 7 },
            new Lesson { LessonID = 172, LessonName = "[ Bài 30 ] :Function trong Python - Locals và globals" , LinkVideo = "https://www.youtube.com/embed/w7qnt6iIakM", Duration = 13, CourseID = 7 },
            new Lesson { LessonID = 173, LessonName = "[ Bài 31 ] :Function trong Python - Return" , LinkVideo = "https://www.youtube.com/embed/3bdMH8z50zE", Duration = 7, CourseID = 7 },
            new Lesson { LessonID = 174, LessonName = "[ Bài 32 ] :Function trong Python - Yield" , LinkVideo = "http://youtube.com/embed/aChGfj5h3UQ", Duration = 14, CourseID = 7 },
            new Lesson { LessonID = 175, LessonName = "[ Bài 33 ] :Function trong Python - Lambda" , LinkVideo = "https://www.youtube.com/embed/7YTL1u5Ja5A", Duration = 15, CourseID = 7 },
            new Lesson { LessonID = 176, LessonName = "[ Bài 34 ] :Function trong Python - Functional tools" , LinkVideo = "https://www.youtube.com/embed/W5Xvw_2WPeg", Duration = 12, CourseID = 7 },
            new Lesson { LessonID = 177, LessonName = "[ Bài 35 ] :Function trong Python - Đệ Quy" , LinkVideo = "https://www.youtube.com/embed/AFNMgGmWcdQ", Duration = 10,CourseID = 7 },
            // PYTHON MACHINE LEARNING
            new Lesson { LessonID = 178, LessonName = "[ Bài 1 ] :Giới thiệu Machine learning" , LinkVideo = "https://www.youtube.com/embed/_ZjIv2D6T40", Duration = 20, CourseID = 8 },
            new Lesson { LessonID = 179, LessonName = "[ Bài 2 ] :Ma trận và vector với NumPy" , LinkVideo = "https://www.youtube.com/embed/oykpEGJrf20", Duration = 35, CourseID = 8 },
            new Lesson { LessonID = 180, LessonName = "[ Bài 3 ] :Giới thiệu Linear Regression và hàm hθ(x)" , LinkVideo = "https://www.youtube.com/embed/_KfLLePcgCs", Duration = 46, CourseID = 8 },
            new Lesson { LessonID = 181, LessonName = "[ Bài 4 ] :Hàm J(θ) cho Linear Regression" , LinkVideo = "https://www.youtube.com/embed/sLC88fIG9Q8", Duration = 16, CourseID = 8 },
            new Lesson { LessonID = 182, LessonName = "[ Bài 5 ] :Gradient Descent cho Linear Regression" , LinkVideo = "https://www.youtube.com/embed/VwIjFJ-5Zbk", Duration = 24, CourseID = 8 },
            new Lesson { LessonID = 183, LessonName = "[ Bài 6 ] :Feature Nomalize" , LinkVideo = "https://www.youtube.com/embed/qPWbs7t4FVI", Duration = 17, CourseID = 8 },
            new Lesson { LessonID = 184, LessonName = "[ Bài 7 ] :Normal Equation cho Linear Regression" , LinkVideo = "https://www.youtube.com/embed/eDdxtTcLBRM", Duration = 9, CourseID = 8 },
            new Lesson { LessonID = 185, LessonName = "[ Bài 8 ] :Hồi quy tuyến tính Linear Regression trong Học máy Machine Learning" , LinkVideo = "https://www.youtube.com/embed/aeDzuGQ6DDk", Duration = 75, CourseID = 8 },
            new Lesson { LessonID = 186, LessonName = "[ Bài 9 ] :Logistic Regression phân loại chữ số viết tay | Phân tích toán học" , LinkVideo = "https://www.youtube.com/embed/wNUJM4vQ1z8", Duration = 65, CourseID = 8 },
            new Lesson { LessonID = 187, LessonName = "[ Bài 10 ] :Logistic Regression phân loại chữ số viết tay | Hướng dẫn lập trình" , LinkVideo = "https://www.youtube.com/embed/n0k26uJR09U", Duration = 94, CourseID = 8 },
            new Lesson { LessonID = 188, LessonName = "[ Bài 11 ] :Deep Learning - Tổng quan và giải thích về mạng học sâu| Mạng nơron tích chập| CNN dễ hiểu nhất" , LinkVideo = "https://www.youtube.com/embed/NL6eCtMjikQ", Duration = 36, CourseID = 8 },
            new Lesson { LessonID = 189, LessonName = "[ Bài 12 ] :Machine Learning - Ứng dụng web python flask nhận dạng chữ số viết tay | Phần 1" , LinkVideo = "https://www.youtube.com/embed/joIGaPC2G-w", Duration = 77, CourseID = 8 },
            new Lesson { LessonID = 190, LessonName = "[ Bài 13 ] :Machine Learning - Ứng dụng web python flask nhận dạng chữ số viết tay | Phần 2" , LinkVideo = "https://www.youtube.com/embed/glTMMbnzHXA", Duration = 35, CourseID = 8 }
            );

    }

        public DbSet<HTNL.Edu.Models.User> User { get; set; } = default!;
        public DbSet<HTNL.Edu.Models.Category> Categories { get; set; } = default!;
        public DbSet<HTNL.Edu.Models.Course> Courses { get; set; } = default!;
}
