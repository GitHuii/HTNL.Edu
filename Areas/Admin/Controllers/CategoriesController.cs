using HTNL.Edu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HTNL.Edu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index(string searchString)
        {
            var categories = from c in _context.Categories.Include(c => c.Courses)
                             select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c => c.CategoryName.Contains(searchString));
            }

            ViewBag.SearchString = searchString;
            return View(await categories.OrderBy(c => c.CategoryID).ToListAsync());
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Courses)
                .ThenInclude(c => c.Lessons)
                .FirstOrDefaultAsync(m => m.CategoryID == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                // Check if category name already exists
                if (await _context.Categories.AnyAsync(c => c.CategoryName == category.CategoryName))
                {
                    ModelState.AddModelError("CategoryName", "Tên danh mục đã tồn tại");
                    return View(category);
                }

                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm danh mục thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,CategoryName")] Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if category name already exists (excluding current category)
                    if (await _context.Categories.AnyAsync(c => c.CategoryName == category.CategoryName && c.CategoryID != id))
                    {
                        ModelState.AddModelError("CategoryName", "Tên danh mục đã tồn tại");
                        return View(category);
                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(m => m.CategoryID == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.CategoryID == id);

            if (category != null)
            {
                // Check if category has courses
                if (category.Courses != null && category.Courses.Any())
                {
                    TempData["ErrorMessage"] = $"Không thể xóa danh mục này vì có {category.Courses.Count} khóa học đang sử dụng!";
                    return RedirectToAction(nameof(Index));
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa danh mục thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
