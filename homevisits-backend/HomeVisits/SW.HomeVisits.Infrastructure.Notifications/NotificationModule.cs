using System;
using Microsoft.Extensions.DependencyInjection;
using SW.HomeVisits.Application.Notification;

namespace SW.HomeVisits.Infrastructure.Notifications
{
    public class NotificationModule
    {
        public void Initialise(IServiceCollection services)
        {
            services.AddTransient<IPushNotificationManager, PushNotificationManager>();
        }
    }
}
