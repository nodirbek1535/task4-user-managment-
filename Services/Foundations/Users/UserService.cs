//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Microsoft.Data.SqlClient;
using task4_user_managment_.Brokers.Loggings;
using task4_user_managment_.Brokers.Storages;
using task4_user_managment_.Models.Exceptions;
using task4_user_managment_.Services.Foundations.Security;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Foundations.Users
{
    public partial class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IPasswordHashService passwordHashService;
        private readonly ITokenService tokenService;

        public UserService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IPasswordHashService passwordHashService,
            ITokenService tokenService)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.passwordHashService = passwordHashService;
            this.tokenService = tokenService;
        }

        public ValueTask<User> AddUserAsync(User user) =>
            TryCatch(async () =>
            {
                ValidateUserOnAdd(user);

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
            });

        public ValueTask<User> RetrieveUserByAsync(Guid userId) =>
            TryCatch(async () =>
            {
                ValidateUserId(userId);
                User maybeUser = await this.storageBroker.SelectUserByIdAsync(userId);
                ValidateUserStorage(maybeUser, userId);

                return maybeUser!;
            });

        public IQueryable<User> GetAllUsers()
        {
            try
            {
                return this.storageBroker.SelectAllUsers();
            }
            catch(SqlException sqlException)
            {
                var failedUserStorageException =
                    new FailedUserStorageException(sqlException);

                this.loggingBroker.LogError(sqlException);

                throw new UserDependencyException(failedUserStorageException);
            }
            catch(Exception ex)
            {
                var failedUserServiceException =
                    new FailedUserServiceException(ex);

                throw new UserServiceException(failedUserServiceException);
            }
        }

        public ValueTask<User> ModifyUserAsync(User user) =>
            TryCatch(async () =>
            {
                ValidateUserOnModify(user);

                User maybeUser = 
                    await this.storageBroker.SelectUserByIdAsync(user.Id);

                ValidateUserStorage(maybeUser, user.Id);
                user.UpdatedDate = DateTime.UtcNow;

                return await this.storageBroker.UpdateUserAsync(user);
             });
    }
}
