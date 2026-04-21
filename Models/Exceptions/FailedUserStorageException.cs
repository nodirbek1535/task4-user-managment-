//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Xeptions;

namespace task4_user_managment_.Models.Exceptions
{
    public class FailedUserStorageException:Xeption
    {
        public FailedUserStorageException(Exception innerException)
            : base(message: "Failed to storage user error occurred, contact support.", innerException)
        { }
    }
}
