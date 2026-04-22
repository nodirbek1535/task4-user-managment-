//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<User> InsertUserAsync(User user);
        ValueTask<User> SelectUserByIdAsync(Guid userId);   
        IQueryable<User> SelectAllUsers();
        ValueTask<User> UpdateUserAsync(User user);
    }
}
