//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

namespace task4_user_managment_.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(Exception exception);
        void LogCritical(Exception exception);
    }
}
