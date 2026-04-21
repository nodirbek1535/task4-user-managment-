//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Models.Requests.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
