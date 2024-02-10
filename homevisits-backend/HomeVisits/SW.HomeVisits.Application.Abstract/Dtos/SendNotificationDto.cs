using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class SendNotificationDto
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        public string Reciever { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public NotificationTypes NotificationType { get; set; }
        public string DeviceToken { get; set; }
        public Guid? VisitId { get; set; }
        public string TitleAr { get; set; }
        public string MessageAr { get; set; }
        public SystemNotificationTypes SystemNotificationType { get; set; }
        public CultureNames Culture { get; set; }
        public HomeVisitsWebApiResponse<ChemistScheduleDto> ChemistScheduleDto { get; set; }
        public string ClickAction { get; set; }


    }
}
