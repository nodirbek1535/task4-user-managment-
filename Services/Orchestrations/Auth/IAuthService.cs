//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Models.Requests.Auth;
using task4_user_managment_.Models.Responses;

namespace task4_user_managment_.Services.Orchestrations.Auth
{
    public interface IAuthService
    {
        ValueTask<UserResponse> RegisterAsync(RegisterRequest request);
        ValueTask<AuthResponse> LoginAsync(LoginRequest request);
        ValueTask<bool> ConfirmEmailAsync(string token);
    }
}
