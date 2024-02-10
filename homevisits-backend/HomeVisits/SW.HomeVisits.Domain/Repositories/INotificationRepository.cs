using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface INotificationRepository : IDisposableRepository
    {
        void PresistNewNotification(Notification notification);
        void AddNewVisitNotification(VisitNotification visitNotification);

    }
}
