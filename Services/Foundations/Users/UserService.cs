//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Brokers.Storages;
using task4_user_managment_.Services.Foundations.Security;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IPasswordHashService passwordHashService;
        private readonly ITokenService tokenService;

        public UserService(
            IStorageBroker storageBroker,
            IPasswordHashService passwordHashService,
            ITokenService tokenService)
        {
            this.storageBroker = storageBroker;
            this.passwordHashService = passwordHashService;
            this.tokenService = tokenService;
        }

        public async ValueTask<User> AddUserAsync(User user)
        {
            user.Id = Guid.NewGuid();

            user.PasswordHash =
                this.passwordHashService.HashPassword(user.PasswordHash);

            user.TokenExpiresAt =
                this.tokenService.GetTokenExpirationTime();

            user.Status = UserStatus.Unverified;

            user.CreatedDate = DateTime.UtcNow;
            user.UpdatedDate = DateTime.UtcNow;
            user.RegistrationTime = DateTime.UtcNow;

            return await this.storageBroker.InsertUserAsync(user);
        }
    }
}
