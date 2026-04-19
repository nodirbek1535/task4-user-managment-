//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using System.Security.Cryptography;

namespace task4_user_managment_.Brokers.Security
{
    public class RandomBroker:IRandomBroker
    {
        public string GenerateString(int length)
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(length);
            return Convert.ToBase64String(bytes);
        }
    }
}
