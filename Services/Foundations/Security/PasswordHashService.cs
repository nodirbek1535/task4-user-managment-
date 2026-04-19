//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Brokers.Security;

namespace task4_user_managment_.Services.Foundations.Security
{
    public class PasswordHashService : IPasswordHashService
    {
        private readonly IHashBroker hashBroker;

        public PasswordHashService(IHashBroker hashBroker) =>
            this.hashBroker = hashBroker;

        public string HashPassword(string password) =>
            this.hashBroker.HashPassword(password);

        public bool VerifyPassword(string hashedPassword, string hash) =>
            this.hashBroker.VerifyPassword(hashedPassword, hash);
    }
}
