using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Notification;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace SW.HomeVisits.WebAPI.Helper
{
    public class ChemistTrackingService : IChemistTrackingService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICultureService _cultureService;
        private readonly INotitificationService _notificationService;
        public ChemistTrackingService(ICommandBus commandBus, IQueryProcessor queryProcessor, ICultureService cultureService, INotitificationService notificationService)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _cultureService = cultureService;
            _notificationService = notificationService;
        }

        public void ChemistTrackingBackGroundService()
        {
            var currentTime = DateTime.Now.TimeOfDay;

            //Get All Chemists
            var chemistsResponse = _queryProcessor.ProcessQueryAsync<IGetAllChemistsQuery, IGetAllChemistsQueryResponse>(new GetAllChemistsQuery());

            if (chemistsResponse.Result.Chemists.Count > 0)
            {
                for (int i = 0; i < chemistsResponse.Result.Chemists.Count; i++)
                {

                    //Get Chemist Schedule plan
                    var chemistSchedulePlanResponse =  _queryProcessor.ProcessQueryAsync<IGetChemistSchedulePlanQuery, IGetChemistSchedulePlanQueryResponse>(new GetChemistSchedulePlanQuery
                    {
                        ClientId = chemistsResponse.Result.Chemists[i].ClientId,
                        ChemistId = chemistsResponse.Result.Chemists[i].ChemistId,
                        date = DateTime.Now,
                        DayFilter = true
                    });

                    //Check if no record saved in chemist tracking log for last 20 mins
                    if (chemistSchedulePlanResponse.Result.ChemistSchedulePlans.Count > 0)
                    {
                        var result = chemistSchedulePlanResponse.Result.ChemistSchedulePlans.Any(x => x.StartTime < currentTime && currentTime < x.EndTime);
                        if (result)
                        {
                            //apiResponse.Message = "match found";
                            var chemistTrackResponse =  _queryProcessor.ProcessQueryAsync<IGetChemistTrackingLogQuery, IGetChemistTrackingLogQueryResponse>(new GetChemistTrackingLogQuery
                            {
                                ChemistId = chemistsResponse.Result.Chemists[i].ChemistId
                            });

                            if (chemistTrackResponse.Result.ChemistTrackingLog == null)
                            {

                                #region Send push notification

                                var userDeviceResponse =  _queryProcessor.ProcessQueryAsync<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>(new GetUserDeviceByChemistIdQuery
                                {
                                    ChemistId = chemistsResponse.Result.Chemists[i].ChemistId
                                });

                              //  string deviceToken = userDeviceResponse.Result.UserDevice.FireBaseDeviceToken;

                                string deviceToken = "";
                                _notificationService.SendNotification(new SendNotificationDto
                                {
                                    NotificationId = Guid.NewGuid(),
                                    UserId = chemistsResponse.Result.Chemists[i].ChemistId,
                                    Reciever = chemistsResponse.Result.Chemists[i].ChemistId.ToString(),
                                    NotificationType = NotificationTypes.MobilePush,
                                    Title = "Chemist Tracking",
                                    Message = "You’re considered as absent as you’re closing GPS",
                                    TitleAr = "Chemist Tracking",
                                    MessageAr = "انت الان تعتبر غائب عن العمل لعدم تشغيل GPS",
                                    DeviceToken = deviceToken,
                                    SystemNotificationType = SystemNotificationTypes.ChemistTracking,
                                    Culture = CultureNames.en,
                                    ClickAction = "com.smartwave.chemist.feature.home"
                                });

                                #endregion
                            }
                            else
                            {
                                if (chemistTrackResponse.Result.ChemistTrackingLog.CreationDate.AddMinutes(20) < DateTime.Now)
                                {

                                    #region Send push notification

                                    var userDeviceResponse = _queryProcessor.ProcessQueryAsync<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>(new GetUserDeviceByChemistIdQuery
                                    {
                                        ChemistId = chemistsResponse.Result.Chemists[i].ChemistId
                                    });

                                    string deviceToken = userDeviceResponse?.Result?.UserDevice?.FireBaseDeviceToken;

                                     _notificationService.SendNotification(new SendNotificationDto
                                    {
                                        NotificationId = Guid.NewGuid(),
                                        UserId = chemistsResponse.Result.Chemists[i].ChemistId,
                                        Reciever = chemistsResponse.Result.Chemists[i].ChemistId.ToString(),
                                        NotificationType = NotificationTypes.MobilePush,
                                        Title = "Chemist Tracking",
                                        Message = "You’re considered as absent as you’re closing GPS",
                                        TitleAr = "Chemist Tracking",
                                        MessageAr = "انت الان تعتبر غائب عن العمل لعدم تشغيل GPS",
                                        DeviceToken = deviceToken,
                                        SystemNotificationType = SystemNotificationTypes.ChemistTracking,
                                        Culture = CultureNames.en,
                                        ClickAction = "com.smartwave.chemist.feature.home"
                                    });

                                    #endregion
                                }

                            }
                        }
                    }
                }
            }

        }
    }
}
