//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask BlockUsersAsync(List<Guid> userIds);
        ValueTask UnblockUsersAsync(List<Guid> userIds);
        ValueTask DeleteUsersAsync(List<Guid> userIds);
        ValueTask DeleteUnverifiedUsersAsync(List<Guid> userIds);
        IQueryable<User> GetAllUsersSorted();
    }
}
