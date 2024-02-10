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
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.WebAPI.Helper;
using SW.HomeVisits.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAuthenticationManager _authenticationManager;

        public RolesController(ICommandBus commandBus, IQueryProcessor queryProcessor, IAuthenticationManager authenticationManager)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _authenticationManager = authenticationManager;
        }
        [HttpPost("CreateRole")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<Guid>();
                    var user = GetCurrentUserId();
                   
                    var createRoleCommand = new CreateRoleCommand
                    {
                        RoleId = Guid.NewGuid(),
                        ClientId = user.ClientId.Value,
                        CreatedBy = user.UserId,
                        Description = model.Description,
                        IsActive = model.IsActive,
                        NameAr = model.Name,
                        DefaultPageId = model.DefaultPageId,
                        GeoZones = model.GeoZones,
                        Permissions = model.Permissions
                    };
                    await _commandBus.SendAsync((ICreateRoleCommand)createRoleCommand);
                    response.Response = createRoleCommand.RoleId;
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

        [HttpGet("GetRoleForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetRoleForEditQueryResponse>), 200)]
        public async Task<IActionResult> GetRoleForEdit([FromQuery] Guid roleId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetRoleForEditQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<IGetRoleForEditQuery, IGetRoleForEditQueryResponse>(new GetRoleForEditQuery
                    {
                        RoleId = roleId
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetRoleForEditQueryResponse
                    {
                        Role = result.Role
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

        [HttpGet("SearchRoles")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchRolesQueryResponse>), 200)]
        public async Task<IActionResult> SearchRoles([FromQuery] SearchRolesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchRolesQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchRolesQuery, ISearchRolesQueryResponse>(new SearchRolesQuery
                    {
                        Code = model.Code,
                        IsActive = model.IsActive,
                        Name = model.Name,
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchRolesQueryResponse
                    {

                        Roles = result.Roles,
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
                throw new Exception(GetCultureName() == CultureNames.ar ? "حدث خطاء" : "error");
            }

        }

        [HttpPut("UpdateRole")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateRole([FromQuery] Guid roleId,[FromBody] UpdateRoleModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();
            var user = GetCurrentUserId();
            try
            {
                if (ModelState.IsValid)
                {

                    var updateRoleCommand = new UpdateRoleCommand
                    {
                        Client = user.ClientId.GetValueOrDefault(),
                        DefaultPageId = model.DefaultPageId,
                        Description = model.Description,
                        GeoZones = model.GeoZones,
                        IsActive = model.IsActive,
                        NameAr = model.Name,
                        Permissions = model.Permissions,
                        RoleId = roleId,
                    };
                    await _commandBus.SendAsync((IUpdateRoleCommand)updateRoleCommand);

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

        [HttpDelete("DeleteRole")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteRole([FromQuery] Guid roleId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteRoleCommand)new DeleteRoleCommand
                    {
                        RoleId = roleId
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

        [HttpGet("RolesKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetRolesKeyValueQueryResponse>), 200)]
        public async Task<IActionResult> RolesKeyValue()
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<GetRolesKeyValueQueryResponse>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetRolesKeyValueQuery, IGetRolesKeyValueQueryResponse>(new GetRolesKeyValueQuery
                    {
                        
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    if (response == null || !response.Roles.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا توجد بيانات" : "No Items found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = new GetRolesKeyValueQueryResponse
                    {
                        Roles = response.Roles
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
