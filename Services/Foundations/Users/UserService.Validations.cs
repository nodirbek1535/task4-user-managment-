//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using task4_user_managment_.Models.Exceptions;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Foundations.Users
{
    public partial class UserService
    {
        private void ValidateUserOnAdd(User user)
        {
            ValidateUserNotNull(user);

            Validate(
                (Rule: IsInvalid(user.Name), Parameter: nameof(User.Name)),
                (Rule: IsInvalid(user.Email), Parameter: nameof(User.Email)),
                (Rule: IsInvalid(user.PasswordHash), Parameter: nameof(User.PasswordHash))
            );
        }

        private void ValidateUserNotNull(User user)
        {
            if (user is null)
                throw new NullUserException();
        }

        private void ValidateUserId(Guid userId)
        {
            Validate(
                (Rule: IsInvalid(userId), Parameter: nameof(User.Id))
            );
        }

        private void ValidateUserStorage(User maybeUser, Guid userId)
        {
            if (maybeUser is null)
                throw new NotFoundUserException(userId);
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidUserException = new InvalidUserException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidUserException.UpsertDataList(parameter, rule.Message);
                }
            }

            invalidUserException.ThrowIfContainsErrors();
        }
    }
}
