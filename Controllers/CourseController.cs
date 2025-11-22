using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            // Lấy tất cả khóa học kèm theo thông tin Category
            var courses = from c in _context.Courses.Include(c => c.Category)
                          select c;

            // Lọc theo category nếu có
            if (categoryId != null && categoryId != 0)
            {
                courses = courses.Where(c => c.CategoryID == categoryId);
            }

            // Tìm kiếm theo tên khóa học
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.CourseName.Contains(searchString)
                                          || c.Description.Contains(searchString));
            }

            // Lấy danh sách categories cho dropdown filter
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
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
    }
}
