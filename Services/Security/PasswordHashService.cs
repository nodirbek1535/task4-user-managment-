//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Services.Security
{
    public class PasswordHashService: IPasswordHashService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string hashedPassword, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(hashedPassword, hash);
        }
    }
}
