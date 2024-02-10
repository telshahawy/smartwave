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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GovernatsController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public GovernatsController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("GovernatsKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetGovernatsKeyValueQueryResponse>), 200)]
        public async Task<IActionResult> GovernatsKeyValue([FromQuery] GetGovernatsKeyValueModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<GetGovernatsKeyValueQueryResponse>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetGovernatsKeyValueQuery, IGetGovernatsKeyValueQueryResponse>(new GetGovernatsKeyValueQuery
                    {
                        CultureName = GetCultureName(),
                        CountryId = model.CountryId,
                        ClientId = userInfo.ClientId
                    });
                    if (response == null || !response.Governats.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا توجد بيانات" : "No Items found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = new GetGovernatsKeyValueQueryResponse
                    {
                        Governats = response.Governats
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

        [HttpGet("SearchGovernats")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchGovernatsQueryResponse>), 200)]
        public async Task<IActionResult> SearchGovernats([FromQuery] SearchGovernatsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchGovernatsQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<ISearchGovernatsQuery, ISearchGovernatsQueryResponse>(new SearchGovernatsQuery
                    {
                        Code = model.Code,
                        Name = model.Name,
                        IsActive = model.IsActive,
                        CountryId = model.CountryId,
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchGovernatsQueryResponse
                    {

                        Governats = result.Governats,
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

        [HttpDelete("DeleteGovernate")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteGovernate([FromQuery] Guid governateId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteGovernatCommand)new DeleteGovernateCommand
                    {
                        GovernateId = governateId
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

        [HttpPost("CreateGovernate")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateGovernate([FromBody] CreateGovernateModel model)
        {
            var response = new HomeVisitsWebApiResponse<Guid>();

            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();

                    var createGovernateCommand = new CreateGovernateCommand
                    {
                        GovernateId = Guid.NewGuid(),
                        CountryId = model.CountryId,
                        GovernateNameEn = model.GovernateNameEn,
                        GovernateNameAr = model.GovernateNameEn,
                        CustomerServiceEmail = model.CustomerServiceEmail,
                        IsActive = model.IsActive,
                        CreatedBy = userInfo.UserId
                    };

                    await _commandBus.SendAsync((ICreateGovernateCommand)createGovernateCommand);

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Message = "Data saved successfully";
                    response.Response = createGovernateCommand.GovernateId;
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
                    case ErrorCodes.GovernateNameAlreadyExists:
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = GetCultureName() == CultureNames.ar ? "إسم المحافظة موجود من قبل" : "governate name already exists";
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

        [HttpPut("UpdateGovernate")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateGovernate([FromQuery] Guid governateId, [FromBody] UpdateGovernateModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {

                    var updateGovernateCommand = new UpdateGovernateCommand
                    {
                        GovernateId = governateId,
                        CountryId = model.CountryId,
                        GovernateNameEn = model.GovernateNameEn,
                        CustomerServiceEmail = model.CustomerServiceEmail,
                        IsActive = model.IsActive
                    };

                    await _commandBus.SendAsync((IUpdateGovernateCommand)updateGovernateCommand);

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
                    case ErrorCodes.GovernateNameAlreadyExists:
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = GetCultureName() == CultureNames.ar ? "إسم المحافظة موجود من قبل" : "governate name already exists";
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

        [HttpGet("GetGovernateForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GovernatsDto>), 200)]
        public async Task<IActionResult> GetGovernateForEdit([FromQuery] Guid governateId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GovernatsDto>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetGovernateForEditQuery, IGetGovernateForEditQueryResponse>(new GetGovernateForEditQuery
                    {
                       GovernateId = governateId
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = result.Governate;
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

