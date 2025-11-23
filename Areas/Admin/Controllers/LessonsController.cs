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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LessonsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(Lesson lesson, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (imageFile != null && imageFile.Length > 0)
                {
                    lesson.LessonImage = "";
                    // Thêm bài học vào cơ sở dữ liệu (nhưng chưa có thông tin ảnh)
                    _context.Add(lesson);
                    await _context.SaveChangesAsync();  // Sau khi Save, LessonID sẽ có giá trị hợp lệ.

                    // Tạo đường dẫn đến thư mục chứa ảnh theo cấu trúc CourseID và LessonID
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "lesson_img", lesson.CourseID.ToString());

                    // Tạo tên file sử dụng LessonID
                    string fileName = lesson.LessonID.ToString() + ".jpg";  // Dùng LessonID làm tên file ảnh

                    // Đường dẫn hoàn chỉnh để lưu file
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // Tạo thư mục nếu chưa tồn tại
                    Directory.CreateDirectory(uploadsFolder);

                    // Lưu file vào thư mục đã tạo
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Cập nhật đường dẫn ảnh trong đối tượng lesson
                    lesson.LessonImage = $"/img/lesson_img/{lesson.CourseID}/{fileName}";

                    // Cập nhật lại thông tin bài học với đường dẫn ảnh mới
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();  // Lưu lại thông tin với ảnh đã được cập nhật.
                }

                TempData["SuccessMessage"] = "Thêm bài học thành công!";
                return RedirectToAction("Details", "Courses", new { id = lesson.CourseID });

            }
            foreach (var modelError in ModelState)
            {
                Console.WriteLine($"{modelError.Key}: {string.Join(", ", modelError.Value.Errors.Select(e => e.ErrorMessage))}");
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
        public async Task<IActionResult> Edit(int id, Lesson lesson, IFormFile? imageFile)
        {
            if (id != lesson.LessonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý upload ảnh mới
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Xóa ảnh cũ nếu có
                        if (!string.IsNullOrEmpty(lesson.LessonImage))
                        {
                            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, lesson.LessonImage.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/lesson_img");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        Directory.CreateDirectory(uploadsFolder);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        lesson.LessonImage = "/img/lesson_img/" + uniqueFileName;
                    }

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

                // Xóa ảnh nếu có
                if (!string.IsNullOrEmpty(lesson.LessonImage))
                {
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, lesson.LessonImage.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

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
