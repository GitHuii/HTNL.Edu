using HTMLEdu.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HTNL.Edu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Statistics
            ViewBag.TotalCourses = await _context.Courses.CountAsync();
            ViewBag.TotalUsers = await _context.Users.CountAsync();
            ViewBag.TotalCategories = await _context.Categories.CountAsync();

            // Latest courses
            ViewBag.LatestCourses = await _context.Courses
                .Include(c => c.Category)
                .OrderByDescending(c => c.CourseID)
                .Take(5)
                .ToListAsync();

            // Latest users
            ViewBag.LatestUsers = await _context.Users
                .OrderByDescending(u => u.Streak)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}
