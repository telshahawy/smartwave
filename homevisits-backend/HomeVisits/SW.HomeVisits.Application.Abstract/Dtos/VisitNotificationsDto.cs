using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class VisitNotificationsDto
    {
        public Guid VisitId { get; set; }
        public Guid NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
