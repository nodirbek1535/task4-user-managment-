//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Xeptions;

namespace task4_user_managment_.Models.Exceptions
{
    public class NotFoundUserException:Xeption
    {
        public NotFoundUserException(Guid userId)
            : base(message: $"Couldn't find user with id: {userId}.")
        { }
    }
}
