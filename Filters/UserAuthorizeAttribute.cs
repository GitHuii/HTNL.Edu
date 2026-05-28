using Microsoft.AspNetCore.Authorization;

namespace HTMLEdu.Filters
{
    /// <summary>
    /// Authorize cho User (wwwroot Controllers)
    /// </summary>
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public UserAuthorizeAttribute()
        {
            AuthenticationSchemes = "UserScheme";
            Policy = "UserPolicy";
        }
    }

    /// <summary>
    /// Authorize cho Admin (Areas/Admin Controllers).
    /// Framework sẽ tự sử dụng AdminScheme và kiểm tra Policy.
    /// Redirect khi chưa login hoặc access denied sẽ dựa theo LoginPath và AccessDeniedPath của AdminScheme.
    /// </summary>
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public AdminAuthorizeAttribute()
        {
            AuthenticationSchemes = "AdminScheme";
            Policy = "AdminPolicy";
        }
    }
}
