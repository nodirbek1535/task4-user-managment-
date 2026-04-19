//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Brokers.Security
{
    public interface IHashBroker
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string hash);
    }
}
