using Microsoft.Toolkit.Uwp.Notifications;
namespace NoteManager.CommonTypes.Data
{
    public static class ActionNotification
    {
        private static readonly string[] _notificationTitle = { "Успешное выполнение!", "Ошибка выполнения!"};
        public enum NotificationResultType
        { 
            ResultOK,
            ResultError
        };
        public static void ShowActionNotification(string notificationText, NotificationResultType notificationType)
        {
            new ToastContentBuilder()
                .AddArgument("conversationId", 9813)
                .AddText(_notificationTitle[(int)notificationType])
                .AddText(notificationText)
                .Show();                                  
        }
    }
}
