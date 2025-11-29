using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HTMLEdu.Filters
{
    /// <summary>
    /// Authorize cho User (wwwroot Controllers)
    /// </summary>
    public class UserAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public UserAuthorizeAttribute()
        {
            AuthenticationSchemes = "UserScheme";
            Policy = "UserPolicy";
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Kiểm tra đã đăng nhập chưa
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "", returnUrl = context.HttpContext.Request.Path });
                return;
            }

            // Kiểm tra role có phải User không
            if (!user.HasClaim("Role", "User"))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", new { area = "" });
                return;
            }
        }
    }

    /// <summary>
    /// Authorize cho Admin (Areas/Admin Controllers)
    /// </summary>
    public class AdminAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public AdminAuthorizeAttribute()
        {
            AuthenticationSchemes = "AdminScheme";
            Policy = "AdminPolicy";
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Kiểm tra đã đăng nhập chưa
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", new { area = "Admin", returnUrl = context.HttpContext.Request.Path });
                return;
            }

            // Kiểm tra role có phải Admin không
            if (!user.HasClaim("Role", "Admin"))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Auth", new { area = "Admin" });
                return;
            }
        }
    }
}