using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class NotificationRepository : EfRepository<HomeVisitsDomainContext>, INotificationRepository
    {
        public NotificationRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void AddNewVisitNotification(VisitNotification visitNotification)
        {
            Context.VisitNotifications.Add(visitNotification);
        }

        public void PresistNewNotification(Notification notification)
        {
            Context.Notifications.Add(notification);
        }
    }
}
