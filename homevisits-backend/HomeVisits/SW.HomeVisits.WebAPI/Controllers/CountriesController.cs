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
    public class CountriesController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public CountriesController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("CountriesKeyValue")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetCountriesKeyValueQueryResponse>), 200)]
        public async Task<IActionResult> CountriesKeyValue()
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<GetCountriesKeyValueQueryResponse>();
                if (ModelState.IsValid)
                {
                    var response = await _queryProcessor.ProcessQueryAsync<IGetCountriesKeyValueQuery, IGetCountriesKeyValueQueryResponse>(new GetCountriesKeyValueQuery
                    {
                        CultureName = GetCultureName(),
                        ClientId = userInfo.ClientId
                    });
                    if (response == null || !response.Countries.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = null;
                        apiResponse.Message = GetCultureName() == CultureNames.ar ? "لا توجد بيانات" : "No Items found";
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = new GetCountriesKeyValueQueryResponse {
                        Countries = response.Countries
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

        [HttpGet("SearchCountries")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchCountriesQueryResponse>), 200)]
        public async Task<IActionResult> SearchCountries([FromQuery] SearchCountriesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchCountriesQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<ISearchCountriesQuery, ISearchCountriesQueryResponse>(new SearchCountriesQuery
                    {
                        Code = model.Code,
                        Name = model.Name,
                        IsActive = model.IsActive,
                        CurrentPageIndex = model.CurrentPageIndex,
                        PageSize = model.PageSize,
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = new SearchCountriesQueryResponse
                    {

                        Countries = result.Countries,
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

        [HttpPost("CreateCountry")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryModel model)
        {
            var response = new HomeVisitsWebApiResponse<Guid>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();

                    var createCountryCommand = new CreateCountryCommand
                    {
                        CountryId = Guid.NewGuid(),
                        CountryNameEn = model.CountryNameEn,
                        CountryNameAr = model.CountryNameEn,
                        MobileNumberLength = model.MobileNumberLength,
                        ClientId = userInfo.ClientId.Value,
                        IsActive = model.IsActive,
                        CreatedBy = userInfo.UserId
                    };

                    await _commandBus.SendAsync((ICreateCountryCommand)createCountryCommand);

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Message = "Data saved successfully";
                    response.Response = createCountryCommand.CountryId;
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

        [HttpPut("UpdateCountry")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateCountry([FromQuery] Guid countryId, [FromBody] UpdateCountryModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {

                    var updateCountryCommand = new UpdateCountryCommand
                    {
                        CountryId = countryId,
                        CountryNameEn = model.CountryNameEn,
                        MobileNumberLength = model.MobileNumberLength,
                        IsActive = model.IsActive,
                        ClientId = GetCurrentUserId().ClientId.Value
                    };

                    await _commandBus.SendAsync((IUpdateCountryCommand)updateCountryCommand);

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

        [HttpGet("GetCountryForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<CountriesDto>), 200)]
        public async Task<IActionResult> GetCountryForEdit([FromQuery] Guid countryId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<CountriesDto>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetCountryForEditQuery, IGetCountryForEditQueryResponse>(new GetCountryForEditQuery
                    {
                        CountryId = countryId
                    });
                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = result.Country;
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

        [HttpDelete("DeleteCountry")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteCountry([FromQuery] Guid countryId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeleteCountryCommand)new DeleteCountryCommand
                    {
                        CountryId = countryId
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
