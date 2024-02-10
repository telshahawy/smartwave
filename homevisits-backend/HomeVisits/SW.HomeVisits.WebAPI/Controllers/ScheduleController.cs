using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.WebAPI.Models;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using Microsoft.Extensions.Localization;
using SW.HomeVisits.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;
using System.Net;
using System.IO;
using System.Text;
using SW.HomeVisits.Application.Abstract.Notification;
using SW.HomeVisits.Application.Abstract.Commands;
using System.Security.Cryptography.X509Certificates;
using Hangfire;
using Hangfire.Storage.Monitoring;
using Cronos;
using SW.HomeVisits.Application.Models;

namespace SW.HomeVisits.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICultureService _cultureService;
        private readonly INotitificationService _notificationService;

        public ScheduleController(ICommandBus commandBus, IQueryProcessor queryProcessor, ICultureService cultureService, INotitificationService notificationService)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _cultureService = cultureService;
            _notificationService = notificationService;
        }

        [HttpGet("MySchedule")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<MyScheduleDto>>), 200)]
        public async Task<IActionResult> MySchedule([FromQuery] SearchMyScheduleModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<List<MyScheduleDto>>();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }
                    var response = await _queryProcessor.ProcessQueryAsync<ISearchMyScheduleQuery, ISearchMyScheduleQueryResponse>(new SearchMyScheduleQuery
                    {
                        ChemistId = userInfo.UserId,
                        Order = model.order,
                        VisitDate = model.visitDate,
                        CultureName = GetCultureName(),
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    if (!response.mySchedule.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.mySchedule;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("ChemistNoScheduleFound", GetCulture());
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.mySchedule;
                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "Invalide Input Parameter";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Send")]
        public async Task<IActionResult> Test(string deviceToken)
        {
            var userInfo = GetCurrentUserId();
            await _notificationService.SendNotification(new SendNotificationDto
            {
                Reciever = userInfo.UserId.ToString(),
                NotificationType = NotificationTypes.MobilePush,
                Title = "New visit",
                Message = "new visit from customer A",
                DeviceToken = deviceToken
            });
            return Ok();
        }

        [HttpPost("SendChemistTracking")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> Post([FromBody] AddChemistTrackingLogModel model)
        {

            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<bool>();

                if (ModelState.IsValid)
                {
                    var chemistTrackingLogCommand = new CreateChemistTrackingLogCommand
                    {
                        ChemistId = Guid.Parse("2d954a43-ac4b-4de4-9d87-ab260193c894"),
                        // ChemistId = GetCurrentUserId().UserId,
                        Longitude = model.Longitude,
                        Latitude = model.Latitude,
                        DeviceSerialNumber = model.DeviceSerialNumber,
                        MobileBatteryPercentage = model.MobileBatteryPercentage,
                        UserName = "test"

                    };

                    await _commandBus.SendAsync((ICreateChemistTrackingLogCommand)chemistTrackingLogCommand);
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = true;
                    return Ok(apiResponse);

                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "Invalide Input Parameter";
                    apiResponse.Response = false;
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("SendChemistAction")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<VisitDetailsDto>), 200)]
        public async Task<IActionResult> SendChemistAction([FromBody] CreateVisitStatusCommand model)
        {

            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<VisitDetailsDto>();
                var userInfo = GetCurrentUserId();

                if (ModelState.IsValid)
                {
                    model.CreatedBy = userInfo.UserId;
                    await _commandBus.SendAsync((ICreateVisitStatusCommand)model);
                    var response = await _queryProcessor.ProcessQueryAsync<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>(new GetVisitDetailsQuery
                    {
                        VisitId = model.VisitId,
                        CultureName = GetCultureName()
                    });

                    List<Guid> receivers = new List<Guid> { };
                    if (response.VisitDetails?.ChemistId == null || response.VisitDetails?.ChemistId == Guid.Empty)
                    {
                        receivers.Add(response.VisitDetails.PatientId);

                    }
                    else
                    {
                        receivers.Add((Guid)response.VisitDetails?.ChemistId);

                        receivers.Add(response.VisitDetails.PatientId);

                    }


                    if (model.VisitActionTypeId == (int)VisitActionTypes.Done)
                    {
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitCompletedSuccessfully", GetCulture());
                        #region Send push notification

                        foreach (var item in receivers)
                        {


                            var userDeviceResponse = await _queryProcessor.ProcessQueryAsync<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>(new GetUserDeviceByChemistIdQuery
                            {
                                ChemistId = item
                            });


                            string deviceToken = userDeviceResponse.UserDevice?.FireBaseDeviceToken;

                            _notificationService.SendNotification(new SendNotificationDto
                            {
                                NotificationId = Guid.NewGuid(),
                                UserId = item,
                                Reciever = item.ToString(),
                                NotificationType = NotificationTypes.MobilePush,
                                Title = string.Format("Home Visit # {0}", response.VisitDetails.VisitCode),
                                Message = string.Format("Home Visit # {0} {1}", response.VisitDetails.VisitCode, "is confirmed"),
                                TitleAr = string.Format("الحجز رقم {0}", response.VisitDetails.VisitCode),
                                MessageAr = string.Format("تم تأكيد الحجز رقم {0}", response.VisitDetails.VisitCode),
                                DeviceToken = deviceToken,
                                VisitId = response.VisitDetails.VisitId,
                                SystemNotificationType = SystemNotificationTypes.Visit,
                                Culture = GetCultureName(),
                                ClickAction = "com.smartwave.chemist.feature.visit.ActivityVisitDetails"
                            });
                        }
                        #endregion
                    }
                    else if (model.VisitActionTypeId == (int)VisitActionTypes.Cancelled)
                    {
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitIsCancelled", GetCulture());
                        //await SendNotification(response, "Visit successfully reserved", "تم إضافة الحجز بنجاح");
                        #region Send push notification

                        foreach (var item in receivers)
                        {


                            var userDeviceResponse = await _queryProcessor.ProcessQueryAsync<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>(new GetUserDeviceByChemistIdQuery
                            {
                                ChemistId = item
                            });


                            string deviceToken = userDeviceResponse.UserDevice?.FireBaseDeviceToken;

                            _notificationService.SendNotification(new SendNotificationDto
                            {
                                NotificationId = Guid.NewGuid(),
                                UserId = item,
                                Reciever = item.ToString(),
                                NotificationType = NotificationTypes.MobilePush,
                                Title = string.Format("Home Visit # {0}", response.VisitDetails.VisitCode),
                                Message = string.Format("Home Visit # {0} {1}", response.VisitDetails.VisitCode, "is canceled"),
                                TitleAr = string.Format("الحجز رقم {0}", response.VisitDetails.VisitCode),
                                MessageAr = string.Format("تم الغاء الحجز رقم {0}", response.VisitDetails.VisitCode),
                                DeviceToken = deviceToken,
                                VisitId = response.VisitDetails.VisitId,
                                SystemNotificationType = SystemNotificationTypes.Visit,
                                Culture = GetCultureName(),
                                ClickAction = "com.smartwave.chemist.feature.visit.ActivityVisitDetails"
                            });
                        }
                        #endregion
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.VisitDetails;
                    return Ok(apiResponse);

                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "Invalid Input Parameter";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("GetPatientAllVisitNotifications")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<VisitNotificationsDto>>), 200)]
        public async Task<IActionResult> GetPatientAllVisitNotifications(Guid? PatientId)
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<List<VisitNotificationsDto>>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetPatientAllVisitNotificationsQuery, IGetPatientAllVisitNotificationsQueryResponse>(new GetPatientAllVisitNotificationsQuery
                    {

                        PatientId = PatientId ?? GetCurrentUserId().UserId,
                        CultureName = GetCultureName()

                    });
                    if (!response.visitNotifications.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.visitNotifications;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitNotificationNotFound", GetCulture());
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.visitNotifications;
                    return Ok(apiResponse);

                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "Invalide Input Parameter";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       
        [HttpGet("GetAllVisitNotifications")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<VisitNotificationsDto>>), 200)]
        public async Task<IActionResult> VisitNotifications()
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<List<VisitNotificationsDto>>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetAllVisitNotificationsQuery, IGetAllVisitNotificationsQueryResponse>(new GetAllVisitNotificationsQuery
                    {

                        ChemistId = GetCurrentUserId().UserId,
                        CultureName = GetCultureName()

                    });
                    if (!response.visitNotifications.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.visitNotifications;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitNotificationNotFound", GetCulture());
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.visitNotifications;
                    return Ok(apiResponse);

                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "Invalide Input Parameter";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("GetActionReasons")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<ActionReasonKeyValueDto>>), 200)]
        public async Task<IActionResult> GetActionReasons(int actionType)
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<List<ActionReasonKeyValueDto>>();
                var userInfo = GetCurrentUserId();
                if (ModelState.IsValid)
                {

                    var response = await _queryProcessor.ProcessQueryAsync<IGetReasonsKeyValueQuery, IGetReasonsKeyValueQueryResponse>(new GetReasonsKeyValueQuery
                    {

                        VisitTypeActionId = actionType,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.ActionReasons;
                    return Ok(apiResponse);

                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "Invalid Input Parameter";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("AddUserDevice")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<ChemistScheduleDto>), 200)]
        public async Task<IActionResult> AddUserDevice([FromBody] AddUserDeviceModel model)
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<ChemistScheduleDto>();

                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var addUserDeviceCommand = new AddUserDeviceCommand
                    {
                        UserDeviceId = Guid.NewGuid(),
                        UserId = userInfo.UserId,
                        DeviceSerialNumber = model.DeviceSerialNumber,
                        FireBaseDeviceToken = model.FireBaseDeviceToken,
                    };

                    await _commandBus.SendAsync((IAddUserDeviceCommand)addUserDeviceCommand);

                    ChemistScheduleDto chemistScheduleDto = new ChemistScheduleDto();
                    //Get Chemist Schedule plan
                    var chemistSchedulePlanResponse = await _queryProcessor.ProcessQueryAsync<IGetChemistSchedulePlanQuery, IGetChemistSchedulePlanQueryResponse>(new GetChemistSchedulePlanQuery
                    {
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        ChemistId = userInfo.UserId,
                        date = DateTime.Now,
                        DayFilter = false
                    });

                    chemistScheduleDto.Schedule = chemistSchedulePlanResponse.ChemistSchedulePlans.GroupBy(x => x.Day).Select(s => new Days
                    {

                        Day = s.Key,
                        Times = s.Select(t => new Time
                        {
                            StartTime = t.StartTime.ToString(),
                            EndTime = t.EndTime.ToString()

                        }).ToList()

                    }).ToList();

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = chemistScheduleDto;
                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    apiResponse.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("ChemistSchedule")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<ChemistSchedulePlanDto>), 200)]
        public async Task<IActionResult> ChemistSchedule()
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<ChemistScheduleDto>();

                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    ChemistScheduleDto chemistScheduleDto = new ChemistScheduleDto();

                    //Get Chemist Schedule plan
                    var chemistSchedulePlanResponse = await _queryProcessor.ProcessQueryAsync<IGetChemistSchedulePlanQuery, IGetChemistSchedulePlanQueryResponse>(new GetChemistSchedulePlanQuery
                    {
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        ChemistId = userInfo.UserId,
                        date = DateTime.Now,
                        DayFilter = false
                    });

                    chemistScheduleDto.Schedule = chemistSchedulePlanResponse.ChemistSchedulePlans.GroupBy(x => x.Day).Select(s => new Days
                    {

                        Day = s.Key,
                        Times = s.Select(t => new Time
                        {
                            StartTime = t.StartTime.ToString(),
                            EndTime = t.EndTime.ToString()
                        }).ToList()

                    }).ToList();

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = chemistScheduleDto;

                    #region Send push notification

                    var userDeviceResponse = await _queryProcessor.ProcessQueryAsync<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>(new GetUserDeviceByChemistIdQuery
                    {
                        ChemistId = userInfo.UserId
                    });

                    string deviceToken = userDeviceResponse.UserDevice.FireBaseDeviceToken;

                    await _notificationService.SendNotification(new SendNotificationDto
                    {
                        NotificationId = Guid.NewGuid(),
                        UserId = userInfo.UserId,
                        Reciever = userInfo.UserId.ToString(),
                        NotificationType = NotificationTypes.MobilePush,
                        Title = "Chemist Schedule",
                        Message = "Chemist Schedule",
                        TitleAr = "Chemist Schedule",
                        MessageAr = "Chemist Schedule",
                        DeviceToken = deviceToken,
                        SystemNotificationType = SystemNotificationTypes.Chemist,
                        Culture = GetCultureName(),
                        ChemistScheduleDto = apiResponse,
                        ClickAction = "ChemistSchedule"
                    });

                    #endregion

                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    apiResponse.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("ChemistScheduleById")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<ChemistSchedulePlanDto>), 200)]
        public async Task<IActionResult> ChemistScheduleById([FromQuery] Guid chemistId)
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<List<ChemistVisitsScheduleDto>>();

                if (ModelState.IsValid)
                {
                    //Get Chemist Schedule plan
                    var chemistSchedulePlanResponse = await _queryProcessor.ProcessQueryAsync<IGetChemistVisitsScheduleQuery, IGetChemistVisitsScheduleQueryResponse>(new GetChemistVisitsScheduleQuery
                    {
                        ChemistId = chemistId,
                        date = DateTime.Now,
                        CultureName = GetCultureName()
                    });

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = chemistSchedulePlanResponse.ChemistVisitsScheduleDtos;

                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    apiResponse.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("ChemistGPSTracking")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> ChemistGPSTracking()
        {
            try
            {
                RecurringJob.AddOrUpdate(() => ChemistTrackingBackGroundService(), "*/20 * * * * *");

                var apiResponse = new HomeVisitsWebApiResponse<bool>();

                //if (ModelState.IsValid)
                //{
                //    await ChemistTrackingBackGroundService(userInfo);

                //    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                //    apiResponse.Response = true;
                //    return Ok(apiResponse);
                //}
                //else
                //{
                //    apiResponse.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                //    apiResponse.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                //    return BadRequest(apiResponse);
                //}

                apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                apiResponse.Response = true;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task ChemistTrackingBackGroundService()
        {
            var currentTime = DateTime.Now.TimeOfDay;

            //Get All Chemists
            var chemistsResponse = await _queryProcessor.ProcessQueryAsync<IGetAllChemistsQuery, IGetAllChemistsQueryResponse>(new GetAllChemistsQuery());

            if (chemistsResponse.Chemists.Count > 0)
            {
                for (int i = 0; i < chemistsResponse.Chemists.Count; i++)
                {
                    //Get Chemist Schedule plan
                    var chemistSchedulePlanResponse = await _queryProcessor.ProcessQueryAsync<IGetChemistSchedulePlanQuery, IGetChemistSchedulePlanQueryResponse>(new GetChemistSchedulePlanQuery
                    {
                        ClientId = chemistsResponse.Chemists[i].ClientId,
                        ChemistId = chemistsResponse.Chemists[i].ChemistId,
                        date = DateTime.Now,
                        DayFilter = true
                    });

                    //Check if no record saved in chemist tracking log for last 20 mins
                    if (chemistSchedulePlanResponse.ChemistSchedulePlans.Count > 0)
                    {
                        var result = chemistSchedulePlanResponse.ChemistSchedulePlans.Any(x => x.StartTime < currentTime && currentTime < x.EndTime);
                        if (result)
                        {
                            //apiResponse.Message = "match found";
                            var chemistTrackResponse = await _queryProcessor.ProcessQueryAsync<IGetChemistTrackingLogQuery, IGetChemistTrackingLogQueryResponse>(new GetChemistTrackingLogQuery
                            {
                                ChemistId = chemistsResponse.Chemists[i].ChemistId
                            });

                            if (chemistTrackResponse.ChemistTrackingLog == null)
                            {
                                #region Send push notification

                                var userDeviceResponse = await _queryProcessor.ProcessQueryAsync<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>(new GetUserDeviceByChemistIdQuery
                                {
                                    ChemistId = chemistsResponse.Chemists[i].ChemistId
                                });

                                string deviceToken = userDeviceResponse?.UserDevice?.FireBaseDeviceToken;

                                await _notificationService.SendNotification(new SendNotificationDto
                                {
                                    NotificationId = Guid.NewGuid(),
                                    UserId = chemistsResponse.Chemists[i].ChemistId,
                                    Reciever = chemistsResponse.Chemists[i].ChemistId.ToString(),
                                    NotificationType = NotificationTypes.MobilePush,
                                    Title = "Chemist Tracking",
                                    Message = "You’re considered as absent as you’re closing GPS",
                                    TitleAr = "Chemist Tracking",
                                    MessageAr = "انت الان تعتبر غائب عن العمل لعدم تشغيل GPS",
                                    DeviceToken = deviceToken,
                                    SystemNotificationType = SystemNotificationTypes.ChemistTracking,
                                    Culture = GetCultureName(),
                                    ClickAction = "com.smartwave.chemist.feature.home"
                                });

                                #endregion
                            }
                            else
                            {
                                if (chemistTrackResponse.ChemistTrackingLog.CreationDate.AddMinutes(20) < DateTime.Now)
                                {
                                    #region Send push notification

                                    var userDeviceResponse = await _queryProcessor.ProcessQueryAsync<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>(new GetUserDeviceByChemistIdQuery
                                    {
                                        ChemistId = chemistsResponse.Chemists[i].ChemistId
                                    });

                                    if (userDeviceResponse != null && userDeviceResponse.UserDevice != null)
                                    {
                                        string deviceToken = userDeviceResponse.UserDevice.FireBaseDeviceToken;

                                        await _notificationService.SendNotification(new SendNotificationDto
                                        {
                                            NotificationId = Guid.NewGuid(),
                                            UserId = chemistsResponse.Chemists[i].ChemistId,
                                            Reciever = chemistsResponse.Chemists[i].ChemistId.ToString(),
                                            NotificationType = NotificationTypes.MobilePush,
                                            Title = "Chemist Tracking",
                                            Message = "You’re considered as absent as you’re closing GPS",
                                            TitleAr = "Chemist Tracking",
                                            MessageAr = "انت الان تعتبر غائب عن العمل لعدم تشغيل GPS",
                                            DeviceToken = deviceToken,
                                            SystemNotificationType = SystemNotificationTypes.ChemistTracking,
                                            Culture = GetCultureName(),
                                            ClickAction = "com.smartwave.chemist.feature.home"
                                        });
                                    }
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
