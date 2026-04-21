//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Xeptions;

namespace task4_user_managment_.Models.Exceptions
{
    public class FailedUserServiceException:Xeption
    {
        public FailedUserServiceException(Exception innerException)
            : base(message: "Failed user service error occurred, contact support.", innerException)
        { }
    }
}
