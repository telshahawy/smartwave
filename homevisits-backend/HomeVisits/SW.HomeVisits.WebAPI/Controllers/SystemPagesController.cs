using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.WebAPI.Helper;
using SW.HomeVisits.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SystemPagesController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public SystemPagesController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("GetSystemPagesTree")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetSystemPagesWithPermissionsTreeQueryResponse>), 200)]
        public async Task<IActionResult> GetSystemPagesTree()
        {
            var apiResponse = new HomeVisitsWebApiResponse<GetSystemPagesWithPermissionsTreeQueryResponse>();
            try
            {
                if (ModelState.IsValid)
                {
                    var getSystemPagesWithPermissionsTreeQuery = new GetSystemPagesWithPermissionsTreeQuery { CultureName = GetCultureName() };
                    var response = await _queryProcessor.ProcessQueryAsync<IGetSystemPagesWithPermissionsTreeQuery, IGetSystemPagesWithPermissionsTreeQueryResponse>(getSystemPagesWithPermissionsTreeQuery);
                    if (response == null || !response.SystemPages.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا توجد بيانات" : "No Items found";
                        return BadRequest(apiResponse);
                    }

                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = new GetSystemPagesWithPermissionsTreeQueryResponse
                    {
                        SystemPages = response.SystemPages
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
                apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                apiResponse.Message = ex.Message;
                return BadRequest(apiResponse);
            }
        }

        [HttpGet("GetSystemPagesKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetSystemPagesKeyValueQueryResponse>), 200)]
        public async Task<IActionResult> GetSystemPagesKeyValue()
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<GetSystemPagesKeyValueQueryResponse>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetSystemPageKeyValueQuery, IGetSystemPagesKeyValueQueryResponse>(new GetsystemPagesKeyValueQuery());
                    if (response == null || !response.SystemPages.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا توجد بيانات" : "No Items found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = new GetSystemPagesKeyValueQueryResponse
                    {
                        SystemPages = response.SystemPages
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
    }
}
