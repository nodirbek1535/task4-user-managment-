//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Models.Requests.Auth;
using task4_user_managment_.Models.Responses;
using task4_user_managment_.Services.Foundations.Security;
using task4_user_managment_.Services.Foundations.Users;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Orchestrations.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly IPasswordHashService passwordHashService;

        public AuthService(
            IUserService userService, 
            IPasswordHashService passwordHashService)
        {
            this.userService = userService;
            this.passwordHashService = passwordHashService;
        }

        public async ValueTask<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.Password
            };

            var createdUser = await this.userService.AddUserAsync(user);

            return new UserResponse
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email
            };
        }


        public ValueTask<AuthResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
