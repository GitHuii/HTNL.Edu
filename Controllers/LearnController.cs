using HTNL.Edu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HTNL.Edu.Controllers
{
    [Authorize]
    public class LearnController : Controller
    {
        private readonly AppDbContext _context;

        public LearnController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Learn/Index?courseId=1
        public async Task<IActionResult> Index(int courseId, int? lessonId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra user đã đăng ký khóa học chưa
            var courseDetail = await _context.CourseDetails
                .Include(cd => cd.Course)
                    .ThenInclude(c => c.Category)
                .Include(cd => cd.Course)
                    .ThenInclude(c => c.Lessons)
                .Include(cd => cd.CourseDetailLessons)
                    .ThenInclude(cdl => cdl.Lesson)
                .FirstOrDefaultAsync(cd => cd.UserID == userId && cd.CourseID == courseId);

            if (courseDetail == null)
            {
                TempData["ErrorMessage"] = "Bạn chưa đăng ký khóa học này!";
                return RedirectToAction("Details", "Course", new { id = courseId });
            }

            // Lấy bài học hiện tại
            Lesson? currentLesson = null;
            CourseDetailLesson? currentProgress = null;

            if (lessonId.HasValue)
            {
                currentLesson = courseDetail.Course?.Lessons?
                    .FirstOrDefault(l => l.LessonID == lessonId.Value);
                currentProgress = courseDetail.CourseDetailLessons?
                    .FirstOrDefault(cdl => cdl.LessonID == lessonId.Value);
            }
            else
            {
                // Nếu không có lessonId, lấy bài học đầu tiên chưa hoàn thành
                var incompleteLessons = courseDetail.CourseDetailLessons?
                    .Where(cdl => !cdl.IsDone)
                    .OrderBy(cdl => cdl.Lesson.LessonID)
                    .ToList();

                if (incompleteLessons?.Any() == true)
                {
                    currentProgress = incompleteLessons.First();
                    currentLesson = currentProgress.Lesson;
                }
                else
                {
                    // Nếu đã hoàn thành hết, lấy bài đầu tiên
                    currentProgress = courseDetail.CourseDetailLessons?.FirstOrDefault();
                    currentLesson = currentProgress?.Lesson;
                }
            }

            // Cập nhật LastTime
            courseDetail.LastTime = DateTime.Now;
            await _context.SaveChangesAsync();

            var viewModel = new LearnViewModel
            {
                CourseDetail = courseDetail,
                CurrentLesson = currentLesson,
                CurrentProgress = currentProgress,
                AllLessons = courseDetail.Course?.Lessons?.OrderBy(l => l.LessonID).ToList() ?? new List<Lesson>(),
                LessonProgresses = courseDetail.CourseDetailLessons?.ToList() ?? new List<CourseDetailLesson>()
            };

            return View(viewModel);
        }

        // POST: Learn/CompleteLesson
        [HttpPost]
        public async Task<IActionResult> CompleteLesson(int courseId, int lessonId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập" });
            }

            var courseDetail = await _context.CourseDetails
                .Include(cd => cd.CourseDetailLessons)
                .FirstOrDefaultAsync(cd => cd.UserID == userId && cd.CourseID == courseId);

            if (courseDetail == null)
            {
                return Json(new { success = false, message = "Không tìm thấy khóa học" });
            }

            var lessonProgress = courseDetail.CourseDetailLessons?
                .FirstOrDefault(cdl => cdl.LessonID == lessonId);

            if (lessonProgress == null)
            {
                return Json(new { success = false, message = "Không tìm thấy bài học" });
            }

            // Đánh dấu hoàn thành
            if (!lessonProgress.IsDone)
            {
                // Lấy streak cũ
                var user = await _context.Users.FindAsync(userId);
                var oldStreak = user?.Streak ?? 0;

                lessonProgress.IsDone = true;
                lessonProgress.CompletedTime = DateTime.Now;

                // Cập nhật Streak
                var streakUpdated = await UpdateUserStreak(userId);

                await _context.SaveChangesAsync();

                // Lấy streak mới
                user = await _context.Users.FindAsync(userId);
                var newStreak = user?.Streak ?? 0;

                // Tính tiến độ
                var totalLessons = courseDetail.CourseDetailLessons?.Count ?? 0;
                var completedLessons = courseDetail.CourseDetailLessons?.Count(cdl => cdl.IsDone) ?? 0;
                var progressPercent = totalLessons > 0 ? (int)((completedLessons * 100.0) / totalLessons) : 0;

                return Json(new
                {
                    success = true,
                    message = "Hoàn thành bài học thành công!",
                    completedLessons = completedLessons,
                    totalLessons = totalLessons,
                    progressPercent = progressPercent,
                    streakUpdated = streakUpdated,
                    oldStreak = oldStreak,
                    newStreak = newStreak
                });
            }

            return Json(new { success = true, message = "Bài học đã được hoàn thành trước đó" });
        }


        // POST: Learn/UncompleteLesson
        [HttpPost]
        public async Task<IActionResult> UncompleteLesson(int courseId, int lessonId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập" });
            }

            var courseDetail = await _context.CourseDetails
                .Include(cd => cd.CourseDetailLessons)
                .FirstOrDefaultAsync(cd => cd.UserID == userId && cd.CourseID == courseId);

            if (courseDetail == null)
            {
                return Json(new { success = false, message = "Không tìm thấy khóa học" });
            }

            var lessonProgress = courseDetail.CourseDetailLessons?
                .FirstOrDefault(cdl => cdl.LessonID == lessonId);

            if (lessonProgress == null)
            {
                return Json(new { success = false, message = "Không tìm thấy bài học" });
            }

            if (lessonProgress.IsDone)
            {
                lessonProgress.IsDone = false;
                lessonProgress.CompletedTime = null;

                await _context.SaveChangesAsync();

                var totalLessons = courseDetail.CourseDetailLessons?.Count ?? 0;
                var completedLessons = courseDetail.CourseDetailLessons?.Count(cdl => cdl.IsDone) ?? 0;
                var progressPercent = totalLessons > 0 ? (int)((completedLessons * 100.0) / totalLessons) : 0;

                return Json(new
                {
                    success = true,
                    message = "Đã bỏ đánh dấu hoàn thành",
                    completedLessons = completedLessons,
                    totalLessons = totalLessons,
                    progressPercent = progressPercent
                });
            }

            return Json(new { success = true, message = "Bài học chưa được hoàn thành" });
        }


        private async Task<bool> UpdateUserStreak(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            var today = DateTime.Today;

            // Lấy tất cả các ngày đã học (có ít nhất 1 bài hoàn thành)
            var learnedDates = await _context.CourseDetailLessons
                .Include(cdl => cdl.CourseDetail)
                .Where(cdl => cdl.CourseDetail.UserID == userId
                           && cdl.IsDone
                           && cdl.CompletedTime.HasValue)
                .Select(cdl => cdl.CompletedTime!.Value.Date)
                .Distinct()
                .OrderByDescending(d => d)
                .ToListAsync();

            if (!learnedDates.Any())
            {
                user.Streak = 1;
                return true;
            }

            // Nếu hôm nay chưa có trong danh sách (bài học đầu tiên trong ngày)
            if (!learnedDates.Contains(today))
            {
                var lastLearnedDate = learnedDates.First();
                var daysDiff = (today - lastLearnedDate).Days;

                if (daysDiff == 1)
                {
                    // Học liên tiếp, tăng streak
                    user.Streak = (user.Streak ?? 0) + 1;
                    return true;
                }
                else if (daysDiff > 1)
                {
                    // Bỏ lỡ, reset streak
                    user.Streak = 1;
                    return true;
                }
            }

            return false;
        }
    }

    // ViewModel
    public class LearnViewModel
    {
        public CourseDetail CourseDetail { get; set; }
        public Lesson? CurrentLesson { get; set; }
        public CourseDetailLesson? CurrentProgress { get; set; }
        public List<Lesson> AllLessons { get; set; } = new List<Lesson>();
        public List<CourseDetailLesson> LessonProgresses { get; set; } = new List<CourseDetailLesson>();
    }
}