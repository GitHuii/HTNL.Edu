using HTMLEdu.Filters;
using HTNL.Edu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HTNL.Edu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class LessonsController : Controller
    {
        private readonly AppDbContext _context;

        public LessonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Lessons/Create
        public IActionResult Create(int? courseId)
        {
            ViewBag.Courses = new SelectList(_context.Courses, "CourseID", "CourseName", courseId);

            var lesson = new Lesson();
            if (courseId.HasValue)
            {
                lesson.CourseID = courseId.Value;
            }

            return View(lesson);
        }

        // POST: Admin/Lessons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thêm bài học thành công!";
                return RedirectToAction("Details", "Courses", new { id = lesson.CourseID });
            }

            ViewBag.Courses = new SelectList(_context.Courses, "CourseID", "CourseName", lesson.CourseID);
            return View(lesson);
        }

        // GET: Admin/Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            ViewBag.Courses = new SelectList(_context.Courses, "CourseID", "CourseName", lesson.CourseID);
            return View(lesson);
        }

        // POST: Admin/Lessons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lesson lesson)
        {
            if (id != lesson.LessonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Cập nhật bài học thành công!";
                    return RedirectToAction("Details", "Courses", new { id = lesson.CourseID });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.LessonID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Courses = new SelectList(_context.Courses, "CourseID", "CourseName", lesson.CourseID);
            return View(lesson);
        }

        // GET: Admin/Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.LessonID == id);

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Admin/Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                int courseId = lesson.CourseID;

                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Xóa bài học thành công!";
                return RedirectToAction("Details", "Courses", new { id = courseId });
            }

            return NotFound();
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.LessonID == id);
        }
    }
}