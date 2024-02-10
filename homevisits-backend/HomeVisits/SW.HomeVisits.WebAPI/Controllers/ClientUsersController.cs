using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract;
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
    public class ClientUsersController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EzagelSmsConfiguration _ezagelSmsConfiguration;

        public ClientUsersController(ICommandBus commandBus, IQueryProcessor queryProcessor, IAuthenticationManager authenticationManager, IOptions<EzagelSmsConfiguration> settings)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _ezagelSmsConfiguration = settings.Value;
            _authenticationManager = authenticationManager;
        }

        [HttpPost("CreateClientUser")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateClientUser([FromBody] CreateClientUserModel model)
        {
            var response = new HomeVisitsWebApiResponse<Guid>();
            try
            {
                if (ModelState.IsValid)
                {
                    var user = GetCurrentUserId();
                    var createClientUserCommand = new CreateClientUserCommand
                    {
                        ClientId = user.ClientId.GetValueOrDefault(),
                        CreateBy = user.UserId,
                        UserId = Guid.NewGuid(),
                        GeoZones = model.GeoZones,
                        IsActive = model.IsActive,
                        Name = model.Name,
                        Password = model.Password,
                        PhoneNumber = model.PhoneNumber,
                        RoleId = model.RoleId,
                        UserName = model.UserName,
                        Permissions = model.Permissions
                    };
                    await _commandBus.SendAsync((ICreateClientUserCommand)createClientUserCommand);

                    if (model.SendCredentials)
                    {
                        if (!model.PhoneNumber.StartsWith("+2"))
                            model.PhoneNumber = "%2B2" + model.PhoneNumber; //use %2B becuase in ezagel server + decode to space and %2B Decode to +
                        if (model.PhoneNumber.StartsWith("+"))
                            model.PhoneNumber = model.PhoneNumber.Replace("+", "%2B");

                        HttpClient client = new HttpClient();
                        var url = string.Format($"{_ezagelSmsConfiguration.EndPoindAddress}?{_ezagelSmsConfiguration.EndPoindParameter}", Guid.NewGuid().ToString(),
                            model.PhoneNumber, string.Format(_ezagelSmsConfiguration.CredentialSmsBody, model.UserName, model.Password),
                            string.Empty, string.Empty, _ezagelSmsConfiguration.SMSSenderName, _ezagelSmsConfiguration.SMSUserName,
                            _ezagelSmsConfiguration.SMSPassword, _ezagelSmsConfiguration.SMSService);
                        HttpResponseMessage wcfResponse = client.GetAsync(url).Result;

                        HttpContent stream = wcfResponse.Content;
                        var data = stream.ReadAsStringAsync();

                        //EzagelSmsServive.ServiceSoapClient serviceSoapClient = new EzagelSmsServive.ServiceSoapClient(EzagelSmsServive.ServiceSoapClient.EndpointConfiguration.ServiceSoap);
                        //var smsResponse = serviceSoapClient.Send_SMS_PortAsync(Guid.NewGuid().ToString(), model.PhoneNumber, string.Format(_ezagelSmsConfiguration.CredentialSmsBody, model.UserName, model.Password), string.Empty, DateTime.Now.ToString("yyyyMMddHHmmss"), _ezagelSmsConfiguration.SMSSenderName, _ezagelSmsConfiguration.SMSUserName, _ezagelSmsConfiguration.SMSPassword, _ezagelSmsConfiguration.SMSService, _ezagelSmsConfiguration.SMSPort).Result;
                    }

                    response.Response = createClientUserCommand.UserId;
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
                response.Message = ex.Message;
                response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                return BadRequest(response);
            }

        }

        [HttpGet("GetClientUserForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetClientUserForEditQueryResponse>), 200)]
        public async Task<IActionResult> GetClientUserForEdit([FromQuery] Guid userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetClientUserForEditQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<IGetClientUserForEditQuery, IGetClientUserForEditQueryResponse>(new GetClientUserForEditQuery
                    {
                        UserId = userId,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetClientUserForEditQueryResponse
                    {
                        User = result.User
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

        [HttpGet("SearchClientUsers")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchClientUserQueryResponse>), 200)]
        public async Task<IActionResult> SearchClientUsers([FromQuery] SearchRolesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchClientUserQueryResponse>();
                    //await _authenticationManager.CreateAsync();
                    var result = await _queryProcessor.ProcessQueryAsync<ISearchClientUserQuery, ISearchClientUserQueryResponse>(new SearchClientUserQuery
                    {
                        Code = model.Code,
                        IsActive = model.IsActive,
                        Name = model.Name,
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                        PhoneNumber = model.PhoneNumber
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchClientUserQueryResponse
                    {

                        Users = result.Users,
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

        [HttpPut("UpdateClientUser")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateClientUser([FromQuery] Guid userId, [FromBody] UpdateClientUserModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();
            var user = GetCurrentUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var updateClientUserCommand = new UpdateClientUserCommand
                    {
                        UserId = userId,
                        Name = model.Name,
                        RoleId = model.RoleId,
                        IsActive = model.IsActive,
                        PhoneNumber = model.PhoneNumber,
                        GeoZonesIds = model.GeoZonesIds,
                        Permissions = model.Permissions
                    };
                    await _commandBus.SendAsync((IUpdateClientUserCommand)updateClientUserCommand);

                    response.Response = true;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpPut("UpdateClientUserPermission")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateClientUserPermission([FromQuery] Guid userId, [FromBody] UpdateClientUserPermissionModel model)
        {
            var user = GetCurrentUserId();
            var response = new HomeVisitsWebApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var updateClientUserPermissionCommand = new UpdateClientUserPermissionCommand
                    {
                        UserId = userId,
                        ClientId = user.ClientId.Value,
                        Permissions = model.Permissions
                    };
                    await _commandBus.SendAsync((IUpdateClientUserPermissionCommand)updateClientUserPermissionCommand);

                    response.Response = true;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpGet("GetUserPermission")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetUserPermissionQueryResponse>), 200)]
        public async Task<IActionResult> GetUserPermission([FromQuery] GetUserPermissionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetUserPermissionQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetUserPermissionQuery, IGetUserPermissionQueryResponse>(new GetUserPermissionQuery
                    {
                        UserId = model.UserId,
                        ClientId = model.ClientId.HasValue ? model.ClientId.Value : userInfo.ClientId.GetValueOrDefault()
                    });

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetUserPermissionQueryResponse
                    {
                        SystemPagePermissionDtos = result.SystemPagePermissionDtos
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

        [HttpGet("GetUserSystemPageWithPermissionsTree")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetUserPermissionQueryResponse>), 200)]
        public async Task<IActionResult> GetUserSystemPageWithPermissionsTree([FromQuery] GetUserPermissionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetUserSystemPageWithPermissionsTreeQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetUserSystemPageWithPermissionsTreeQuery, IGetUserSystemPageWithPermissionsTreeQueryResponse>(new GetUserSystemPageWithPermissionsTreeQuery
                    {
                        UserId = model.UserId,
                        CultureName = GetCultureName(),
                        ClientId = model.ClientId.HasValue ? model.ClientId.Value : userInfo.ClientId.GetValueOrDefault()
                    });

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new GetUserSystemPageWithPermissionsTreeQueryResponse
                    {
                        SystemPages = result.SystemPages
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

        [HttpDelete("DeleteClientUser")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteClientUser([FromQuery] Guid userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteClientuserCommand)new DeleteClientUserCommand
                    {
                        UserId = userId
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
    }
}
