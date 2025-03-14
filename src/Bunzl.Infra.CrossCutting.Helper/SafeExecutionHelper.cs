namespace Bunzl.Infra.CrossCutting.Helper;

public static class SafeExecutionHelper
{
    public static async Task<T> SafeExecuteAsync<T>(Func<Task<T>> func, Action<string, string> addNotification, string notificationKey, string notificationMessage)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            addNotification(notificationKey, notificationMessage);
            return default;
        }
    }
}

