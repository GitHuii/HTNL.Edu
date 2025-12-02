using HTNL.Edu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HTNL.Edu.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Load 3 khóa học đầu tiên
            var courses = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.CourseDetails)
                .OrderByDescending(c => c.CourseID)
                .Take(3)
                .Select(c => new CourseCardViewModel
                {
                    CourseID = c.CourseID,
                    CourseName = c.CourseName,
                    Description = c.Description,
                    ImageUrl = c.CourseImage,
                    CategoryName = c.Category != null ? c.Category.CategoryName : "Chưa phân loại",
                    LessonCount = c.CourseDetails.Count,
                    EnrolledCount = _context.CourseDetails
                        .Count(cd => cd.CourseID == c.CourseID)
                })
                .ToListAsync();

            ViewBag.InitialCourses = courses;
            ViewBag.TotalCourses = await _context.Courses.CountAsync();

            return View();
        }

        // GET: Home/LoadMoreCourses
        [HttpGet]
        public async Task<IActionResult> LoadMoreCourses(int skip = 0, int take = 3)
        {
            var courses = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.CourseDetails)
                .OrderByDescending(c => c.CourseID)
                .Skip(skip)
                .Take(take)
                .Select(c => new CourseCardViewModel
                {
                    CourseID = c.CourseID,
                    CourseName = c.CourseName,
                    Description = c.Description,
                    ImageUrl = c.CourseImage,
                    CategoryName = c.Category != null ? c.Category.CategoryName : "Chưa phân loại",
                    LessonCount = c.CourseDetails.Count,
                    EnrolledCount = _context.CourseDetails
                        .Count(cd => cd.CourseID == c.CourseID)
                })
                .ToListAsync();

            var totalCourses = await _context.Courses.CountAsync();
            var hasMore = (skip + take) < totalCourses;

            return Json(new
            {
                courses = courses,
                hasMore = hasMore,
                totalCourses = totalCourses,
                currentCount = skip + courses.Count
            });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // Optional: Handle contact form submission
        [HttpPost]
        public IActionResult Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Here you can:
                // 1. Send email
                // 2. Save to database
                // 3. Send notification

                TempData["SuccessMessage"] = "Cảm ơn bạn! Tin nhắn của bạn đã được gửi thành công.";
                return RedirectToAction(nameof(Contact));
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }

    // Optional: Contact Form Model
    public class ContactFormModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
