using System;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Notification:Entity<Guid>
    {
        public Guid NotificationId { get =>Id; set =>Id=value; }
        public Guid UserId { get; set; }
        public string Reciever { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public int NotificationType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string TitleAr { get; set; }
        public string MessageAr { get; set; }
    }
}
