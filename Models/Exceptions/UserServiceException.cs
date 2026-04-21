//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Xeptions;

namespace task4_user_managment_.Models.Exceptions
{
    public class UserServiceException : Xeption
    {
        public UserServiceException(Exception innerException)
            : base(message: "User service error occurred, contact support.", innerException)
        { }
    }
}
