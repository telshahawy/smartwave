using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.WebAPI.Helper;
using SW.HomeVisits.WebAPI.Models;

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public ClientController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }
        [HttpPost("AddClient")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> AddClient([FromBody] AddClientModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<Guid>();

                if (ModelState.IsValid)
                {
                    //var userInfo = GetCurrentUserId();
                    Guid g = Guid.NewGuid();
                    var addClientCommand = new AddClientCommand
                    {
                        ClientId = Guid.NewGuid(),
                        
                        //ClientId = g,
                        ClientName = model.ClientName,
                        IsActive = model.IsActive,
                        ClientCode = model.ClientCode,
                        URLName = model.URLName,
                        DisplayName = model.DisplayName,
                        Logo = model.Logo,
                        CountryId = model.CountryId,
                    };
                    await _commandBus.SendAsync((IAddClientCommand)addClientCommand);


                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Message = "Data saved successfully";
                    response.Response = addClientCommand.ClientId;
                    return Ok(response);
                }
                else
                {
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    response.Message = "Invalid Input Parameter";
                   // response.Response = false;
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPut("UpdateClient")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateClient([FromQuery] Guid ClientId, [FromBody] UpdateClientModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {

                    var updateClientCommand = new UpdateClientCommand
                    {
                        ClientId = ClientId,
                        ClientName = model.ClientName,
                        ClientCode = model.ClientCode,
                        URLName = model.URLName,
                        DisplayName = model.DisplayName,
                        Logo = model.Logo,
                        CountryId = model.CountryId,
                        IsActive = model.IsActive,
                    };

                    await _commandBus.SendAsync((IUpdateClientCommand)updateClientCommand);

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

        [HttpDelete("DeleteClient")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteClient([FromQuery] Guid clientId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteClientCommand)new DeleteClientCommand
                    {
                        ClientId = clientId
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