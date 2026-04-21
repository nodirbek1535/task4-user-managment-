//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Xeptions;

namespace task4_user_managment_.Models.Exceptions
{
    public class UserValidationException:Xeption
    {
        public UserValidationException(Xeption innerException)
            : base(message: "User validation error occurred, please fix the errors and try again.", innerException)
        { }
    }
}
