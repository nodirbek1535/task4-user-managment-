//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Models.Responses;

namespace task4_user_managment_.Models.Responces
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
        public UserResponse User { get; set; } = default!;
    }
}
