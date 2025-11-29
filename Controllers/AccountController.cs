using HTMLEdu.Filters;
using HTNL.Edu.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HTMLEdu.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            // Nếu đã đăng nhập, chuyển về trang chủ
            if (User.Identity?.IsAuthenticated == true && User.HasClaim("Role", "User"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Tìm user với Role = "User"
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName
                                       && u.PassWord == model.PassWord
                                       && u.Role == "User");

            if (user != null)
            {
                // Tạo claims cho user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim("FullName", user.FullName ?? ""),
                    new Claim("Role", user.Role ?? "User"),
                    new Claim("Streak", user.Streak?.ToString() ?? "0")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "UserScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = model.RememberMe
                        ? DateTimeOffset.UtcNow.AddDays(30)
                        : DateTimeOffset.UtcNow.AddHours(8)
                };

                await HttpContext.SignInAsync("UserScheme", claimsPrincipal, authProperties);

                // Redirect
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
            return View(model);
        }

        // GET: Account/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra username đã tồn tại
            if (await _context.Users.AnyAsync(u => u.UserName == model.UserName))
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại");
                return View(model);
            }

            // Kiểm tra email đã tồn tại
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng");
                return View(model);
            }

            // Tạo user mới
            var user = new User
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email,
                PassWord = model.PassWord, // Nên hash password trong thực tế
                Role = "User",
                Streak = 0
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction(nameof(Login));
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("UserScheme");
            return RedirectToAction(nameof(Login));
        }

        // GET: Account/AccessDenied
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [UserAuthorize]
        public async Task<IActionResult> Profile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = await _context.Users
                .Include(u => u.CourseDetails)
                    .ThenInclude(cd => cd.Course)
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var viewModel = new UserProfileViewModel
            {
                UserID = user.UserID,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Streak = user.Streak ?? 0,
                TotalCourses = user.CourseDetails.Count,
                //CompletedCourses = user.CourseDetails.Count(cd => cd.CompletionPercentage >= 100),
                //InProgressCourses = user.CourseDetails.Count(cd => cd.CompletionPercentage < 100)
                CompletedCourses = 1,
                InProgressCourses = 1
            };

            return View(viewModel);
        }

        // GET: Account/Settings
        [UserAuthorize]
        public async Task<IActionResult> Settings()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var viewModel = new UserSettingsViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(viewModel);
        }

        // POST: Account/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public async Task<IActionResult> UpdateProfile(UserSettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Settings", model);
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Kiểm tra email đã tồn tại (trừ email của chính user)
            if (await _context.Users.AnyAsync(u => u.Email == model.Email && u.UserID != userId))
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng bởi tài khoản khác");
                return View("Settings", model);
            }

            // Update user info
            user.FullName = model.FullName;
            user.Email = model.Email;

            await _context.SaveChangesAsync();

            // Update claims
            await UpdateUserClaims(user);

            TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("Settings");
        }

        // POST: Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Vui lòng kiểm tra lại thông tin";
                return RedirectToAction("Settings");
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Verify old password
            if (user.PassWord != model.CurrentPassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu hiện tại không đúng";
                return RedirectToAction("Settings");
            }

            // Update password
            user.PassWord = model.NewPassword;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("Settings");
        }

        // Helper method to update claims after profile update
        private async Task UpdateUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim("FullName", user.FullName ?? ""),
                new Claim("Role", user.Role ?? "User"),
                new Claim("Streak", user.Streak?.ToString() ?? "0")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "UserScheme");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync("UserScheme", claimsPrincipal, authProperties);
        }
    }
}