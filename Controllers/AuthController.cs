//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using task4_user_managment_.Models.Exceptions;
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

        [HttpPost("register")]
        public async ValueTask<IActionResult> RegisterAsync(RegisterRequest request)
        {
            try
            {
                UserResponse response = await this.authService.RegisterAsync(request);
                return Ok(response);
            }
            catch (UserValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (UserDependencyValidationException dependencyValidationException)
                when (dependencyValidationException.InnerException is AlreadyExistsUserException)
            {
                return Conflict(dependencyValidationException.InnerException);
            }
            catch (UserDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (UserServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }

        [HttpGet("confirm-email")]
        public async ValueTask<IActionResult> ConfirmEmailAsync([FromQuery] string token)
        {
            try
            {
                bool isConfirmed = await this.authService.ConfirmEmailAsync(token);

                if (isConfirmed)
                    return Ok("Emailingiz muvaffaqiyatli tasdiqlandi! Endi login qilishingiz mumkin.");

                return BadRequest("Token yaroqsiz yoki muddati o'tgan.");
            }
            catch (UserValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (UserDependencyValidationException dependencyValidationException)
            {
                return BadRequest(dependencyValidationException.InnerException);
            }
            catch (UserServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }

        [HttpPost("login")]
        public async ValueTask<IActionResult> LoginAsync(LoginRequest request)
        {
            try
            {
                AuthResponse response = await this.authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UserValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (UserDependencyValidationException dependencyValidationException)
                when (dependencyValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(dependencyValidationException.InnerException);
            }
            catch (UserDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (UserServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
