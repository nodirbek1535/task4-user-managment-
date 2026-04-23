//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using task4_user_managment_.Models.Requests.Auth;
using task4_user_managment_.Models.Responses;
using task4_user_managment_.Services.Orchestrations.Auth;

namespace task4_user_managment_.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : RESTFulController
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService) =>
            this.authService = authService;

        //register new user
        [HttpPost("register")]
        public async ValueTask<IActionResult> RegisterAsync(RegisterRequest request)
        {
            UserResponse response =
                await this.authService.RegisterAsync(request);

            return Ok(response);
        }

        [HttpGet("confirm-email")]
        public async ValueTask<ActionResult<string>> ConfirmEmailAsync([FromQuery] string token)
        {
            bool isConfirmed = await this.authService.ConfirmEmailAsync(token);

            if (isConfirmed)
            {
                return Ok("Emailingiz muvaffaqiyatli tasdiqlandi! Endi login qilishingiz mumkin.");
            }

            return BadRequest("Token yaroqsiz yoki muddati o'tgan.");
        }


        //login user
        [HttpPost("login")]
        public async ValueTask<IActionResult> LoginAsync(LoginRequest request)
        {
            AuthResponse response =
                await this.authService.LoginAsync(request);

            return Ok(response);
        }
    }
}
