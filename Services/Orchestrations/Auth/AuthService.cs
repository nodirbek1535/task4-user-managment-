//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

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

            // Fire-and-forget: email async jo'natiladi, foydalanuvchi kutmaydi
            _ = Task.Run(async () =>
            {
                try
                {
                    await this.emailService.SendVerificationEmailAsync(
                        createdUser.Email!,
                        createdUser.Name!,
                        createdUser.EmailConfirmationToken!);
                }
                catch { /* Email xatosi butun registratsiyani to'xtata olmaydi */ }
            });

            return new UserResponse
            {
                Id = createdUser.Id,
                Name = createdUser.Name!,
                Email = createdUser.Email!
            };
        }

        public async ValueTask<bool> ConfirmEmailAsync(string token)
        {
            var user = await this.userService.RetrieveUserByTokenAsync(token);

            if (user.TokenExpiresAt < DateTime.UtcNow)
                return false;

            // Faqat Unverified bo'lsa Active qilamiz; Blocked qolsa — Blocked qoladi
            if (user.Status == UserStatus.Unverified)
                user.Status = UserStatus.Active;

            user.EmailConfirmationToken = null;
            await this.userService.ModifyUserAsync(user);

            return true;
        }

        public async ValueTask<AuthResponse> LoginAsync(LoginRequest request)
        {
            // 1. Email bo'yicha qidirish
            var user = await this.userService.RetrieveUserByEmailAsync(request.Email!);

            // 2. Parolni tekshirish
            bool isPasswordValid =
                this.passwordHashService.VerifyPassword(request.Password!, user.PasswordHash!);

            if (!isPasswordValid)
                throw new Exception("Parol noto'g'ri!");

            // 3. Faqat Blocked kirololmaydi (Unverified ham kira oladi — task talabi!)
            if (user.Status == UserStatus.Blocked)
                throw new Exception("Akkauntingiz bloklangan. Iltimos, administrator bilan bog'laning.");

            // 4. LastLoginTime yangilaymiz
            user.LastLoginTime = DateTime.UtcNow;
            await this.userService.ModifyUserAsync(user);

            // 5. JWT token generatsiya
            string jwtToken = this.tokenService.GenerateJwtToken(user);

            return new AuthResponse
            {
                Token = jwtToken,
                ExpireAt = DateTime.UtcNow.AddMinutes(1440),
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
