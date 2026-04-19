//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Services.Foundations.Security
{
    public interface ITokenService
    {
        string GenerateEmailConfirmationToken();
        DateTime GetTokenExpirationTime();
    }
}
