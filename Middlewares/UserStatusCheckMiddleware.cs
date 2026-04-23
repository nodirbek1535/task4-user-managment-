//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using System.Security.Claims;
using task4_user_managment_.Services.Foundations.Users;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Middlewares
{
    public class UserStatusCheckMiddleware
    {
        private readonly RequestDelegate next;

        public UserStatusCheckMiddleware(RequestDelegate next) =>
            this.next = next;

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            string path = context.Request.Path.Value?.ToLower() ?? "";
            if (path.Contains("api/auth/register") ||
                path.Contains("api/auth/login") ||
                path.Contains("api/auth/confirm-email"))
            {
                await next(context);
                return;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if(userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                try
                {
                    var user = await userService.RetrieveUserByAsync(userId);

                    if (user != null || user.Status == UserStatus.Blocked)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        await context.Response.WriteAsJsonAsync(new
                        {
                            Message = "Your account is deleted or blocked. Please contact support.",
                            redirect = "/login"
                        });
                        return;
                    }
                }
                catch
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                await next(context);
            }
        }
    }
}
