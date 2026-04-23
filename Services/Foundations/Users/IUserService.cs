//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> AddUserAsync(User user);
        ValueTask<User> RetrieveUserByAsync(Guid userId);
        IQueryable<User> GetAllUsers();
        ValueTask<User> ModifyUserAsync(User user);
        ValueTask<User> RemoveUserByIdAsync(Guid userId);
        ValueTask<User> RetrieveUserByTokenAsync(string token);
    }
}
