//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Brokers.Emails;
using task4_user_managment_.Models.Requests.Auth;
using task4_user_managment_.Models.Responses;
using task4_user_managment_.Services.Foundations.Emails;
using task4_user_managment_.Services.Foundations.Security;
using task4_user_managment_.Services.Foundations.Users;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Orchestrations.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly IPasswordHashService passwordHashService;
        private readonly IEmailService emailService;
        private readonly ITokenService tokenService;

        public AuthService(
            IUserService userService, 
            IPasswordHashService passwordHashService,
            IEmailService emailService,
            ITokenService tokenService)
        {
            this.userService = userService;
            this.passwordHashService = passwordHashService;
            this.emailService = emailService;
            this.tokenService = tokenService;
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

            await this.emailService.SendVerificationEmailAsync(
                createdUser.Email, 
                createdUser.Name,
                createdUser.EmailConfirmationToken!);
            return new UserResponse
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email
            };
        }

        public async ValueTask<bool> ConfirmEmailAsync(string token)
        {
            // 1. Token orqali userni topamiz
            var user = await this.userService.RetrieveUserByTokenAsync(token);

            // 2. Token muddati o'tmaganini tekshiramiz
            if (user.TokenExpiresAt < DateTime.UtcNow)
                return false;

            // 3. Userni faollashtiramiz
            user.Status = UserStatus.Active;
            user.EmailConfirmationToken = null; // Tokenni bir martalik qilish uchun o'chiramiz

            await this.userService.ModifyUserAsync(user);

            return true;
        }


        public async ValueTask<AuthResponse> LoginAsync(LoginRequest request)
        {
            // 1. Userni emaili orqali qidiramiz
            var user = await this.userService.RetrieveUserByEmailAsync(request.Email);
            // 2. Parolni tekshiramiz
            bool isPasswordValid = this.passwordHashService.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid) throw new Exception("Parol noto'g'ri!");
            // 3. Statusni tekshiramiz (Faqat Active userlar kira oladi)
            if (user.Status != UserStatus.Active)
                throw new Exception("Akkauntingiz faollashtirilmagan yoki blocklangan!");
            // 4. LastLoginTime yangilaymiz
            user.LastLoginTime = DateTime.UtcNow;
            await this.userService.ModifyUserAsync(user);
            // 5. Token generatsiya qilamiz
            string token = this.tokenService.GenerateJwtToken(user);
            return new AuthResponse
            {
                Token = token,
                User = new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name!,
                    Email = user.Email!
                }
            };
        }
    }
}
