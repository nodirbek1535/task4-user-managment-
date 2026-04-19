//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace UserManagement.Core.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string? EmailConfirmationToken { get; set; }
        public DateTime? TokenExpiresAt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
