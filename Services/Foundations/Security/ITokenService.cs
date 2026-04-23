//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Foundations.Security
{
    public interface ITokenService
    {
        string GenerateEmailConfirmationToken();
        DateTime GetTokenExpirationTime();
        string GenerateJwtToken(User user);
    }
}
