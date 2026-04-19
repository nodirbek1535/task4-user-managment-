//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> AddUserAsync(User user);
    }
}
