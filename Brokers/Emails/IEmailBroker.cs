//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Brokers.Emails
{
    public interface IEmailBroker
    {
        ValueTask SendEmailAsync(string receiverAddress, string subject, string body);
    }
}
