using System;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Notification;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.Notification
{
    public class NotificationService : INotitificationService
    {
        private readonly IPushNotificationManager _pushNotificationManager;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHomeVisitsUnitOfWork _unitOfWork;

        public NotificationService(IPushNotificationManager pushNotificationManager, INotificationRepository notificationRepository, IHomeVisitsUnitOfWork unitOfWork)
        {
            _pushNotificationManager = pushNotificationManager;
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task SendNotification(SendNotificationDto notificationDto)
        {
            try
            {
                switch (notificationDto.NotificationType)
                {
                    case Abstract.Enum.NotificationTypes.MobilePush:
                        _notificationRepository.PresistNewNotification(new Domain.Entities.Notification
                        {
                            NotificationId = notificationDto.NotificationId,
                            UserId = notificationDto.UserId,
                            Reciever = notificationDto.Reciever,
                            Title = notificationDto.Title,
                            Message = notificationDto.Message,
                            Link = notificationDto.Link,
                            NotificationType = (int)Abstract.Enum.NotificationTypes.MobilePush,
                            CreatedBy = 1,
                            CreationDate = DateTime.Now,
                            TitleAr = notificationDto.TitleAr,
                            MessageAr = notificationDto.MessageAr

                        });

                        if (notificationDto.SystemNotificationType == Abstract.Enum.SystemNotificationTypes.Visit)
                        {
                            _notificationRepository.AddNewVisitNotification(new Domain.Entities.VisitNotification
                            {
                                VisitNotificationId = Guid.NewGuid(),
                                NotificationId = notificationDto.NotificationId,
                                VisitId = notificationDto.VisitId.Value
                            });
                        }
                        _unitOfWork.SaveChanges();

                        switch (notificationDto.SystemNotificationType)
                        {
                            case SystemNotificationTypes.Visit:
                                await _pushNotificationManager.SendPushNotification(notificationDto.VisitId.Value.ToString(),
                                       notificationDto.Culture == CultureNames.en ? notificationDto.Title : notificationDto.TitleAr,
                                       notificationDto.Culture == CultureNames.en ? notificationDto.Message : notificationDto.MessageAr, notificationDto.DeviceToken,
                                       notificationDto.ClickAction);
                                break;
                            case SystemNotificationTypes.Chemist:
                                await _pushNotificationManager.SendPushNotification(null,
                                      notificationDto.Culture == CultureNames.en ? notificationDto.Title : notificationDto.TitleAr,
                                      notificationDto.ChemistScheduleDto, notificationDto.DeviceToken, notificationDto.ClickAction);
                                break;
                            case SystemNotificationTypes.ChemistTracking:
                                await _pushNotificationManager.SendPushNotification(null,
                                      notificationDto.Culture == CultureNames.en ? notificationDto.Title : notificationDto.TitleAr,
                                      notificationDto.Culture == CultureNames.en ? notificationDto.Message : notificationDto.MessageAr, notificationDto.DeviceToken,
                                      notificationDto.ClickAction);
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
