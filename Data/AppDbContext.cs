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
    }

        public DbSet<HTNL.Edu.Models.User> User { get; set; } = default!;
        public DbSet<HTNL.Edu.Models.Category> Category { get; set; } = default!;
}
