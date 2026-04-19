//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Brokers.Security
{
    public class HashBroker:IHashBroker
    {
        public string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string hashedPassword, string hash) =>
            BCrypt.Net.BCrypt.Verify(hashedPassword, hash);
    }
}
