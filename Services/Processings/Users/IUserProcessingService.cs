//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask BlockUsersAsync(List<Guid> userIds);
        ValueTask UnblockUsersAsync(List<Guid> userIds);
        ValueTask DeleteUsersAsync(List<Guid> userIds);
    }
}
