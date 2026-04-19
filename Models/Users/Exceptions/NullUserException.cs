//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Xeptions;

namespace task4_user_managment_.Models.Users.Exceptions
{
    public class NullUserException:Xeption
    {
        public NullUserException() 
            : base("User is null.")
        { }
    }
}
