//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Xeptions;

namespace task4_user_managment_.Models.Users.Exceptions
{
    public class InvalidUserException:Xeption
    {
        public InvalidUserException()
            : base("Invalid user. Please correct the errors and try again.")
        { }
    }
}
