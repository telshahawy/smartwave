using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class VisitNotification : Entity<Guid>
    {
        public Guid VisitNotificationId
        {
            get => Id;
            set => Id = value;
        }

        public Guid VisitId { get; set; }
        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }
        public Visit Visit { get; set; }
    }
}
