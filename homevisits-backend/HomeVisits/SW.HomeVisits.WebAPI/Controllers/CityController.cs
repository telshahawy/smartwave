using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.WebAPI.Models;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Authentication ;
namespace SW.HomeVisits.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAuthenticationManager _authenticationManager;

        public CityController(ICommandBus commandBus, IQueryProcessor queryProcessor,IAuthenticationManager authenticationManager)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _authenticationManager = authenticationManager;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCityCommand model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                 
                    await _commandBus.SendAsync((ICreateCityCommand)model);
                    return Ok("Success");
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid cityId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   //await _authenticationManager.CreateAsync();
                   var city =  await _queryProcessor.ProcessQueryAsync<IGetCityQuery,IGetCityQueryResponse>(new GetCityQuery
                    {
                        CityId = cityId
                    });
                    return Ok(city);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
