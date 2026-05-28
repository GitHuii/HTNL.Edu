using HTNL.Edu.Helpers;
using HTNL.Edu.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HTMLEdu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Auth/Login
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            var result = await HttpContext.AuthenticateAsync("AdminScheme");
            // Nếu đã đăng nhập với quyền Admin
            if (result.Succeeded && result.Principal.HasClaim("Role", "Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Admin/Auth/Login
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

            // Tìm admin theo username và role (không so sánh password trực tiếp ở DB)
            var admin = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName
                                       && u.Role == "Admin");

            // Xác minh mật khẩu bằng BCrypt
            if (admin != null && !PasswordHasher.Verify(model.PassWord, admin.PassWord ?? ""))
            {
                admin = null;
            }

            if (admin != null)
            {
                // Tạo claims cho admin
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.UserID.ToString()),
                    new Claim(ClaimTypes.Name, admin.UserName ?? ""),
                    new Claim(ClaimTypes.Email, admin.Email ?? ""),
                    new Claim("FullName", admin.FullName ?? ""),
                    new Claim("Role", admin.Role ?? "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = model.RememberMe
                        ? DateTimeOffset.UtcNow.AddDays(7)
                        : DateTimeOffset.UtcNow.AddHours(8)
                };

                await HttpContext.SignInAsync("AdminScheme", claimsPrincipal, authProperties);

                // Lưu session (optional, để tương thích với các code khác)
                HttpContext.Session.SetInt32("AdminID", admin.UserID);
                HttpContext.Session.SetString("AdminName", admin.FullName ?? "");
                HttpContext.Session.SetString("AdminRole", admin.Role ?? "Admin");

                // Redirect
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng hoặc bạn không có quyền Admin");
            return View(model);
        }

        // POST: Admin/Auth/Logout
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Xóa session
            HttpContext.Session.Clear();

            // Sign out
            await HttpContext.SignOutAsync("AdminScheme");

            return RedirectToAction(nameof(Login));
        }

        // GET: Admin/Auth/AccessDenied
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}