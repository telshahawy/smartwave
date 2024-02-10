using System;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.Notification
{
    public interface INotitificationService
    {
        Task SendNotification(SendNotificationDto notificationDto);
    }
}
