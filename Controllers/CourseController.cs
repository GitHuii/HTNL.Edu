using HTNL.Edu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HTNL.Edu.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId, string searchString)
        {
            var courses = from c in _context.Courses.Include(c => c.Category)
                          select c;

            if (categoryId != null && categoryId != 0)
            {
                courses = courses.Where(c => c.CategoryID == categoryId);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.CourseName.Contains(searchString)
                                          || c.Description.Contains(searchString));
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CurrentCategory = categoryId;
            ViewBag.SearchString = searchString;

            return View(await courses.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            // Kiểm tra xem user đã đăng ký khóa học này chưa
            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    var isEnrolled = await _context.CourseDetails
                        .AnyAsync(cd => cd.UserID == userId && cd.CourseID == id);

                    ViewBag.IsEnrolled = isEnrolled;
                }
            }

            return View(course);
        }

        // POST: Course/Enroll
        [HttpPost]
        [Authorize] // Yêu cầu đăng nhập
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(int courseId)
        {
            // Lấy UserID từ Claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Details", new { id = courseId }) });
            }

            // Kiểm tra khóa học có tồn tại không
            var course = await _context.Courses
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(c => c.CourseID == courseId);

            if (course == null)
            {
                TempData["ErrorMessage"] = "Khóa học không tồn tại!";
                return RedirectToAction(nameof(Index));
            }

            // Kiểm tra đã đăng ký chưa
            var existingEnrollment = await _context.CourseDetails
                .FirstOrDefaultAsync(cd => cd.UserID == userId && cd.CourseID == courseId);

            if (existingEnrollment != null)
            {
                TempData["InfoMessage"] = "Bạn đã đăng ký khóa học này rồi!";
                return RedirectToAction("MyCourses");
            }

            // Tạo CourseDetail mới
            var courseDetail = new CourseDetail
            {
                UserID = userId,
                CourseID = courseId,
                LastTime = DateTime.Now
            };

            _context.CourseDetails.Add(courseDetail);
            await _context.SaveChangesAsync();

            // Tạo CourseDetailLesson cho tất cả các bài học
            if (course.Lessons != null && course.Lessons.Any())
            {
                foreach (var lesson in course.Lessons)
                {
                    var courseDetailLesson = new CourseDetailLesson
                    {
                        CourseDetailID = courseDetail.CourseDetailID,
                        LessonID = lesson.LessonID,
                        IsDone = false,
                        CompletedTime = null
                    };

                    _context.CourseDetailLessons.Add(courseDetailLesson);
                }

                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = $"Đăng ký khóa học '{course.CourseName}' thành công!";
            return RedirectToAction("MyCourses");
        }

        // GET: Course/MyCourses
        [Authorize]
        public async Task<IActionResult> MyCourses()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var myCourses = await _context.CourseDetails
                .Include(cd => cd.Course)
                    .ThenInclude(c => c.Category)
                .Include(cd => cd.Course)
                    .ThenInclude(c => c.Lessons)
                .Include(cd => cd.CourseDetailLessons)
                    .ThenInclude(cdl => cdl.Lesson)
                .Where(cd => cd.UserID == userId)
                .OrderByDescending(cd => cd.LastTime)
                .ToListAsync();

            return View(myCourses);
        }
    }
}