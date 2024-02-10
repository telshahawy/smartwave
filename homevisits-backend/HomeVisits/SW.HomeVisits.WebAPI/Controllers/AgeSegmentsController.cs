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
using SW.Framework.Exceptions;
using SW.HomeVisits.Application.Abstract.Validations;
using System.Globalization;
using SW.HomeVisits.WebAPI.CustomAttribute;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeByUserPermission]
    public class AgeSegmentsController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public AgeSegmentsController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("SearchAgeSegments")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchAgeSegmentsQueryResponse>), 200)]
        public async Task<IActionResult> SearchAgeSegments([FromQuery] SearchAgeSegmentsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchAgeSegmentsQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<ISearchAgeSegmentsQuery, ISearchAgeSegmentsQueryResponse>(new SearchAgeSegmentsQuery
                    {
                        Code = model.Code,
                        Name = model.Name,
                        IsActive = model.IsActive,
                        NeedExpert = model.NeedExpert,
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchAgeSegmentsQueryResponse
                    {

                        AgeSegments = result.AgeSegments,
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

        [HttpPost("CreateAgeSegment")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateAgeSegment([FromBody] CreateAgeSegmentModel model)
        {
            var response = new HomeVisitsWebApiResponse<Guid>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchAgeSegmentsQuery, ISearchAgeSegmentsQueryResponse>(new SearchAgeSegmentsQuery
                    {
                        IsActive = model.IsActive,

                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });

                    foreach (var item in result.AgeSegments)
                    {
                        int yearStart = item.AgeFromYear;
                        int yearEnd = item.AgeToYear;
                        int monthsStart = item.AgeFromMonth;
                        int monthsEnd = item.AgeToMonth;
                        int daysStart = item.AgeFromDay;
                        int daysEnd = item.AgeToDay;
                        bool AgeFromInclusive = item.AgeFromInclusive;
                        bool AgeToInclusive = item.AgeToInclusive;
                       int  nodaysStart = (yearStart * 365) +  (monthsStart*30)+ daysStart;
                       int  nodaysEnd = (yearEnd * 365) + ( monthsEnd * 30) + daysEnd;
                        bool includeStart = item.AgeFromInclusive;
                        bool includeEnd = item.AgeToInclusive;

                        int yearStartModel = model.AgeFromYear;
                        int yearEndModel = model.AgeToYear;
                        int monthsStartModel = model.AgeFromMonth;
                        int monthsEndModel = model.AgeToMonth;
                        int daysStartModel = model.AgeFromDay;
                        int daysEndModel = model.AgeToDay;
                        bool includeStartModel = model.AgeFromInclusive;
                        bool includeEndModel = model.AgeToInclusive;

                        int nodaysStartModel = (yearStartModel * 365) + (monthsStartModel*30) + daysStartModel;
                           int nodaysEndModel = (yearEndModel * 365) + (monthsEndModel*30) + daysEndModel;

                        if (!includeStartModel) nodaysStartModel--;
                        if (!includeEndModel) nodaysEndModel--;
                        if (!includeStart) nodaysStart--;
                        if (!includeEnd) nodaysEnd--;
                       
                        //a.Start<b.end&&b.start<a.end//...
                        //(StartA > StartB? Start A: StartB) <= (EndA < EndB? EndA: EndB)
                      
                            if (nodaysStartModel <= nodaysEnd && nodaysStart <= nodaysEndModel)
                            {
                                response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                                response.Message = "Another Age segment is reserved for these ages. ";
                                return BadRequest(response);
                            }
                       
                    }

                    var createAgeSegmentCommand = new CreateAgeSegmentCommand
                    {
                        AgeSegmentId = Guid.NewGuid(),
                        Name = model.Name,
                        AgeFromDay = model.AgeFromDay,
                        AgeFromMonth = model.AgeFromMonth,
                        AgeFromYear = model.AgeFromYear,
                        AgeFromInclusive = model.AgeFromInclusive,
                        AgeToDay = model.AgeToDay,
                        AgeToMonth = model.AgeToMonth,
                        AgeToYear = model.AgeToYear,
                        AgeToInclusive = model.AgeToInclusive,
                        NeedExpert = model.NeedExpert,
                        IsActive = model.IsActive,
                        ClientId = userInfo.ClientId.Value,
                        CreatedBy = userInfo.UserId
                    };

                    await _commandBus.SendAsync((ICreateAgeSegmentCommand)createAgeSegmentCommand);

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Message = "Data saved successfully";
                    response.Response = createAgeSegmentCommand.AgeSegmentId;
                    return Ok(response);
                }
                else
                {
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    response.Message = "Invalid Input Parameter";
                    return BadRequest();
                }
            }
            catch (ValidationRuleException ex)
            {
                ErrorCodes code = (ErrorCodes)ex.ErrorCode;
                switch (code)
                {
                    case ErrorCodes.CountryNameAlreadyExists:
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = GetCultureName() == CultureNames.ar ? "إسم الدولة موجود من قبل" : "country name already exists";
                        return BadRequest(response);
                    default:
                        throw new Exception($"something wrong happened on the server" + ex.ErrorCode.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPut("UpdateAgeSegment")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateAgeSegment([FromQuery] Guid ageSegmentId, [FromBody] UpdateAgeSegmentModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {

                    var updateAgeSegmentCommand = new UpdateAgeSegmentCommand
                    {
                        AgeSegmentId = ageSegmentId,
                        Name = model.Name,
                        AgeFromDay = model.AgeFromDay,
                        AgeFromMonth = model.AgeFromMonth,
                        AgeFromYear = model.AgeFromYear,
                        AgeFromInclusive = model.AgeFromInclusive,
                        AgeToDay = model.AgeToDay,
                        AgeToMonth = model.AgeToMonth,
                        AgeToYear = model.AgeToYear,
                        AgeToInclusive = model.AgeToInclusive,
                        NeedExpert = model.NeedExpert,
                        IsActive = model.IsActive
                    };

                    await _commandBus.SendAsync((IUpdateAgeSegmentCommand)updateAgeSegmentCommand);

                    response.Response = true;
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
                }
                else
                {
                    response.Response = false;
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    return BadRequest(response);
                }
            }
            catch (ValidationRuleException ex)
            {
                ErrorCodes code = (ErrorCodes)ex.ErrorCode;
                switch (code)
                {
                    case ErrorCodes.CountryNameAlreadyExists:
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = GetCultureName() == CultureNames.ar ? "إسم الدولة موجود من قبل" : "country name already exists";
                        return BadRequest(response);
                    default:
                        throw new Exception($"something wrong happened on the server" + ex.ErrorCode.ToString());
                }
            }
            catch (Exception ex)
            {
                response.Response = false;
                response.ResponseCode = WebApiResponseCodes.Failer;
                return BadRequest(response);
            }

        }

        [HttpGet("GetAgeSegmentForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<AgeSegmentsDto>), 200)]
        public async Task<IActionResult> GetAgeSegmentForEdit([FromQuery] Guid ageSegmentId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<AgeSegmentsDto>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetAgeSegmentForEditQuery, IGetAgeSegmentForEditQueryResponse>(new GetAgeSegmentForEditQuery
                    {
                        AgeSegmentId = ageSegmentId
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = result.AgeSegment;
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

        [HttpDelete("DeleteAgeSegment")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteAgeSegment([FromQuery] Guid ageSegmentId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteAgeSegmentCommand)new DeleteAgeSegmentCommand
                    {
                        AgeSegmentId = ageSegmentId
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
    }
}
