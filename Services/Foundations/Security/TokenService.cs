//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Brokers.Security;

namespace task4_user_managment_.Services.Foundations.Security
{
    public class TokenService:ITokenService
    {
        private readonly IRandomBroker randomBroker;
        
        public TokenService(IRandomBroker randomBroker) =>
            this.randomBroker = randomBroker;

        public string GenerateEmailConfirmationToken() =>
            this.randomBroker.GenerateString(32);

        public DateTime GetTokenExpirationTime() =>
            DateTime.UtcNow.AddHours(1);
    }
}
