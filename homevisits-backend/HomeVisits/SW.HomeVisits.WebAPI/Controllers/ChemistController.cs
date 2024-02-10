using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Authentication;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.HomeVisits.Application.Validations;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.WebAPI.Helper;
using SW.HomeVisits.WebAPI.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.HomeVisits.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChemistController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAuthenticationManager _authenticationManager;

        public ChemistController(ICommandBus commandBus, IQueryProcessor queryProcessor, IAuthenticationManager authenticationManager)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _authenticationManager = authenticationManager;
        }
        [HttpPost("CreateChemist")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateChemist([FromBody] CreateChemistModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<Guid>();

                    if (model.BirthDate >= DateTime.Today)
                    {

                        response.ResponseCode = WebApiResponseCodes.Failer;
                        response.Message = "Birth date can't be equal or greater than today";
                        return BadRequest(response);
                    }
                    var createChemistCommand = new CreateChemistCommand
                    {
                        BirthDate = model.BirthDate,
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault(),
                        DOB = model.DOB,
                        ExpertChemist = model.ExpertChemist,
                        Gender = model.Gender,
                        CreatedBy = GetCurrentUserId().UserId,
                        UserName = model.UserName,
                        UserId = Guid.NewGuid(),
                        IsActive = model.IsActive,
                        Password = model.Password,
                        PersonalPhoto = model.PersonalPhoto,
                        JoinDate = model.JoinDate,
                        PhoneNumber = model.PhoneNumber,
                        Name = model.Name,
                    };
                    createChemistCommand.GeoZoneIds = new List<Guid>();
                    createChemistCommand.GeoZoneIds.AddRange(model.GeoZoneIds);
                    await _commandBus.SendAsync((ICreateChemistCommand)createChemistCommand);
                    response.Response = createChemistCommand.UserId;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == typeof(ValidateBirthDateRule).ToString())
                {
                    throw new Exception("Chemist birth date cannot begreater than today");
                }
                throw ex;
            }

        }

        [HttpGet("SearchChemists")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchChemistsQueryResponse>), 200)]
        public async Task<IActionResult> SearchChemists([FromQuery] SearchChemistsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchChemistsQueryResponse>();
                    
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchChemistsQuery, ISearchChemistsQueryResponse>(new SeachChemistsQuery
                    {
                        Code = model.Code,
                        Gender = model.Gender,
                        ChemistName = model.ChemistName,
                        ChemistStatus = model.ChemistStatus,
                        CountryId = model.CountryId,
                        CurrentPageIndex = model.CurrentPageIndex,
                        ExpertChemist = model.ExpertChemist,
                        GeoZoneId = model.GeoZoneId,
                        PhoneNo = model.PhoneNo,
                        cultureName = GetCultureName(),
                        GovernateId = model.GovernateId,
                        JoinDateFrom = model.JoinDateFrom,
                        JoinDateto = model.JoinDateto,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        AreaAssignStatus = model.AreaAssignStatus
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchChemistsQueryResponse
                    {

                        Chemists = result.Chemists,
                        CurrentPageIndex = result.CurrentPageIndex,
                        PageSize = result.PageSize,
                        TotalCount = result.TotalCount
                    };
                    return Ok(response);
                    ////return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GetCultureName() == CultureNames.ar ? "حدث خطاء" : "error", ex);
            }

        }
        
        [HttpGet("GetChemistsLastTracking")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetChemistsLastTrackingsQueryResponse>), 200)]
        public async Task<IActionResult> GetChemistsLastTracking([FromQuery] GetChemistsLastTrackingsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<GetChemistsLastTrackingsQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<IGetChemistsLastTrackingsQuery, IGetChemistsLastTrackingsQueryResponse>(new GetChemistsLastTrackingsQuery
                    {
                        Name = model.Name,
                        CultureName = GetCultureName(),
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetChemistsLastTrackingsQueryResponse
                    {
                        ChemistLastTrackingLogs = result.ChemistLastTrackingLogs,
                        CurrentPageIndex = result.CurrentPageIndex,
                        TotalCount = result.TotalCount,
                        PageSize = result.PageSize
                    };
                    return Ok(response);
                    ////return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpPost("UpdateChemist")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateChemist([FromBody] UpdateChemistModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.BirthDate >= DateTime.Today)
                    {
                        response.Response = false;
                        response.ResponseCode = WebApiResponseCodes.Failer;
                        response.Message = "Birth date can't be equal or greater than today";
                        return BadRequest(response);
                    }
                    var updateChemistCommand = new UpdateChemistCommand
                    {
                        BirthDate = model.BirthDate,
                        ExpertChemist = model.ExpertChemist,
                        Gender = model.Gender,
                        IsActive = model.IsActive,
                        PersonalPhoto = model.PersonalPhoto,
                        JoinDate = model.JoinDate,
                        PhoneNumber = model.PhoneNumber,
                        Name = model.Name,
                        UserId = model.UserId,
                        GeoZoneIds = model.GeoZoneIds
                    };
                    await _commandBus.SendAsync((IUpdateChemistCommand)updateChemistCommand);

                    response.Response = true;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
                }
                else
                {
                    response.Response = false;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Response = false;
                response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                return BadRequest(response);
            }

        }

        [HttpDelete("DeleteChemist")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteChemist([FromQuery] Guid ChemistId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteChemistCommand)new DeleteChemistCommand
                    {
                        UserId = ChemistId
                    });
                    response.Response = true;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpGet("GeoChemistZonesKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetGeoZonesKeyValueQueryResponse>), 200)]
        public async Task<IActionResult> GeoChemistZonesKeyValue()
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<GetChemistGeoZonesKeyValueQueryResponse>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetChemistGeoZonesKeyValueQuery,
                        IGetChemistGeoZonesKeyValueQueryResponse>(new GetChemistGeoZonesKeyValueQuery
                        {
                            CultureName = GetCultureName(),
                            ChemistId = userInfo.UserId
                        });
                    if (response == null || !response.GeoZones.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا توجد مناطق متاحة" : "No area Available";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = new GetChemistGeoZonesKeyValueQueryResponse
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

        [HttpGet("ChemistsKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<ChemistKeyValueDto>>), 200)]
        public async Task<IActionResult> ChemistsKeyValue(Guid? GeoZoneId)
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<List<ChemistKeyValueDto>>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetChemistsKeyValueQuery, IGetChemistsKeyValueQueryResponse>(new GetChemistsKeyValueQuery
                    {
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault(),
                        GeoZoneId = GeoZoneId,
                        CultureName = GetCultureName()
                    });
                    if (!response.Chemists.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = "No Chemists found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.Chemists;
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

        [HttpGet("GetChemistForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetChemistForEditQueryResponse>), 200)]
        public async Task<IActionResult> GetChemistForEdit([FromQuery] Guid ChemistId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetChemistForEditQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<IGetChemistForEditQuery, IGetChemistForEditQueryResponse>(new GetChemistForEditQuery
                    {
                        ChemistId = ChemistId
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetChemistForEditQueryResponse
                    {
                        Chemist = result.Chemist
                    };
                    return Ok(response);
                    ////return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpPost("CreateChemistSchedule")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateChemistSchedule([FromBody] CreateChemistScheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<Guid>();

                    var result = await _queryProcessor.ProcessQueryAsync<ISearchChemistScheduleQuery, ISearchChemistScheduleQueryResponse>(new SearchChemistSchduleQuery
                    {

                        ChemistId = model.ChemistId,
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault(),
                        //EndDate = model.EndDate,
                        //StartDate = model.StartDate
                    });
                    if (result.Schedules.Any(x => x.StartDate <= model.StartDate && x.EndDate >= model.StartDate
                    || x.StartDate <= model.EndDate && x.EndDate >= model.EndDate
                    ))
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = "Another Schedule is reserved for these days. ";
                        return BadRequest(response);
                    }
                    if (model.StartDate < DateTime.Now || model.EndDate < DateTime.Now)
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = "You can not add schedule with in past dates. ";
                        return BadRequest(response);
                    }


                    var createChemistScheduleCommand = new CreateChemistScheduleCommand
                    {
                        AssignedChemistGeoZoneId = model.AssignedChemistGeoZoneId,
                        ChemistScheduleId = Guid.NewGuid(),
                        ChemistId = model.ChemistId,
                        CreatedBy = GetCurrentUserId().UserId,
                        EndDate = model.EndDate,
                        FriEnd = model.FriEnd,
                        FriStart = model.FriStart,
                        MonEnd = model.MonEnd,
                        MonStart = model.MonStart,
                        SatEnd = model.SatEnd,
                        SatStart = model.SatStart,
                        StartDate = model.StartDate,
                        StartLangitude = model.StartLangitude,
                        StartLatitude = model.StartLatitude,
                        SunEnd = model.SunEnd,
                        SunStart = model.SunStart,
                        ThuEnd = model.ThuEnd,
                        ThuStart = model.ThuStart,
                        TueEnd = model.TueEnd,
                        TueStart = model.TueStart,
                        WedEnd = model.WedEnd,
                        WedStart = model.WedStart
                    };
                    await _commandBus.SendAsync((ICreateChemistScheduleCommand)createChemistScheduleCommand);
                    response.Response = createChemistScheduleCommand.ChemistScheduleId;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpPut("UpdateChemistSchedule")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateChemistSchedule([FromQuery] Guid ChemistScheduleId, [FromBody] UpdateChemistScheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    var updateChemistScheduleCommand = new UpdateChemistScheduleCommand
                    {
                        AssignedChemistGeoZoneId = model.AssignedChemistGeoZoneId,
                        ChemistScheduleId = ChemistScheduleId,
                        EndDate = model.EndDate,
                        FriEnd = model.FriEnd,
                        FriStart = model.FriStart,
                        MonEnd = model.MonEnd,
                        MonStart = model.MonStart,
                        SatEnd = model.SatEnd,
                        SatStart = model.SatStart,
                        StartDate = model.StartDate,
                        StartLangitude = model.StartLangitude,
                        StartLatitude = model.StartLatitude,
                        SunEnd = model.SunEnd,
                        SunStart = model.SunStart,
                        ThuEnd = model.ThuEnd,
                        ThuStart = model.ThuStart,
                        TueEnd = model.TueEnd,
                        TueStart = model.TueStart,
                        WedEnd = model.WedEnd,
                        WedStart = model.WedStart
                    };
                    await _commandBus.SendAsync((IUpdateChemistScheduleCommand)updateChemistScheduleCommand);
                    response.Response = true;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpDelete("DeleteChemistSchedule")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteChemistSchedule([FromQuery] Guid ChemistScheduleId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    var deleteChemistCommand = new DeleteChemistScheduleCommand
                    {
                        ChemistScheduleId = ChemistScheduleId,
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault()
                    };
                    await _commandBus.SendAsync((IDeleteChemistScheduleCommand)deleteChemistCommand);
                    response.Response = true;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpPost("DuplicateChemistSchedule")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> DuplicateChemistSchedule([FromBody] DublicateChemistScheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<Guid>();
                    //
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchChemistScheduleQuery, ISearchChemistScheduleQueryResponse>(new SearchChemistSchduleQuery
                    {

                        ChemistId = model.ChemistId,
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault(),
                        //EndDate = model.EndDate,
                        //StartDate = model.StartDate
                    });
                    if (result.Schedules.Any(x => x.StartDate <= model.StartDate && x.EndDate >= model.StartDate
                    || x.StartDate <= model.EndDate && x.EndDate >= model.EndDate
                    ))
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = "Another Schedule is reserved for these days. ";
                        return BadRequest(response);
                    }
                    if (model.StartDate < DateTime.Now || model.EndDate < DateTime.Now)
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = "You can only duplicate with in future dates. ";
                        return BadRequest(response);
                    }
                    var duplicateChemistScheduleCommand = new DuplicateChemistScheduleCommand
                    {
                        NewChemistScheduleId = Guid.NewGuid(),
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault(),
                        CreatedAt = DateTime.Now,
                        CreatedBy = GetCurrentUserId().UserId,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        ChemistScheduleId = model.ChemistScheduleId
                    };
                    await _commandBus.SendAsync((IDuplicateChemistScheduleCommand)duplicateChemistScheduleCommand);
                    response.Response = duplicateChemistScheduleCommand.NewChemistScheduleId;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpGet("GetChemistScheduleForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<CreateChemistScheduleModel>), 200)]
        public async Task<IActionResult> GetChemistScheduleForEdit([FromQuery] Guid ChemistScheduleId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<CreateChemistScheduleModel>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<IGetChemistScheduleForEditQuery, IGetChemistScheduleForEditQueryResponse>(new GetChemistScheduleForEditQuery
                    {
                        ChemistScheduleId = ChemistScheduleId,
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new CreateChemistScheduleModel
                    {
                        ChemistId = result.Schedule.ChemistId,
                        ChemistName = result.Schedule.ChemistName,
                        AssignedChemistGeoZoneId = result.Schedule.AssignedChemistGeoZoneId,
                        EndDate = result.Schedule.EndDate,
                        FriEnd = result.Schedule.FriEnd,
                        FriStart = result.Schedule.FriStart,
                        MonEnd = result.Schedule.MonEnd,
                        MonStart = result.Schedule.MonStart,
                        SatEnd = result.Schedule.SatEnd,
                        SatStart = result.Schedule.SatStart,
                        StartDate = result.Schedule.StartDate,
                        StartLangitude = result.Schedule.StartLangitude,
                        StartLatitude = result.Schedule.StartLatitude,
                        SunEnd = result.Schedule.SunEnd,
                        SunStart = result.Schedule.SunStart,
                        ThuEnd = result.Schedule.ThuEnd,
                        ThuStart = result.Schedule.ThuStart,
                        TueEnd = result.Schedule.TueEnd,
                        TueStart = result.Schedule.TueStart,
                        WedEnd = result.Schedule.WedEnd,
                        WedStart = result.Schedule.WedStart
                    };
                    return Ok(response);
                    ////return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpGet("GetChemistAssignedGeoZonesKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<ChemistAssignedGeoZoneKeyValueDto>>), 200)]
        public async Task<IActionResult> GetChemistAssignedGeoZonesKeyValue(Guid chemistId)
        {
            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<List<ChemistAssignedGeoZoneKeyValueDto>>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetChemistAssignedGeoZoneKeyValueQuery, IGetChemistAssignedGeoZoneKeyValueQueryResponse>(new GetChemistAssignedGeoZonesKeyValueQuery
                    {
                        ClientId = GetCurrentUserId().ClientId.GetValueOrDefault(),
                        ChemistId = chemistId
                    });
                    if (!response.GeoZones.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = "No Chemists found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.GeoZones;
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

        [HttpGet("SearchChemistSchedule")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchChemistScheduleQueryResponse>), 200)]
        public async Task<IActionResult> SearchChemistSchedule([FromQuery] SearchChemistScheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchChemistScheduleQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchChemistScheduleQuery, ISearchChemistScheduleQueryResponse>(new SearchChemistSchduleQuery
                    {
                        AssignedGeoZoneId = model.AssignedGeoZoneId,
                        ChemistId = model.ChemistId,
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        EndDate = model.EndDate,
                        StartDate = model.StartDate
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchChemistScheduleQueryResponse
                    {
                        Schedules = result.Schedules
                    };
                    return Ok(response);
                    ////return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpPost("CreateChemistPermit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateChemistPermit([FromBody] CreateChemistPermitModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<Guid>();

                    var chemistVisitInPermitTime = await _queryProcessor.ProcessQueryAsync<IGetChemistVisitInPermitTimeQuery, IGetChemistVisitInPermitTimeQueryResponse>(new GetChemistVisitInPermitTimeQuery
                    {
                        ChemistId = model.ChemistId,
                        PermitStartTime = TimeSpan.Parse(model.StartTime),
                        PermitEndTime =TimeSpan.Parse(model.EndTime),
                        PermitDate = model.PermitDate,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    if (chemistVisitInPermitTime != null && chemistVisitInPermitTime.ChemistVisitInPermitTime != null && chemistVisitInPermitTime.ChemistVisitInPermitTime.Count > 0)
                    {
                        response.Response = Guid.Empty;
                        response.Message = "Can't Create Chemist Permit, there visit assigned to chemist in this permit interval";
                        response.ErrorList = chemistVisitInPermitTime.ChemistVisitInPermitTime.Select(p => string.Format("Visit Code:- {0}, Time Slot:- {1}", p.VisitCode.ToString(), p.TimeZoneTimeSlot)).ToList();
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        return Ok(response);
                    }

                    var createChemistPermitCommand = new CreateChemistPermitCommand
                    {
                        ChemistId = model.ChemistId,
                        ChemistPermitId = Guid.NewGuid(),
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        CreatedAt = DateTime.Now,
                        CreatedBy = userInfo.UserId,
                        EndTime = TimeSpan.Parse(model.EndTime),
                        PermitDate = model.PermitDate,
                        StartTime = TimeSpan.Parse(model.StartTime)

                    };
                    await _commandBus.SendAsync((ICreateChemistPermitCommand)createChemistPermitCommand);
                    response.Response = createChemistPermitCommand.ChemistPermitId;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpPut("UpdateChemistPermit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateChemistPermit(Guid chemistPermitId, [FromBody] UpdateChemistPermitModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<bool>();
                    var chemistPermit = await _queryProcessor.ProcessQueryAsync<IGetChemistPermitForEditQuery, IGetChemistPermitForEditQueryResponse>(new GetChemistPermitForEditQuery
                    {
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        ChemistPermitId = chemistPermitId
                    });

                    var chemistVisitInPermitTime = await _queryProcessor.ProcessQueryAsync<IGetChemistVisitInPermitTimeQuery, IGetChemistVisitInPermitTimeQueryResponse>(new GetChemistVisitInPermitTimeQuery
                    {
                        ChemistId = chemistPermit.Permit.ChemistId,
                        PermitStartTime = TimeSpan.Parse(model.StartTime),
                        PermitEndTime = TimeSpan.Parse(model.EndTime),
                        PermitDate = model.PermitDate,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    if (chemistVisitInPermitTime != null && chemistVisitInPermitTime.ChemistVisitInPermitTime != null && chemistVisitInPermitTime.ChemistVisitInPermitTime.Count > 0)
                    {
                        response.Response = false;
                        response.Message = "Can't Update Chemist Permit, there visit assigned to chemist in this permit interval";
                        response.ErrorList = chemistVisitInPermitTime.ChemistVisitInPermitTime.Select(p => string.Format("Visit Code:- {0}, Time Slot:- {1}", p.VisitCode.ToString(), p.TimeZoneTimeSlot)).ToList();
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        return Ok(response);
                    }

                    var updateChemistPermitCommand = new UpdateChemistPermitCommand
                    {
                        ChemistPermitId = chemistPermitId,
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        EndTime = TimeSpan.Parse(model.EndTime),
                        PermitDate = model.PermitDate,
                        StartTime = TimeSpan.Parse(model.StartTime)

                    };
                    await _commandBus.SendAsync((IUpdateChemistPermitCommand)updateChemistPermitCommand);
                    response.Response = true;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpDelete("DeleteChemistPermit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteChemistPermit(Guid chemistPermitId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<bool>();
                    var deleteChemistPermitCommand = new DeleteChemistPermitCommand
                    {
                        ChemistPermitId = chemistPermitId,
                        ClientId = user.ClientId.GetValueOrDefault()
                    };
                    await _commandBus.SendAsync((IDeleteChemistPermitCommand)deleteChemistPermitCommand);
                    response.Response = true;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpGet("SearchChemistPermits")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchChemistPermitsQueryResponse>), 200)]
        public async Task<IActionResult> SearchChemistPermits([FromQuery] SearchChemistPermitsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchChemistPermitsQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchChemistPermitsQuery, ISearchChemistPermitsQueryResponse>(new SearchChemistPermitsQuery
                    {
                        ChemistId = model.ChemistId,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchChemistPermitsQueryResponse
                    {
                        Permits = result.Permits
                    };
                    return Ok(response);
                    ////return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpGet("GetChemistPermitForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetChemistPermitForEditQueryResponse>), 200)]
        public async Task<IActionResult> GetChemistPermitForEdit([FromQuery] Guid ChemistPermitId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetChemistPermitForEditQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<IGetChemistPermitForEditQuery, IGetChemistPermitForEditQueryResponse>(new GetChemistPermitForEditQuery
                    {
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        ChemistPermitId = ChemistPermitId
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetChemistPermitForEditQueryResponse
                    {
                        Permit = result.Permit
                    };
                    return Ok(response);
                    ////return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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

        [HttpGet("GetChemistRoutes")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetChemistPermitForEditQueryResponse>), 200)]
        public async Task<IActionResult> GetChemistRoutes([FromQuery] GetChemistRoutesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetChemistRoutesQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<IGetChemistRoutesQuery, IGetChemistRoutesQueryResponse>(new GetChemistRoutesQuery
                    {
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        CultureName = GetCultureName(),
                        ChemistId = userInfo.UserId,
                        StartLatitude = model.StartLatitude,
                        StartLongitude = model.StartLongitude
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetChemistRoutesQueryResponse
                    {
                        Route = result.Route
                    };
                    return Ok(response);
                    ////return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
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
    }
}
