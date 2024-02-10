using System;
using System.Threading.Tasks;

namespace SW.HomeVisits.Application.Notification
{
    public interface IPushNotificationManager
    {
        Task SendPushNotification(string id, string title, object message, string deviceToken, string clickAction);
    }
}