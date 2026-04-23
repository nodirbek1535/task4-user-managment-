//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using MailKit.Net.Smtp;
using MimeKit;

namespace task4_user_managment_.Brokers.Emails
{
    public class EmailBroker:IEmailBroker
    {
        private readonly IConfiguration configuration;
        public EmailBroker(IConfiguration configuration) =>
            this.configuration = configuration;

        public async ValueTask SendEmailAsync(string receiverAddress, string subject, string body)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            // Null xatolarini oldini olish uchun qiymatlarni o'qib olamiz
            string fromAddress = emailSettings["From"] ?? 
                throw new InvalidOperationException("Email 'From' is not configured.");

            string appPassword = emailSettings["AppPassword"] ?? 
                throw new InvalidOperationException("Email 'AppPassword' is not configured.");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Mini Admin Panel", fromAddress));
            message.To.Add(new MailboxAddress("", receiverAddress));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            // MailKit'ning SmtpClient'idan aniq foydalanish
            using var client = new MailKit.Net.Smtp.SmtpClient();

            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(fromAddress, appPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
