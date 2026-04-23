//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Brokers.Emails;

namespace task4_user_managment_.Services.Foundations.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBroker emailBroker;

        public EmailService(IEmailBroker emailBroker) =>
            this.emailBroker = emailBroker;

        public async ValueTask SendVerificationEmailAsync(string userEmail, string name, string token)
        {
            string verificationLink = $"https://localhost7049/api/user/confirm?token={token}";

            string subject = "Please verify your email";

            string body =
                $@"<div style='font-family: Arial, sans-serif; border: 1px solid #ddd; padding: 20px;'>
                    <h2 style='color: #007bff;'>Assalomu alaykum, {name}!</h2>
                    <p>Mini Admin Panelda ro'yxatdan o'tganingiz uchun rahmat.</p>
                    <p>Akkauntingizni faollashtirish uchun quyidagi tugmani bosing:</p>
                    <a href='{verificationLink}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px;'>Emailni tasdiqlash</a>
                    <p style='margin-top: 20px; font-size: 0.8em; color: #666;'>Agar siz ro'yxatdan o'tmagan bo'lsangiz, bu xatga e'tibor bermang.</p>
                </div>";

            await this.emailBroker.SendEmailAsync(userEmail, subject, body);
        }
    }
}
