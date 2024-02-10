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
using SW.HomeVisits.Application.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GeoZonesController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public GeoZonesController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("GeoZonesKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetGeoZonesKeyValueQueryResponse>), 200)]
        public async Task<IActionResult> GeoZonesKeyValue([FromQuery] GetGeoZonesKeyValueModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<GetGeoZonesKeyValueQueryResponse>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetGeoZonesKeyValueQuery, IGetGeoZonesKeyValueQueryResponse>(new GetGeoZonesKeyValueQuery
                    {
                        CultureName = GetCultureName(),
                        GovernateId = model.GovernateId,
                        ClientId = userInfo.ClientId
                    });
                    if (response == null || !response.GeoZones.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا توجد بيانات" : "No Items found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = new GetGeoZonesKeyValueQueryResponse
                    {
                        GeoZones = response.GeoZones
                    };
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

        [HttpGet("SearchGeoZones")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchGeoZonesQueryResponse>), 200)]
        public async Task<IActionResult> SearchGeoZones([FromQuery] SearchGeoZonesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchGeoZonesQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<ISearchGeoZonesQuery, ISearchGeoZonesQueryResponse>(new SearchGeoZonesQuery
                    {
                        Code = model.Code,
                        Name = model.Name,
                        IsActive = model.IsActive,
                        CountryId = model.CountryId,
                        GovernateId = model.GovernateId,
                        MappingCode = model.MappingCode,
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchGeoZonesQueryResponse
                    {

                        GeoZones = result.GeoZones,
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

        [HttpDelete("DeleteGeoZone")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteGeoZone([FromQuery] Guid geoZoneId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteGeoZoneCommand)new DeleteGeoZoneCommand
                    {
                        GeoZoneId = geoZoneId
                    });
                    response.Response = true;
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("AddGeoZone")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> AddGeoZone([FromBody] AddGeoZoneModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<Guid>();

                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    string errorMessage;
                    if (!ValidTimeZonesAsync(model.TimeZoneFrames, out errorMessage))
                    {
                        response.ResponseCode = WebApiResponseCodes.Failer;
                        response.Message = errorMessage;
                        return BadRequest(response);
                    }

                    var addGeoZoneCommand = new AddGeoZoneCommand
                    {
                        GeoZoneId = Guid.NewGuid(),
                        NameAr = model.AreaName,
                        NameEn = model.AreaName,
                        MappingCode = model.MappingCode,
                        KmlFilePath = model.KmlFilePath,
                        KmlFileName = model.KmlFileName,
                        GovernateId = model.GovernateId,
                        IsActive = model.IsActive,
                        CreatedBy = userInfo.UserId,
                        TimeZoneFrames = model.TimeZoneFrames.Select(item => new CreateTimeZoneFrameDto
                        {
                            TimeZoneFrameId = Guid.NewGuid(),
                            NameAr = model.AreaName,
                            NameEn = model.AreaName,
                            VisitsNoQuota = item.VisitsNoQuota,
                            StartTime = item.StartTime,
                            EndTime = item.EndTime,
                            BranchDispatch = item.BranchDispatch

                        }).ToList()
                    };

                    await _commandBus.SendAsync((IAddGeoZoneCommand)addGeoZoneCommand);

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = addGeoZoneCommand.GeoZoneId;
                    return Ok(response);
                }
                else
                {
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    response.Message = "Invalid Input Parameter";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }        }

        [HttpPut("UpdateGeoZone")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateGeoZone([FromBody] UpdateGeoZoneModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<bool>();

                if (ModelState.IsValid)
                {
                    string errorMessage;
                    if (!ValidTimeZonesAsync(model.TimeZoneFrames, out errorMessage))
                    {
                        response.ResponseCode = WebApiResponseCodes.Failer;
                        response.Message = errorMessage;
                        return BadRequest(response);
                    }

                    var updateGeoZoneCommand = new UpdateGeoZoneCommand
                    {
                        GeoZoneId = model.GeoZoneId,
                        Code = model.Code,
                        Name = model.Name,
                        MappingCode = model.MappingCode,
                        KmlFilePath = model.KmlFilePath,
                        KmlFileName = model.KmlFileName,
                        GovernateId = model.GovernateId,
                        IsActive = model.IsActive,
                        TimeZoneFrames = model.TimeZoneFrames.Select(item => new CreateTimeZoneFrameDto
                        {
                            TimeZoneFrameId = item.TimeZoneFrameId,
                            NameAr = model.Name,
                            NameEn = model.Name,
                            VisitsNoQuota = item.VisitsNoQuota,
                            StartTime = item.StartTime,
                            EndTime = item.EndTime,
                            BranchDispatch = item.BranchDispatch

                        }).ToList()
                    };

                    await _commandBus.SendAsync((IUpdateGeoZoneCommand)updateGeoZoneCommand);

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = true;
                    return Ok(response);
                }
                else
                {
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    response.Message = "Invalid Input Parameter";
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("GetGeoZoneForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GeoZonesDto>), 200)]
        public async Task<IActionResult> GetGeoZoneForEdit([FromQuery] Guid geoZoneId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<GeoZonesDto>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetGeoZoneForEditQuery, IGetGeoZoneForEditQueryResponse>(new GetGeoZoneForEditQuery
                    {
                        GeoZoneId = geoZoneId
                    });

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = result.GeoZone;
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

        [HttpGet("GetTimeZonesForGeoZone")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetTimeZonesForGeoZoneQueryResponse>), 200)]
        public async Task<IActionResult> GetTimeZonesForGeoZone([FromQuery] Guid geoZoneId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<GetTimeZonesForGeoZoneQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetGeoZoneForEditQuery, IGetTimeZonesForGeoZoneQueryResponse>(new GetGeoZoneForEditQuery
                    {
                        GeoZoneId = geoZoneId
                    });

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetTimeZonesForGeoZoneQueryResponse
                    {
                        TimeZonesForGeoZone = result.TimeZonesForGeoZone
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

        #region Helper Method
        private bool ValidTimeZonesAsync(List<AddTimeZoneFramesModel> timeZoneFrames, out string errorMessage)
        {
            var userInfo = GetCurrentUserId();
            var systemParameters = _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
            {
                ClientId = userInfo.ClientId.GetValueOrDefault()
            }).Result;

            var estimatedVisteDuration = systemParameters.SystemParameters.EstimatedVisitDurationInMin;
            var areaTimes = timeZoneFrames.OrderBy(p => p.StartTime).ToList();

            for (int i = 0; i < areaTimes.Count; i++)
            {
                var maxVisitNo = (TimeSpan.Parse(areaTimes[i].EndTime).TotalMinutes - TimeSpan.Parse(areaTimes[i].StartTime).TotalMinutes) / estimatedVisteDuration;
                if (areaTimes[i].VisitsNoQuota > maxVisitNo)
                {
                    errorMessage = "Maximum Visit No is exceeded";
                    return false;
                }
                if (i > 0 && TimeSpan.Parse(areaTimes[i].StartTime) <= TimeSpan.Parse(areaTimes[i - 1].EndTime))
                {
                    errorMessage = "Time is intersected.";
                    return false;
                }
            }
            errorMessage = null;
            return true;
        }

        private bool ValidTimeZonesAsync(List<UpdateTimeZoneFrameModel> timeZoneFrames, out string errorMessage)
        {
            var userInfo = GetCurrentUserId();
            var systemParameters = _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
            {
                ClientId = userInfo.ClientId.GetValueOrDefault()
            }).Result;

            var estimatedVisteDuration = systemParameters.SystemParameters.EstimatedVisitDurationInMin;
            var areaTimes = timeZoneFrames.OrderBy(p => p.StartTime).ToList();

            for (int i = 0; i < areaTimes.Count; i++)
            {
                var maxVisitNo = (TimeSpan.Parse(areaTimes[i].EndTime).TotalMinutes - TimeSpan.Parse(areaTimes[i].StartTime).TotalMinutes) / estimatedVisteDuration;
                if (areaTimes[i].VisitsNoQuota > maxVisitNo)
                {
                    errorMessage = "Maximum Visit No is exceeded";
                    return false;
                }
                if (i > 0 && TimeSpan.Parse(areaTimes[i].StartTime) <= TimeSpan.Parse(areaTimes[i - 1].EndTime))
                {
                    errorMessage = "Time is intersected.";
                    return false;
                }
            }
            errorMessage = null;
            return true;
        }
        #endregion
    }
}

