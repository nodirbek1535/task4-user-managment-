//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Xeptions;

namespace task4_user_managment_.Models.Exceptions
{
    public class LockedUserException:Xeption
    {
        public LockedUserException(Exception innerException )
            : base(message: "Locked user record error occurred. Please try again later.",
                  innerException)
        { }
    }
}
