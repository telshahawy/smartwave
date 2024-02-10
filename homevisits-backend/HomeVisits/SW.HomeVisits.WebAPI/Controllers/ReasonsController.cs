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

namespace SW.HomeVisits.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReasonsController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public ReasonsController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("SearchReasons")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchReasonsQueryResponse>), 200)]
        public async Task<IActionResult> SearchReasons([FromQuery] SearchReasonsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchReasonsQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<ISearchReasonsQuery, ISearchReasonsQueryResponse>(new SearchReasonsQuery
                    {
                        ReasonId = model.ReasonId,
                        ReasonName = model.ReasonName,
                        IsActive = model.IsActive,
                        VisitTypeActionId = model.VisitTypeActionId,
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchReasonsQueryResponse
                    {

                        Reasons = result.Reasons,
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

        [HttpPost("CreateReason")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> CreateReason([FromBody] CreateReasonModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<bool>();

                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();

                    var createReasonCommand = new CreateReasonCommand
                    {
                        ReasonName = model.ReasonName,
                        IsActive = model.IsActive,
                        VisitTypeActionId = model.VisitTypeActionId,
                        ReasonActionId = model.ReasonActionId,
                        ClientId = userInfo.ClientId.Value,
                    };

                    await _commandBus.SendAsync((ICreateReasonCommand)createReasonCommand);

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Message = "Data saved successfully";
                    response.Response = true;
                    return Ok(response);
                }
                else
                {
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    response.Message = "Invalid Input Parameter";
                    response.Response = false;
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("GetReasonForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<ReasonsDto>), 200)]
        public async Task<IActionResult> GetReasonForEdit([FromQuery] int reasonId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<ReasonsDto>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetReasonForEditQuery, IGetReasonForEditQueryResponse>(new GetReasonForEditQuery
                    {
                        ReasonId = reasonId
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = result.Reason;
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

        [HttpPut("UpdateReason")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateReason([FromQuery] int reasonId, [FromBody] UpdateReasonModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {

                    var updateReasonCommand = new UpdateReasonCommand
                    {
                        ReasonId = reasonId,
                        ReasonName = model.ReasonName,
                        IsActive = model.IsActive,
                        VisitTypeActionId = model.VisitTypeActionId,
                        ReasonActionId = model.ReasonActionId
                    };

                    await _commandBus.SendAsync((IUpdateReasonCommand)updateReasonCommand);

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
            catch (Exception ex)
            {
                response.Response = false;
                response.ResponseCode = WebApiResponseCodes.Failer;
                return BadRequest(response);
            }

        }

        [HttpDelete("DeleteReason")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteReason([FromQuery] int reasonId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteReasonCommand)new DeleteReasonCommand
                    {
                        ReasonId = reasonId
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

        [HttpGet("GetReasonActionsKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<ReasonActionsDto>>), 200)]
        public async Task<IActionResult> GetReasonActionsKeyValue()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<List<ReasonActionsDto>>();


                    var reasonActionsList = new List<ReasonActionsDto>
                    {
                        new ReasonActionsDto {

                            ReasonActionId = 1,
                            Name = "Cancel the request automatically"

                        },
                        new ReasonActionsDto {

                            ReasonActionId = 2,
                            Name = "Send to dashboard for action"

                        }
                    };

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = reasonActionsList;
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

    }
}
