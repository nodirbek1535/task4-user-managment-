//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Services.Foundations.Users;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Processings.Users
{
    public class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;

        public UserProcessingService(IUserService userService) =>
            this.userService = userService;

        public async ValueTask BlockUsersAsync(List<Guid> userIds)
        {
            foreach (Guid userId in userIds)
            {
                User user = await this.userService.RetrieveUserByAsync(userId);
                user.Status = UserStatus.Blocked;
                await this.userService.ModifyUserAsync(user);
            }
        }

        public async ValueTask UnblockUsersAsync(List<Guid> userIds)
        {
            foreach (Guid userId in userIds)
            {
                User user = await this.userService.RetrieveUserByAsync(userId);
                // Unverified → Unverified qoladi, Blocked → Active bo'ladi
                if (user.Status == UserStatus.Blocked)
                    user.Status = UserStatus.Active;

                await this.userService.ModifyUserAsync(user);
            }
        }

        public async ValueTask DeleteUsersAsync(List<Guid> userIds)
        {
            foreach (Guid userId in userIds)
                await this.userService.RemoveUserByIdAsync(userId);
        }

        public async ValueTask DeleteUnverifiedUsersAsync(List<Guid> userIds)
        {
            foreach (Guid userId in userIds)
            {
                User user = await this.userService.RetrieveUserByAsync(userId);

                if (user.Status == UserStatus.Unverified)
                    await this.userService.RemoveUserByIdAsync(userId);
            }
        }

        public IQueryable<User> GetAllUsersSorted() =>
            this.userService.GetAllUsers()
                .OrderByDescending(u => u.LastLoginTime);
    }
}
