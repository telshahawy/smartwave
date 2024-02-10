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
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.Domain.Enums;
using Cronos;
using Hangfire;
using SW.HomeVisits.Application.Abstract.AssignAndOptimizeVisits;
using SW.HomeVisits.Application.Models;
using System.Net.Http.Headers;

namespace SW.HomeVisits.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICultureService _cultureService;
        private readonly INotitificationService _notificationService;
        private readonly IAssignAndOptimizeVisitsService _assignAndOptimizeVisitsService;

        public VisitController(ICommandBus commandBus, IQueryProcessor queryProcessor, IAssignAndOptimizeVisitsService assignAndOptimizeVisitsService, ICultureService cultureService, INotitificationService notificationService)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _cultureService = cultureService;
            _notificationService = notificationService;
            _assignAndOptimizeVisitsService = assignAndOptimizeVisitsService;
        }

        [HttpGet("SearchPatients")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<PatientsDto>>), 200)]
        public async Task<IActionResult> SearchPatients([FromQuery] SearchPatientModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<List<PatientsDto>>();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }
                    var response = await _queryProcessor.ProcessQueryAsync<ISearchPatientsQuery, ISearchPatientsQueryResponse>(new SearchPatientsQuery
                    {
                        PhoneNumber = model.PhoneNumber,
                        CultureName = GetCultureName(),
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    if (!response.Patients.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.Patients;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("PatientsNotFound", GetCulture());
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.Patients;
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

        [HttpGet("GetPatientVisitsByPatientId")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<PatientVistisDto>>), 200)]
        public async Task<IActionResult> GetPatientVisitsByPatientId([FromQuery] PatientVisitsModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<List<PatientVistisDto>>();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }
                    var response = await _queryProcessor.ProcessQueryAsync<IGetPatientVisitsByPatientIdQuery, IGetPatientVisitsByPatientIdQueryResponse>(new GetPatientVisitsByPatientIdQuery
                    {
                        PatientId = Guid.Parse(model.PatientId),
                        CultureName = GetCultureName()
                    });
                    if (!response.PatientVistis.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.PatientVistis;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("PatientVisitsNotFound", GetCulture());
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.PatientVistis;
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
        //#region OptimizingHangFire
        //[HttpGet]
        //public async Task<IActionResult> AreaOptimizing()
        //{
        //    //backgroundJobs.Enqueue(() => Console.WriteLine(msg));
        //    RecurringJob.AddOrUpdate(
        //    "Area Optimizing",
        //    () => OptimizeFuncAsync(),
        //    Cron.MinuteInterval(3)
        //    );
        //    return Ok();
        //}


        //private async Task OptimizeFuncAsync()
        //{
        //    var userInfo = GetCurrentUserId();

        //    var result = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
        //    {
        //        ClientId = userInfo.ClientId.GetValueOrDefault()
        //    });
        //    var geoZonesResult = await _queryProcessor.ProcessQueryAsync<ISearchGeoZonesQuery, ISearchGeoZonesQueryResponse>(new SearchGeoZonesQuery
        //    {
        //        ClientId = userInfo.ClientId.GetValueOrDefault()
        //    });
        //    var isOptimizedZoneBefore = result.SystemParameters.IsOptimizezonebefore;
        //    if (isOptimizedZoneBefore == true)
        //    {
        //        var optimizeZoneBefore = result.SystemParameters.OptimizezonebeforeInMin;


        //        CronExpression expression = CronExpression.Parse("* * * * *");

        //        DateTime? nextUtc = expression.GetNextOccurrence(DateTime.UtcNow);
        //        foreach (var item in geoZonesResult.GeoZones)
        //        {
        //            var timeZoneFrameId = item.TimeZoneFrames.FirstOrDefault().TimeZoneFrameId;
        //            var timeZoneResult = await _queryProcessor.ProcessQueryAsync<IGetGeoZoneForEditQuery, IGetTimeZonesForGeoZoneQueryResponse>(new GetGeoZoneForEditQuery
        //            {
        //                GeoZoneId = item.GeoZoneId
        //            });

        //            var startZoneHr = timeZoneResult.TimeZonesForGeoZone.FirstOrDefault().StartTime.Hours;
        //            var startZoneMin = timeZoneResult.TimeZonesForGeoZone.FirstOrDefault().StartTime.Minutes;

        //            //DateTime.Now.Subtract(new TimeSpan(0, -(int)optimizeZoneBefore, 0));

        //            BackgroundJob.Schedule(() => this.AssignChemistOptimization(item.GeoZoneId), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startZoneHr, startZoneMin - (int)optimizeZoneBefore, 0));
        //        }

        //    }

        //}


        //private async Task AssignChemistOptimization(Guid geoZoneId)
        //{
        //    var userInfo = GetCurrentUserId();

        //    var visitsResponse = await _queryProcessor.ProcessQueryAsync<IGetAvailableVisitsInAreaQuery, IGetAvailableVisitsInAreaQueryResponse>(new GetAvailableVisitsInAreaQuery
        //    {
        //        GeoZoneId = geoZoneId,
        //        date = DateTime.Now,
        //        ClientId = userInfo.ClientId.Value,
        //        CultureName = GetCultureName()
        //    });
        //    foreach (var item in visitsResponse.AvailableVisits)
        //    {
        //        var assignModel = new AssignChemistModel
        //        {
        //            TimeZoneGeoZoneId = geoZoneId,
        //            VisitDate = DateTime.Now,
        //            PatientId = visitsResponse.AvailableVisits.FirstOrDefault().PatientId,
        //            VisitTypeId = visitsResponse.AvailableVisits.FirstOrDefault().VisitType,
        //            RelativeAgeSegmentId = visitsResponse.AvailableVisits.FirstOrDefault().AgeSegmentId
        //        };

        //        var chemistId = AssignChemist(assignModel, userInfo);

        //        if (chemistId == Guid.Empty)
        //        {
        //            //no assigning
        //        }
        //        else
        //        {
        //            //assign visit to that chemist
        //            var assignChemistCommand = new CreateVisitStatusCommand
        //            {
        //                VisitId = Guid.NewGuid(),
        //                ChemistId = chemistId,
        //                VisitActionTypeId = (int)VisitActionTypes.ReassignChemist,
        //                TimeZoneGeoZoneId = assignModel.TimeZoneGeoZoneId,
        //                VisitDate = visitsResponse.AvailableVisits.FirstOrDefault().VisitDate
        //            };

        //            await _commandBus.SendAsync((ICreateVisitStatusCommand)assignChemistCommand);
        //        }

        //    }

        //}
        //#endregion

        [HttpGet("GetAvailableVisitsInArea")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<AvailableVisitsInAreaDto>>), 200)]
        public async Task<IActionResult> GetAvailableVisitsInArea([FromQuery] GetAvailableVisitsInAreaModel model)
        {
            var apiResponse = new HomeVisitsWebApiResponse<List<AvailableVisitsInAreaDto>>();
            try
            {
                var userInfo = GetCurrentUserId();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }

                    if (model.Date < DateTime.Now.Date)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا يمكن البحث بتاريخ سابق" : "cannot search with old date";
                        return BadRequest(apiResponse);
                    }

                    try
                    {
                        var response = await _queryProcessor.ProcessQueryAsync<IGetAvailableVisitsInAreaQuery, IGetAvailableVisitsInAreaQueryResponse>(new GetAvailableVisitsInAreaQuery
                        {
                            GeoZoneId = model.GeoZoneId,
                            date = model.Date,
                            ClientId = userInfo.ClientId.Value,
                            CultureName = GetCultureName()
                        });

                        if (!response.AvailableVisits.Any())
                        {
                            apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                            apiResponse.Response = null;
                            apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا يوجد فترات  فى المنطقة او التاريخ" : "No available slots in this area or date";
                        }
                        else
                        {
                            apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                            apiResponse.Response = response.AvailableVisits.Where(x => x.AvalailableVisits > 0).ToList();
                        }

                        return Ok(apiResponse);
                    }
                    catch (Exception ex)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Response = null;
                        apiResponse.Message = ex.Message;
                        return BadRequest(apiResponse);
                    }
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
                apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                apiResponse.Response = null;
                apiResponse.Message = ex.Message;
                return BadRequest(apiResponse);
            }
        }

        //Patient Confirm,Cancel
        [HttpPost("SendPatientAction")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<VisitDetailsDto>), 200)]
        public async Task<IActionResult> SendPatientAction([FromBody] CreateVisitStatusByPatientCommand model)
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<VisitDetailsDto>();

                if (ModelState.IsValid)
                {
                    if (model.VisitId == Guid.Empty)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = "No VisitId Sent!";
                        return BadRequest(apiResponse);
                    }

                    await _commandBus.SendAsync((ICreateVisitStatusByPatientCommand)model);
                    var response = await _queryProcessor.ProcessQueryAsync<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>(new GetVisitDetailsQuery
                    {
                        VisitId = model.VisitId,
                        CultureName = GetCultureName()
                    });

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.VisitDetails;
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

                    if (model.VisitActionTypeId == (int)VisitActionTypes.Confirmed)
                    {
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitConfirmedSuccessfully", GetCulture());
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
                    }
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

        [HttpGet("GetAvailableVisitsInAreaWeb")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<AvailableVisitsInAreaDto>>), 200)]
        public async Task<IActionResult> GetAvailableVisitsInAreaWeb([FromQuery] GetAvailableVisitsInAreaModel model)
        {
            var response1 = new HomeVisitsWebApiResponse<bool>();
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<List<AvailableVisitsInAreaDto>>();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }

                    if (model.Date < DateTime.Now.Date)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا يمكن البحث بتاريخ سابق" : "cannot search with old date";
                        return BadRequest(apiResponse);
                    }

                    try
                    {
                        var response = await _queryProcessor.ProcessQueryAsync<IGetAvailableVisitsInAreaQuery, IGetAvailableVisitsInAreaQueryResponse>(new GetAvailableVisitsInAreaQuery
                        {
                            GeoZoneId = model.GeoZoneId,
                            date = model.Date,
                            ClientId = userInfo.ClientId.Value,
                            CultureName = GetCultureName()
                        });

                        if (!response.AvailableVisits.Any())
                        {
                            apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                            apiResponse.Response = response.AvailableVisits;
                            apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا يوجد فترات  فى المنطقة او التاريخ" : "No available slots in this area or date";
                        }

                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.AvailableVisits.Where(x => x.AvalailableVisits > 0).ToList();
                        return Ok(apiResponse);
                    }
                    catch (Exception ex)
                    {
                        response1.Response = false;
                        response1.ResponseCode = WebApiResponseCodes.Failer;
                        return BadRequest(response1);
                    }
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
                //  throw ex;
                response1.Response = false;
                response1.ResponseCode = WebApiResponseCodes.Failer;
                return BadRequest(response1);


            }
        }

        [HttpGet("GetAvailableVisitsForChemist")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<AvailableVisitsInAreaDto>>), 200)]
        public async Task<IActionResult> GetAvailableVisitsForChemist([FromQuery] GetAvailableVisitsForChemistModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<List<AvailableVisitsInAreaDto>>();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }
                    if (model.Date < DateTime.Now.Date)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا يمكن البحث بتاريخ سابق" : "cannot search with old date";
                        return BadRequest(apiResponse);
                    }
                    var response = await _queryProcessor.ProcessQueryAsync<IGetAvailableVisitsForChemistQuery, IGetAvailableVisitsInAreaQueryResponse>(new GetAvailableVisitsForChemistQuery
                    {
                        GeoZoneId = model.GeoZoneId,
                        Date = model.Date,
                        ClientId = userInfo.ClientId.Value,
                        CultureName = GetCultureName(),
                        ChemistId = model.ChemistId
                    });
                    if (!response.AvailableVisits.Any())
                    {
                        apiResponse.Response = null;
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا يوجد فترات  فى المنطقة او التاريخ" : "No available slots in this area or date";
                    }

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.AvailableVisits.Where(x => x.AvalailableVisits > 0).ToList();
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

        [HttpGet("GetVisitDetailsByVisitId")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<VisitDetailsDto>), 200)]
        public async Task<IActionResult> GetVisitDetailsByVisitId([FromQuery] VisitDetailsModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<VisitDetailsDto>();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }

                    var response = await _queryProcessor.ProcessQueryAsync<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>(new GetVisitDetailsQuery
                    {
                        VisitId = Guid.Parse(model.VisitId),
                        CultureName = GetCultureName()
                    });

                    if (response.VisitDetails == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.VisitDetails;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitDetailsNotFound", GetCulture());
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

        [HttpPost("CreateLostVisitTime")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> CreateLostVisitTime([FromBody] CreateLostVisitTimeCommand model)
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<bool>();
                var userInfo = GetCurrentUserId();

                if (ModelState.IsValid)
                {
                    await _commandBus.SendAsync((ICreateLostVisitTimeCommand)model);
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = true;
                    apiResponse.Message = Resources.Resource.ResourceManager.GetString("LostTimeSavedSuccess", GetCulture());
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

        [HttpGet("GetAllLostVisitTimes")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<LostVisitTimesDto>>), 200)]
        public async Task<IActionResult> LostVisitTimes()
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<List<LostVisitTimesDto>>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetAllLostVisitTimesQuery, IGetAllLostVisitTimesQueryResponse>(new GetAllLostVisitTimesQuery());
                    if (!response.lostVisitTimes.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.lostVisitTimes;
                        apiResponse.Message = "No Lost Visits found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.lostVisitTimes;
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

        [HttpPost("AddVisitByChemistApp")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<VisitDetailsDto>), 200)]
        public async Task<IActionResult> AddVisitByChemistApp([FromBody] AddVisitModel model)
        {
            var apiResponse = new HomeVisitsWebApiResponse<VisitDetailsDto>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    Guid timeZoneGeoZoneId = model.TimeZoneGeoZoneId;
                    Guid? geoZoneId;
                    if (model.ChemistId == null)
                    {
                        if (!string.IsNullOrEmpty(model.VisitTime))
                        {
                            var patient = await _queryProcessor.ProcessQueryAsync<IGetPatientByPatientIdQuery, IGetPatientByPatientIdQueryResponse>(new GetPatientByPatientIdQuery
                            {
                                ClientId = userInfo.ClientId.GetValueOrDefault(),
                                PatientId = model.PatientId
                            });
                            if (patient == null)
                            {
                                throw new Exception("Patient not found");
                            }
                            geoZoneId = patient.Patient.PatientAddresses.SingleOrDefault(x => x.PatientAddressId == model.PatientAddressId)?.GeoZoneId;
                            if (geoZoneId == null)
                            {
                                throw new Exception("Patient Address not found");
                            }

                            if (model.VisitTime != null)
                            {
                                var ZoneAvailability = await _queryProcessor.ProcessQueryAsync<IGetAvailableVisitsInAreaQuery, IGetAvailableVisitsInAreaQueryResponse>(new GetAvailableVisitsInAreaQuery
                                {
                                    ClientId = userInfo.ClientId.GetValueOrDefault(),
                                    date = model.VisitDate,
                                    GeoZoneId = geoZoneId.GetValueOrDefault(),
                                    CultureName = CultureNames.en
                                });
                                var visitime = TimeSpan.Parse(model.VisitTime);
                                var timeZone = ZoneAvailability.AvailableVisits.Where(x => x.StartTimeSpan <= visitime && x.EndTimeSpan >= visitime).FirstOrDefault()?.TimeZoneFrameGeoZoneId;
                                if (timeZone == null)
                                {
                                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                    apiResponse.Message = "Please choose time in the given time slot ranges";
                                    return BadRequest(apiResponse);
                                }
                                timeZoneGeoZoneId = timeZone.GetValueOrDefault();
                            }
                        }

                        #region AssignChemist
                        //var assignModel = new AssignChemistModel
                        //{
                        //    TimeZoneGeoZoneId = timeZoneGeoZoneId,
                        //    VisitDate = model.VisitDate,
                        //    PatientId = model.PatientId,
                        //    VisitTypeId = model.VisitTypeId,
                        //    RelativeAgeSegmentId = model.RelativeAgeSegmentId
                        //};
                        //if (!string.IsNullOrWhiteSpace(model.VisitTime))
                        //{
                        //    assignModel.VisitTime = TimeSpan.Parse(model.VisitTime);
                        //}
                        //var chemistId = AssignChemist(assignModel, userInfo);

                        //if (chemistId == Guid.Empty)
                        //{
                        //    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        //    apiResponse.Message = "No Available Chemists";
                        //    return Ok(apiResponse);
                        //}
                        //else
                        //{
                        //    model.ChemistId = chemistId;
                        //} 
                        #endregion
                    }

                    var addVisitCommand = new AddVisitCommand
                    {
                        VisitId = Guid.NewGuid(),
                        VisitTypeId = model.VisitTypeId,
                        VisitDate = model.VisitDate,
                        PatientId = model.PatientId,
                        PatientAddressId = model.PatientAddressId,
                        ChemistId = model.ChemistId,
                        CreatedBy = userInfo.UserId,
                        RelativeAgeSegmentId = model.RelativeAgeSegmentId,
                        TimeZoneGeoZoneId = timeZoneGeoZoneId,
                        PlannedNoOfPatients = model.PlannedNoOfPatients,
                        RequiredTests = model.RequiredTests,
                        Comments = model.Comments,
                        Longitude = model.Longitude,
                        Latitude = model.Latitude,
                        DeviceSerialNumber = model.DeviceSerialNumber,
                        MobileBatteryPercentage = model.MobileBatteryPercentage,
                        SelectBy = (model.ChemistId == null ? (int)VisitSelectBy.Time : (int)VisitSelectBy.Chemist),
                        VisitTime = model.VisitTime,
                        IamNotSure = model.IamNotSure,
                        RelativeDateOfBirth = model.RelativeDateOfBirth,
                        Attachments = model.Atachments.Select(p => new CreateVisitAttachmentsDto
                        {
                            AttachmentId = Guid.NewGuid(),
                            FileName = p.FileName,
                            FilePath = p.FilePath,
                            CreatedDate = DateTime.Now,
                            CreatedBy = 0
                        }).ToList()
                    };

                    await _commandBus.SendAsync((IAddVisitCommand)addVisitCommand);

                    #region Visit details

                    var response = await _queryProcessor.ProcessQueryAsync<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>(new GetVisitDetailsQuery
                    {
                        VisitId = addVisitCommand.VisitId,
                        CultureName = GetCultureName()
                    });
                    if (response.VisitDetails == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.VisitDetails;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitDetailsNotFound", GetCulture());
                    }
                    #endregion

                    #region Assign And Optimize Visits
                    await _assignAndOptimizeVisitsService.AssignAndOptimizeVisitsInAddVisitsAsync(response.VisitDetails, timeZoneGeoZoneId, model.VisitDate, userInfo.ClientId.GetValueOrDefault());
                    #endregion

                    #region Send push notification
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
                    //await SendNotification(response, receivers, "is assigned to you", "تم إضافة هذا الحجز لك");

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
                            UserId = item,//userInfo.UserId
                            Reciever = item.ToString(),
                            NotificationType = NotificationTypes.MobilePush,
                            Title = string.Format("Home Visit # {0}", response.VisitDetails.VisitCode),
                            Message = string.Format("Home Visit # {0} {1}", response.VisitDetails.VisitCode, "is assigned to you"),
                            TitleAr = string.Format("الحجز رقم {0}", response.VisitDetails.VisitCode),
                            MessageAr = string.Format("الحجز رقم {0} {1}", response.VisitDetails.VisitCode, "تم إضافة هذا الحجز لك"),
                            DeviceToken = deviceToken,
                            VisitId = response.VisitDetails.VisitId,
                            SystemNotificationType = SystemNotificationTypes.Visit,
                            Culture = GetCultureName(),
                            ClickAction = "com.smartwave.chemist.feature.visit.ActivityVisitDetails"
                        });
                    }
                    #endregion

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.VisitDetails;
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

        [HttpPost("AddVisitByPatientApp")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<VisitDetailsDto>), 200)]
        public async Task<IActionResult> AddVisitByPatientApp([FromForm] AddVisitByPatientAppModel model)
        {
            var apiResponse = new HomeVisitsWebApiResponse<VisitDetailsDto>();
            try
            {
                if (ModelState.IsValid)
                {
                    Guid timeZoneGeoZoneId = model.TimeZoneGeoZoneId;
                    Guid? geoZoneId;
                    if (model.ChemistId == null && !string.IsNullOrEmpty(model.VisitTime))
                    {
                        var patient = await _queryProcessor.ProcessQueryAsync<IGetPatientByPatientIdQuery, IGetPatientByPatientIdQueryResponse>(new GetPatientByPatientIdQuery
                        {
                            ClientId = model.ClientId.GetValueOrDefault(),
                            PatientId = model.PatientId
                        });
                        if (patient == null)
                        {
                            throw new Exception("Patient not found");
                        }

                        geoZoneId = patient.Patient.PatientAddresses.SingleOrDefault(x => x.PatientAddressId == model.PatientAddressId)?.GeoZoneId;
                        if (geoZoneId == null)
                        {
                            throw new Exception("Patient Address not found");
                        }

                        if (model.VisitTime != null)
                        {
                            var ZoneAvailability = await _queryProcessor.ProcessQueryAsync<IGetAvailableVisitsInAreaQuery, IGetAvailableVisitsInAreaQueryResponse>(new GetAvailableVisitsInAreaQuery
                            {
                                ClientId = model.ClientId.GetValueOrDefault(),
                                date = model.VisitDate,
                                GeoZoneId = geoZoneId.GetValueOrDefault(),
                                CultureName = CultureNames.en
                            });
                            var visitime = TimeSpan.Parse(model.VisitTime);
                            var timeZone = ZoneAvailability.AvailableVisits.FirstOrDefault(x => x.StartTimeSpan <= visitime && x.EndTimeSpan >= visitime)?.TimeZoneFrameGeoZoneId;
                            if (timeZone == null)
                            {
                                apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                apiResponse.Message = "Please choose time in the given time slot ranges";
                                return BadRequest(apiResponse);
                            }
                            timeZoneGeoZoneId = timeZone.GetValueOrDefault();
                        }
                    }

                    List<CreateVisitAttachmentsDto> createVisitAttachmentsDtos = new List<CreateVisitAttachmentsDto>();
                    if (model.Atachments != null)
                    {
                        createVisitAttachmentsDtos = SaveVisitAttachment(model.Atachments);
                    }
                    else if (Request.Form.Files != null)
                    {
                        createVisitAttachmentsDtos = SaveVisitAttachment(Request.Form.Files.ToList());
                    }

                    var addVisitCommand = new AddVisitByPatientCommand
                    {
                        VisitId = Guid.NewGuid(),
                        VisitTypeId = model.VisitTypeId,
                        VisitDate = model.VisitDate,
                        PatientId = model.PatientId,
                        PatientAddressId = model.PatientAddressId,
                        ChemistId = model.ChemistId,
                        CreatedBy = model.PatientId,
                        TimeZoneGeoZoneId = timeZoneGeoZoneId,
                        PlannedNoOfPatients = model.PlannedNoOfPatients,
                        RequiredTests = model.RequiredTests,
                        Comments = model.Comments,
                        Longitude = model.Longitude,
                        Latitude = model.Latitude,
                        DeviceSerialNumber = model.DeviceSerialNumber,
                        MobileBatteryPercentage = model.MobileBatteryPercentage,
                        SelectBy = (model.ChemistId == null ? (int)VisitSelectBy.Time : (int)VisitSelectBy.Chemist),
                        VisitTime = model.VisitTime,
                        RelativeName = model.RelativeName,
                        RelativeGender = model.RelativeGender,
                        RelativePhoneNumber = model.RelativePhoneNumber,
                        RelativeDateOfBirth = model.RelativeDateOfBirth,
                        RelativeAgeSegmentId = model.RelativeAgeSegmentId,
                        Attachments = createVisitAttachmentsDtos
                    };

                    await _commandBus.SendAsync((IAddVisitByPatientCommand)addVisitCommand);

                    #region Visit details
                    var response = await _queryProcessor.ProcessQueryAsync<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>(new GetVisitDetailsQuery
                    {
                        VisitId = addVisitCommand.VisitId,
                        CultureName = GetCultureName()
                    });

                    if (response.VisitDetails == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.VisitDetails;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitDetailsNotFound", GetCulture());
                    }
                    #endregion

                    #region Assign And Optimize Visits
                    await _assignAndOptimizeVisitsService.AssignAndOptimizeVisitsInAddVisitsAsync(response.VisitDetails, timeZoneGeoZoneId, model.VisitDate, model.ClientId.GetValueOrDefault());
                    #endregion

                    #region Send push notification
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
                    // await SendNotification(response, receivers, "is successfully reserved", "تم إضافة الحجز بنجاح");

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
                            Title = string.Format("Home Visit # {0}", response.VisitDetails.VisitNo),
                            Message = string.Format("Home Visit # {0} {1}", response.VisitDetails.VisitNo, "is assigned to you"),
                            TitleAr = string.Format("الحجز رقم {0}", response.VisitDetails.VisitNo),
                            MessageAr = string.Format("الحجز رقم {0} {1}", response.VisitDetails.VisitNo, "تم إضافة هذا الحجز لك"),
                            DeviceToken = deviceToken,
                            VisitId = response.VisitDetails.VisitId,
                            SystemNotificationType = SystemNotificationTypes.Visit,
                            Culture = GetCultureName(),
                            ClickAction = "com.smartwave.chemist.feature.visit.ActivityVisitDetails"
                        });
                    }
                    #endregion

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.VisitDetails;
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
                apiResponse.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                apiResponse.Message = ex.Message;
                return BadRequest(apiResponse);
            }
        }

        private List<CreateVisitAttachmentsDto> SaveVisitAttachment(List<IFormFile> attachments)
        {
            List<CreateVisitAttachmentsDto> createVisitAttachmentsDtos = new List<CreateVisitAttachmentsDto>();
            foreach (var item in attachments)
            {
                var file = item;
                var folderName = Path.Combine("Uploads", "Visits");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileExtention = fileName.Split('.').Last().ToLower();

                    var fileNameToSave = Guid.NewGuid().ToString() + "." + fileExtention;
                    var fullPath = Path.Combine(pathToSave, fileNameToSave);
                    var filePath = Path.Combine(folderName, fileNameToSave);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    createVisitAttachmentsDtos.Add(new CreateVisitAttachmentsDto
                    {
                        AttachmentId = Guid.NewGuid(),
                        FileName = fileName,
                        FilePath = filePath,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 0
                    });
                }
            }
            return createVisitAttachmentsDtos;
        }

        [HttpGet("GetSecondVisitTimeZoneAndDate")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SecondVisitTimeZoneVisitDateDto>), 200)]
        public async Task<IActionResult> GetSecondVisitTimeZoneAndDate([FromQuery] SecondVisitAfterFirstVisitTimeModel secondVisitAfterFirstVisitTimeModel)
        {
            var apiResponse = new HomeVisitsWebApiResponse<SecondVisitTimeZoneVisitDateDto>();
            try
            {
                if (ModelState.IsValid)
                {
                    string responseMessage = null;
                    var originVisitDetials = await _queryProcessor.ProcessQueryAsync<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>(new GetVisitDetailsQuery
                    {
                        VisitId = secondVisitAfterFirstVisitTimeModel.OriginVisitId,
                        CultureName = GetCultureName()
                    });
                    if (originVisitDetials == null || originVisitDetials.VisitDetails == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Response = null;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitDetailsNotFound", GetCulture());
                        return Ok(apiResponse);
                    }
                    var timeZoneFrames = await _queryProcessor.ProcessQueryAsync<IGetTimeZoneFramesQuery, IGetTimeZoneFramesQueryResponse>(new GetTimeZoneFramesQuery
                    {
                        GeoZoneId = originVisitDetials.VisitDetails.GeoZoneId,
                        CultureName = GetCultureName(),
                        IsDeleted = false
                    });

                    var userInfo = GetCurrentUserId();
                    var visitStatus = originVisitDetials.VisitDetails.VisitStatuses.FirstOrDefault(p => p.VisitStatusTypeId == (int)VisitActionTypes.Done);
                    if (visitStatus == null)
                    {
                        var originalVisitDate = originVisitDetials.VisitDetails.VisitDate;
                        if (originVisitDetials.VisitDetails.ChemistId.HasValue)
                        {
                            var chemistVisitsOrder = await _queryProcessor.ProcessQueryAsync<ISearchChemistVisitsOrderQuery, ISearchChemistVisitsOrderQueryResponse>(new SearchChemistVisitsOrderQuery
                            {
                                ClientId = userInfo.ClientId,
                                VisitDate = originVisitDetials.VisitDetails.VisitDate,
                                ChemistId = originVisitDetials.VisitDetails.ChemistId,
                                TimeZoneFrameId = originVisitDetials.VisitDetails.TimeZoneGeoZoneId,
                            });
                            if (chemistVisitsOrder == null || chemistVisitsOrder.Visits == null || chemistVisitsOrder.Visits.Count == 0)
                            {
                                var secondVisitTimeZoneVisitDateDto = GetSecondVisitTimeZoneOnOrigVisitNotOptimaize(timeZoneFrames.TimeZoneFramesDto, originVisitDetials.VisitDetails, secondVisitAfterFirstVisitTimeModel, out responseMessage);
                                if (secondVisitTimeZoneVisitDateDto == null)
                                {
                                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                    apiResponse.Message = responseMessage;
                                    return BadRequest(apiResponse);
                                }
                                else
                                    apiResponse.Response = secondVisitTimeZoneVisitDateDto;
                            }
                            else
                            {
                                var chemistVisitsOrderByOrder = chemistVisitsOrder.Visits.Where(p => p.VisitStatusTypeId != (int)VisitActionTypes.Cancelled || p.VisitStatusTypeId != (int)VisitActionTypes.Reject).OrderBy(p => p.VisitOrder).ToList();
                                var originVisitNumberInOrderIndex = chemistVisitsOrderByOrder.FindIndex(p => p.VisitId == originVisitDetials.VisitDetails.VisitId);
                                var systemParameters = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
                                {
                                    ClientId = userInfo.ClientId.GetValueOrDefault()
                                });
                                if (systemParameters == null || systemParameters.SystemParameters == null)
                                {
                                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                    apiResponse.Response = null;
                                    apiResponse.Message = Resources.Resource.ResourceManager.GetString("SystemParametersNotFound", GetCulture());
                                    return Ok(apiResponse);
                                }
                                //TODO Apply Logic when original visit is assign get its order and calculate it's visit time by num of visit * visit duaration + visit duration in traffic

                                var originalVisitTimeZoneIndex = timeZoneFrames.TimeZoneFramesDto.FindIndex(p => p.TimeZoneFrameId == originVisitDetials.VisitDetails.TimeZoneGeoZoneId);
                                var originalVisitStartTimeZone = timeZoneFrames.TimeZoneFramesDto[originalVisitTimeZoneIndex].StartTimeValue;
                                var originalVisitDateWithEndTimeZone = new DateTime(originalVisitDate.Year, originalVisitDate.Month, originalVisitDate.Day, originalVisitStartTimeZone.Hours, originalVisitStartTimeZone.Minutes, originalVisitStartTimeZone.Seconds);
                                var visitsDurationTimeInMinutes = (originVisitNumberInOrderIndex + 1) * systemParameters.SystemParameters.EstimatedVisitDurationInMin;
                                int visitsDuratuinTimeInSecond = 0;
                                for (int i = 0; i <= originVisitNumberInOrderIndex; i++)
                                {
                                    visitsDuratuinTimeInSecond += chemistVisitsOrderByOrder[i].DurationInTraffic;
                                }

                                DateTime secondVisitDate = originalVisitDateWithEndTimeZone.AddMinutes(visitsDurationTimeInMinutes).AddSeconds(visitsDuratuinTimeInSecond);
                                DateTime minSecondVisitDate = secondVisitDate.AddMinutes(secondVisitAfterFirstVisitTimeModel.MinMinutes);
                                DateTime maxSecondVisitDate = secondVisitDate.AddMinutes(secondVisitAfterFirstVisitTimeModel.MaxMinutes);
                                var secondVisitTimeZoneFrame = timeZoneFrames.TimeZoneFramesDto.FirstOrDefault(p => p.StartTimeValue <= minSecondVisitDate.TimeOfDay && p.EndTimeValue >= minSecondVisitDate.TimeOfDay);
                                if (secondVisitTimeZoneFrame == null || secondVisitTimeZoneFrame.BranchDispatch)
                                {
                                    secondVisitTimeZoneFrame = timeZoneFrames.TimeZoneFramesDto.FirstOrDefault(p => p.StartTimeValue <= maxSecondVisitDate.TimeOfDay && p.EndTimeValue >= maxSecondVisitDate.TimeOfDay);
                                    if (secondVisitTimeZoneFrame == null || secondVisitTimeZoneFrame.BranchDispatch)
                                    {
                                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("NoTimeZoneAvialableForSecondVisit", GetCulture());
                                        return BadRequest(apiResponse);
                                    }
                                    else
                                        secondVisitDate = maxSecondVisitDate;
                                }
                                else
                                    secondVisitDate = minSecondVisitDate;

                                apiResponse.Response = new SecondVisitTimeZoneVisitDateDto
                                {
                                    MaxMinutes = secondVisitAfterFirstVisitTimeModel.MaxMinutes,
                                    MinMinutes = secondVisitAfterFirstVisitTimeModel.MinMinutes,
                                    OriginVisitId = secondVisitAfterFirstVisitTimeModel.OriginVisitId,
                                    SecondVisitDate = secondVisitDate,
                                    SecondVisitTimeFrameId = secondVisitTimeZoneFrame.TimeZoneFrameId
                                };
                            }
                        }
                        else
                        {
                            var secondVisitTimeZoneVisitDateDto = GetSecondVisitTimeZoneOnOrigVisitNotOptimaize(timeZoneFrames.TimeZoneFramesDto, originVisitDetials.VisitDetails, secondVisitAfterFirstVisitTimeModel, out responseMessage);
                            if (secondVisitTimeZoneVisitDateDto == null)
                            {
                                apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                apiResponse.Message = responseMessage;
                                return BadRequest(apiResponse);
                            }
                            else
                                apiResponse.Response = secondVisitTimeZoneVisitDateDto;
                        }
                    }
                    else
                    {
                        var originalVisitDate = visitStatus.CreationDate;
                        var minSecondVisitTime = originalVisitDate.AddMinutes(secondVisitAfterFirstVisitTimeModel.MinMinutes);
                        var maxSecondVisitDate = originalVisitDate.AddMinutes(secondVisitAfterFirstVisitTimeModel.MaxMinutes);
                        DateTime secondVisitDate;
                        var secondVisitTimeZone = timeZoneFrames.TimeZoneFramesDto.FirstOrDefault(p => p.StartTimeValue <= minSecondVisitTime.TimeOfDay && p.EndTimeValue >= minSecondVisitTime.TimeOfDay);
                        if (secondVisitTimeZone == null || secondVisitTimeZone.BranchDispatch)
                        {
                            secondVisitTimeZone = timeZoneFrames.TimeZoneFramesDto.FirstOrDefault(p => p.StartTimeValue <= maxSecondVisitDate.TimeOfDay && p.EndTimeValue >= maxSecondVisitDate.TimeOfDay);
                            if (secondVisitTimeZone == null || secondVisitTimeZone.BranchDispatch)
                            {
                                apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                apiResponse.Response = null;
                                apiResponse.Message = Resources.Resource.ResourceManager.GetString("NoTimeZoneAvialableForSecondVisit", GetCulture());
                                return BadRequest(apiResponse);
                            }
                            else
                                secondVisitDate = maxSecondVisitDate;
                        }
                        else
                            secondVisitDate = minSecondVisitTime;

                        apiResponse.Response = new SecondVisitTimeZoneVisitDateDto
                        {
                            MaxMinutes = secondVisitAfterFirstVisitTimeModel.MaxMinutes,
                            MinMinutes = secondVisitAfterFirstVisitTimeModel.MinMinutes,
                            OriginVisitId = secondVisitAfterFirstVisitTimeModel.OriginVisitId,
                            SecondVisitDate = secondVisitDate,
                            SecondVisitTimeFrameId = secondVisitTimeZone.TimeZoneFrameId
                        };
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
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

        private SecondVisitTimeZoneVisitDateDto GetSecondVisitTimeZoneOnOrigVisitNotOptimaize(List<TimeZoneFramesDto> timeZoneFramesDto, VisitDetailsDto visitDetailsDto, SecondVisitAfterFirstVisitTimeModel secondVisitAfterFirstVisitTimeModel, out string message)
        {
            message = null;
            DateTime secondVisitDate;
            TimeZoneFramesDto secondVisitTimeZone;
            var originalVisitTimeZoneIndex = timeZoneFramesDto.FindIndex(p => p.TimeZoneFrameId == visitDetailsDto.TimeZoneGeoZoneId);
            if (originalVisitTimeZoneIndex == timeZoneFramesDto.Count - 1)
            {
                var originalVisitDate = visitDetailsDto.VisitDate;
                var originalVisitEndTimeZone = timeZoneFramesDto[originalVisitTimeZoneIndex].EndTimeValue;
                var originalVisitDateWithEndTimeZone = new DateTime(originalVisitDate.Year, originalVisitDate.Month, originalVisitDate.Day, originalVisitEndTimeZone.Hours, originalVisitEndTimeZone.Minutes, originalVisitEndTimeZone.Seconds);

                var minSecondVisitDate = originalVisitDateWithEndTimeZone.AddMinutes(secondVisitAfterFirstVisitTimeModel.MinMinutes);
                var maxSecondVisitDate = originalVisitDateWithEndTimeZone.AddMinutes(secondVisitAfterFirstVisitTimeModel.MaxMinutes);

                secondVisitTimeZone = timeZoneFramesDto.FirstOrDefault(p => p.StartTimeValue <= minSecondVisitDate.TimeOfDay && p.EndTimeValue >= minSecondVisitDate.TimeOfDay);
                if (secondVisitTimeZone == null || secondVisitTimeZone.BranchDispatch)
                {
                    secondVisitTimeZone = timeZoneFramesDto.FirstOrDefault(p => p.StartTimeValue <= maxSecondVisitDate.TimeOfDay && p.EndTimeValue >= maxSecondVisitDate.TimeOfDay);
                    if (secondVisitTimeZone == null || secondVisitTimeZone.BranchDispatch)
                    {
                        message = Resources.Resource.ResourceManager.GetString("NoTimeZoneAvialableForSecondVisit", GetCulture());
                        return null;
                    }
                    else
                        secondVisitDate = maxSecondVisitDate;
                }
                else
                    secondVisitDate = minSecondVisitDate;

                return new SecondVisitTimeZoneVisitDateDto
                {
                    MaxMinutes = secondVisitAfterFirstVisitTimeModel.MaxMinutes,
                    MinMinutes = secondVisitAfterFirstVisitTimeModel.MinMinutes,
                    OriginVisitId = secondVisitAfterFirstVisitTimeModel.OriginVisitId,
                    SecondVisitDate = secondVisitDate,
                    SecondVisitTimeFrameId = secondVisitTimeZone.TimeZoneFrameId
                };
            }
            else
            {
                secondVisitDate = visitDetailsDto.VisitDate;
                secondVisitTimeZone = timeZoneFramesDto[originalVisitTimeZoneIndex + 1];
                if (secondVisitTimeZone.BranchDispatch)
                {
                    message = Resources.Resource.ResourceManager.GetString("NoTimeZoneAvialableForSecondVisit", GetCulture());
                    return null;
                }
            }
            return new SecondVisitTimeZoneVisitDateDto
            {
                MaxMinutes = secondVisitAfterFirstVisitTimeModel.MaxMinutes,
                MinMinutes = secondVisitAfterFirstVisitTimeModel.MinMinutes,
                OriginVisitId = secondVisitAfterFirstVisitTimeModel.OriginVisitId,
                SecondVisitDate = secondVisitDate,
                SecondVisitTimeFrameId = secondVisitTimeZone.TimeZoneFrameId
            };
        }

        [HttpPost("AddSecondVisitByChemistApp")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<VisitDetailsDto>), 200)]
        public async Task<IActionResult> AddSecondVisitByChemistApp([FromBody] AddSecondVisitModel model)
        {
            var apiResponse = new HomeVisitsWebApiResponse<VisitDetailsDto>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    Guid timeZoneGeoZoneId = model.TimeZoneGeoZoneId;
                    if (model.ChemistId == null)
                    {

                        var originVisit = await _queryProcessor.ProcessQueryAsync<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>(new GetVisitDetailsQuery
                        {
                            VisitId = model.OriginVisitId,
                            CultureName = GetCultureName()
                        });
                        if (model.VisitTime != null)
                        {
                            var patient = await _queryProcessor.ProcessQueryAsync<IGetPatientByPatientIdQuery, IGetPatientByPatientIdQueryResponse>(new GetPatientByPatientIdQuery
                            {
                                ClientId = userInfo.ClientId.GetValueOrDefault(),
                                PatientId = originVisit.VisitDetails.PatientId
                            });
                            if (patient == null)
                            {
                                throw new Exception("Patient not found");
                            }
                            var geoZoneId = patient.Patient.PatientAddresses.SingleOrDefault(x => x.PatientAddressId == originVisit.VisitDetails.PatientAddressId)?.GeoZoneId;
                            if (geoZoneId == null)
                            {
                                throw new Exception("Patient Address not found");
                            }

                            if (model.VisitTime != null)
                            {
                                var ZoneAvailability = _queryProcessor.ProcessQueryAsync<IGetAvailableVisitsInAreaQuery, IGetAvailableVisitsInAreaQueryResponse>(new GetAvailableVisitsInAreaQuery
                                {
                                    ClientId = userInfo.ClientId.GetValueOrDefault(),
                                    date = model.VisitDate,
                                    GeoZoneId = geoZoneId.GetValueOrDefault(),
                                    CultureName = CultureNames.en
                                });
                                var timeZone = ZoneAvailability.Result.AvailableVisits.Where(x => x.StartTimeSpan <= TimeSpan.Parse(model.VisitTime) && x.EndTimeSpan >= TimeSpan.Parse(model.VisitTime)).FirstOrDefault()?.TimeZoneFrameGeoZoneId;
                                if (timeZone == null)
                                {
                                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                    apiResponse.Message = "Please choose time in the given time slot ranges";
                                    return BadRequest(apiResponse);
                                }
                                timeZoneGeoZoneId = timeZone.GetValueOrDefault();

                            }
                        }
                        var assignModel = new AssignChemistModel
                        {
                            TimeZoneGeoZoneId = timeZoneGeoZoneId,
                            VisitDate = model.VisitDate,
                            PatientId = originVisit.VisitDetails.PatientId,
                            VisitTypeId = originVisit.VisitDetails.VisitTypeId,
                            RelativeAgeSegmentId = originVisit.VisitDetails.RelativeAgeSegmentId
                        };
                        if (!string.IsNullOrWhiteSpace(model.VisitTime))
                        {
                            assignModel.VisitTime = TimeSpan.Parse(model.VisitTime);
                        }
                        var chemistId = AssignChemist(assignModel, userInfo);

                        if (chemistId == Guid.Empty)
                        {
                            apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                            apiResponse.Message = "No Available Chemists";
                            return Ok(apiResponse);
                        }
                        else
                        {
                            model.ChemistId = chemistId;
                        }
                    }

                    var addSecondVisitCommand = new AddSecondVisitCommand
                    {
                        VisitId = Guid.NewGuid(),
                        TimeZoneGeoZoneId = timeZoneGeoZoneId,
                        VisitDate = model.VisitDate,
                        RequiredTests = model.RequiredTests,
                        Comments = model.Comments,
                        OriginVisitId = model.OriginVisitId,
                        MaxMinutes = model.MaxMinutes,
                        MinMinutes = model.MinMinutes,
                        Longitude = model.Longitude,
                        Latitude = model.Latitude,
                        DeviceSerialNumber = model.DeviceSerialNumber,
                        MobileBatteryPercentage = model.MobileBatteryPercentage,
                        SecondVisitReason = model.SecondVisitReason,
                        ChemistId = model.ChemistId,
                        SelectBy = (model.ChemistId == null ? (int)VisitSelectBy.Time : (int)VisitSelectBy.Chemist),
                        CreatedBy = userInfo.UserId
                    };

                    await _commandBus.SendAsync((IAddSecondVisitCommand)addSecondVisitCommand);

                    #region Visit details

                    var response = await _queryProcessor.ProcessQueryAsync<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>(new GetVisitDetailsQuery
                    {
                        VisitId = addSecondVisitCommand.VisitId,
                        CultureName = GetCultureName()
                    });
                    if (response.VisitDetails == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.VisitDetails;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitDetailsNotFound", GetCulture());
                    }
                    #endregion

                    #region Assign And Optimize Visits
                    await _assignAndOptimizeVisitsService.AssignAndOptimizeVisitsInAddVisitsAsync(response.VisitDetails, timeZoneGeoZoneId, model.VisitDate, userInfo.ClientId.GetValueOrDefault());
                    #endregion
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
                    //await SendNotification(response, receivers, "is assigned to you", "تم إضافة هذا الحجز لك");
                    //#region Send push notification
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
                            Message = string.Format("Home Visit # {0} {1}", response.VisitDetails.VisitCode, "is assigned to you"),
                            TitleAr = string.Format("الحجز رقم {0}", response.VisitDetails.VisitCode),
                            MessageAr = string.Format("الحجز رقم {0} {1}", response.VisitDetails.VisitCode, "تم إضافة هذا الحجز لك"),
                            DeviceToken = deviceToken,
                            VisitId = response.VisitDetails.VisitId,
                            SystemNotificationType = SystemNotificationTypes.Visit,
                            Culture = GetCultureName(),
                            ClickAction = "com.smartwave.chemist.feature.visit.ActivityVisitDetails"
                        });
                    }
                    //#endregion

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.VisitDetails;
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

        [HttpPost("HoldVisit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> HoldVisit([FromBody] AddOnHoldVisitModel model)
        {

            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<bool>();
                var userInfo = GetCurrentUserId();

                if (ModelState.IsValid)
                {
                    var addOnHoldVisitCommand = new AddOnHoldVisitCommand
                    {
                        OnHoldVisitId = Guid.NewGuid(),
                        CreateBy = GetCurrentUserId().UserId,
                        TimeZoneFrameGeoZoneId = model.TimeZoneFrameGeoZoneId,
                        DeviceSerialNo = model.DeviceSerialNo,
                        NoOfPatients = model.NoOfPatients
                    };

                    await _commandBus.SendAsync((IAddOnHoldVisitCommand)addOnHoldVisitCommand);
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = true;
                    return Ok(apiResponse);

                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "Invalid Input Parameter";
                    apiResponse.Response = false;
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("GetAllAgeSegments")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<AgeSegmentsDto>>), 200)]
        public async Task<IActionResult> GetAllAgeSegments()
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<List<AgeSegmentsDto>>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetAllAgeSegmentsQuery, IGetAllAgeSegmentsQueryResponse>(new GetAllAgeSegmentsQuery
                    {
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault()
                    });
                    if (!response.AgeSegments.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.AgeSegments;
                        apiResponse.Message = "No Age Segments found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.AgeSegments;
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

        [HttpGet("SearchPatientSchedule")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchPatientScheduleQueryResponse>), 200)]
        public async Task<IActionResult> SearchPatientSchedule([FromQuery] SearchPatientScheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var response = new HomeVisitsWebApiResponse<SearchPatientScheduleQueryResponse>();
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchPatientScheduleQuery, ISearchPatientScheduleQueryResponse>(new SearchPatientScheduleQuery
                    {
                        VisitDate = model.VisitDate,
                        PatientId = model.PatientId,
                        VisitStatusTypeId = model.VisitStatusTypeId,
                        cultureName = GetCultureName(),
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = model.ClientId,

                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchPatientScheduleQueryResponse
                    {

                        TodaysVisits = result.TodaysVisits,
                        OldVisits = result.OldVisits,
                        CurrentPageIndex = result.CurrentPageIndex,
                        PageSize = result.PageSize,
                        TotalCount = result.TotalCount
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GetCultureName() == CultureNames.ar ? "حدث خطاء" : "error");
            }

        }

        [HttpGet("SearchVisits")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchVisitsQueryResponse>), 200)]
        public async Task<IActionResult> SearchVisits([FromQuery] SearchVisitsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchVisitsQueryResponse>();
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchVisitsQuery, ISearchVisitsQueryResponse>(new SeachVisitsQuery
                    {
                        VisitDateFrom = model.VisitDateFrom,
                        VisitDateTo = model.VisitDateTo,
                        VisitNoFrom = model.VisitNoFrom,
                        VisitNoTo = model.VisitNoTo,
                        CreationDateFrom = model.CreationDateFrom,
                        CreationDateTo = model.CreationDateTo,
                        GovernateId = model.GovernateId,
                        GeoZoneId = model.GeoZoneId,
                        PatientNo = model.PatientNo,
                        PatientName = model.PatientName,
                        Gender = model.Gender,
                        PatientMobileNo = model.PatientMobileNo,
                        VisitStatusTypeId = model.VisitStatusTypeId,
                        NeedExpert = model.NeedExpert,
                        cultureName = GetCultureName(),
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        SortBy = model.SortBy,
                        AssignStatus = model.AssignStatus,
                        AssignedTo = model.AssignedTo,

                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchVisitsQueryResponse
                    {

                        Visits = result.Visits,
                        CurrentPageIndex = result.CurrentPageIndex,
                        PageSize = result.PageSize,
                        TotalCount = result.TotalCount
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GetCultureName() == CultureNames.ar ? "حدث خطاء" : "error");
            }

        }

        private Guid AssignChemist(AssignChemistModel model, LoggedInUserInfo userInfo)
        {
            Guid result = Guid.Empty;
            bool isPatientNeedsExpert = false;

            if (model.VisitTypeId == (int)VisitTypes.Self)
            {
                var patient = _queryProcessor.ProcessQueryAsync<IGetPatientByPatientIdQuery, IGetPatientByPatientIdQueryResponse>(new GetPatientByPatientIdQuery
                {
                    ClientId = userInfo.ClientId.GetValueOrDefault(),
                    PatientId = model.PatientId
                });
                var patientYear = patient.Result.Patient.BirthDate.Value.Year;

                var ageSegments = _queryProcessor.ProcessQueryAsync<IGetAllAgeSegmentsQuery, IGetAllAgeSegmentsQueryResponse>(new GetAllAgeSegmentsQuery
                {
                    ClientId = userInfo.ClientId.GetValueOrDefault()
                });

                if (ageSegments.Result.AgeSegments.Count > 0)
                {
                    isPatientNeedsExpert = ageSegments.Result.AgeSegments.Any(s => patientYear >= s.AgeFromYear && patientYear < s.AgeToYear);
                }
            }
            else
            {
                var ageSegments = _queryProcessor.ProcessQueryAsync<IGetAllAgeSegmentsQuery, IGetAllAgeSegmentsQueryResponse>(new GetAllAgeSegmentsQuery
                {
                    ClientId = userInfo.ClientId.GetValueOrDefault()
                });

                if (ageSegments.Result.AgeSegments.Count > 0)
                {
                    isPatientNeedsExpert = ageSegments.Result.AgeSegments.Find(s => s.AgeSegmentId == model.RelativeAgeSegmentId.GetValueOrDefault()).NeedExpert;
                }
            }

            var availabeChemists = _queryProcessor.ProcessQueryAsync<IGetChemistsByTimeZoneIdQuery, IGetChemistsByTimeZoneIdQueryResponse>(new GetChemistsByTimeZoneIdQuery
            {
                ClientId = userInfo.ClientId.GetValueOrDefault(),
                TimeZoneGeoZoneId = model.TimeZoneGeoZoneId,
                date = model.VisitDate
            });

            if (availabeChemists.Result.AvailableChemists.Count == 0)
            {
                return result;
            }

            if (isPatientNeedsExpert)
            {
                var expertChemists = availabeChemists.Result.AvailableChemists.Where(c => c.ExpertChemist == true);
                if (expertChemists.Count() > 0)
                {
                    foreach (var item in expertChemists)
                    {
                        if (item.VisitsNoQuota - item.TotalVisits > 0)
                        {
                            result = item.ChemistId;
                            break;
                        }
                    }

                }
            }

            if (result == Guid.Empty)
            {
                foreach (var item in availabeChemists.Result.AvailableChemists)
                {
                    if (item.VisitsNoQuota - item.TotalVisits > 0)
                    {
                        result = item.ChemistId;
                        break;
                    }
                }
            }

            return result;
        }

        [HttpGet("GetChemistRoutes")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<ChemistRouteDto>), 200)]
        public async Task<IActionResult> GetChemistRoutes([FromQuery] GetChemistRoutesModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<ChemistRouteDto>();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }

                    var response =
                        await _queryProcessor.ProcessQueryAsync<IGetChemistRoutesQuery, IGetChemistRoutesQueryResponse>(
                            new GetChemistRoutesQuery
                            {
                                ChemistId = userInfo.UserId,
                                ClientId = userInfo.ClientId.GetValueOrDefault(),
                                StartLatitude = model.StartLatitude,
                                StartLongitude = model.StartLongitude,
                                CultureName = GetCultureName()
                            });
                    if (response.Route == null && response.Route.Destinations == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("VisitDetailsNotFound", GetCulture());
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.Route;
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


    }
}
