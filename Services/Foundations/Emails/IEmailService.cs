//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Services.Foundations.Emails
{
    public interface IEmailService
    {
        ValueTask SendVerificationEmailAsync(string userEmail, string name, string token);
    }
}
