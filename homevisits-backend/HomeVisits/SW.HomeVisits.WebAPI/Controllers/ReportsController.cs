using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.WebAPI.Helper;
using SW.HomeVisits.WebAPI.Models;

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : HomeVisitsControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICultureService _cultureService;
        public ReportsController(IQueryProcessor queryProcessor, ICultureService cultureService)
        {
            _queryProcessor = queryProcessor;
            _cultureService = cultureService;
        }


        [HttpGet("GetVisitReport/Detailed")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetVisitReportQueryResponse>), 200)]
        public async Task<IActionResult> GetDetailedVisitReport(DateTime visitDateFrom, DateTime visitDateTo,
            string delayed, Guid? countryId, Guid? governorateId, Guid? areaId, Guid? chemistId, bool showDetails, int? CurrentPageIndex, int? PageSize)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetVisitReportQueryResponse>();

                    if (visitDateFrom != null && visitDateTo != null)
                    {
                        var _CountryOption = countryId == null ? Guid.Empty : (Guid)countryId;
                        var _ChemistOption = chemistId == null ? Guid.Empty : (Guid)chemistId;
                        var _GovernorateOption = governorateId == null ? Guid.Empty : (Guid)governorateId;
                        var _AreaOption = areaId == null ? Guid.Empty : (Guid)areaId;

                        var _delayed = delayed ?? "All";

                        if (showDetails)
                        {
                            var result = await _queryProcessor.ProcessQueryAsync<IGetVisitReportQuery, IGetVisitReportQueryResponse>(new GetVisitReportQuery
                            {
                                VisitDateFrom = visitDateFrom,
                                VisitDateTo = visitDateTo,
                                CountryOption = _CountryOption,
                                ChemistOption = _ChemistOption,
                                DelayedOption = _delayed,
                                GovernorateOption = _GovernorateOption,
                                AreaOption = _AreaOption,
                                ShowDetails = showDetails,
                                UserId = userInfo.UserId,
                                cultureName = GetCultureName(),
                                CurrentPageIndex = CurrentPageIndex,
                                PageSize = PageSize
                            });

                            response.Response = new GetVisitReportQueryResponse
                            {
                                VisitDateFrom = result.VisitDateFrom,
                                VisitDateTo = result.VisitDateTo,
                                CountryOption = result.CountryOption,
                                GovernorateOption = result.GovernorateOption,
                                AreaOption = result.AreaOption,
                                ChemistOption = result.ChemistOption,
                                DelayedOption = result.DelayedOption,
                                TotalVisitsNo = result.TotalVisitsNo,
                                PrintedBy = result.PrintedBy,
                                PrintedDate = result.PrintedDate,
                                VisitReports = result.VisitReports,
                                CurrentPageIndex = result.CurrentPageIndex,
                                PageSize = result.PageSize,
                                TotalCount = result.TotalCount

                            };

                            response.ResponseCode = WebApiResponseCodes.Sucess;

                            return Ok(response);
                        }
                        else
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "It seems you wanna get GetVisitReport/Total try the correct API";
                            return BadRequest(response);
                        }

                    }
                    else
                    {
                        response.ResponseCode = WebApiResponseCodes.Failer;
                        response.Message = "Date interval is required";
                        return BadRequest(response);
                    }

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

        [HttpGet("GetVisitReport/Total")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetNonDetailedVisitReportQueryResponse>), 200)]
        public async Task<IActionResult> GetTotalVisitReport(DateTime visitDateFrom, DateTime visitDateTo,
            string delayed, Guid? countryId, Guid? governorateId, Guid? areaId, Guid? chemistId, bool showDetails, int? CurrentPageIndex, int? PageSize)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();

                    var responseApi = new HomeVisitsWebApiResponse<GetNonDetailedVisitReportQueryResponse>();
                    if (!showDetails)
                    {
                        if (visitDateFrom != null && visitDateTo != null)
                        {
                            var _CountryOption = countryId == null ? Guid.Empty : (Guid)countryId;
                            var _ChemistOption = chemistId == null ? Guid.Empty : (Guid)chemistId;
                            var _GovernorateOption = governorateId == null ? Guid.Empty : (Guid)governorateId;
                            var _AreaOption = areaId == null ? Guid.Empty : (Guid)areaId;
                            var _delayed = delayed ?? "All";
                            var result = await _queryProcessor.ProcessQueryAsync<IGetVisitReportQuery, IGetNonDetailedVisitReportQueryResponse>(new GetVisitReportQuery
                            {
                                VisitDateFrom = visitDateFrom,
                                VisitDateTo = visitDateTo,
                                CountryOption = _CountryOption,
                                ChemistOption = _ChemistOption,
                                DelayedOption = _delayed,
                                GovernorateOption = _GovernorateOption,
                                AreaOption = _AreaOption,
                                ShowDetails = showDetails,
                                UserId = userInfo.UserId,
                                cultureName = GetCultureName(),
                                CurrentPageIndex = CurrentPageIndex,
                                PageSize = PageSize
                            });

                            responseApi.Response = new GetNonDetailedVisitReportQueryResponse
                            {
                                VisitDateFrom = result.VisitDateFrom,
                                VisitDateTo = result.VisitDateTo,
                                CountryOption = result.CountryOption,
                                GovernorateOption = result.GovernorateOption,
                                AreaOption = result.AreaOption,
                                ChemistOption = result.ChemistOption,
                                DelayedOption = result.DelayedOption,
                                TotalVisitsNo = result.TotalVisitsNo,
                                NonDetailedVisitReports = result.NonDetailedVisitReports,
                                PrintedBy = result.PrintedBy,
                                PrintedDate = result.PrintedDate,
                                CurrentPageIndex = result.CurrentPageIndex,
                                PageSize = result.PageSize,
                                TotalCount = result.TotalCount
                            };

                            responseApi.ResponseCode = WebApiResponseCodes.Sucess;
                            return Ok(responseApi);


                        }
                        else
                        {
                            responseApi.ResponseCode = WebApiResponseCodes.Failer;
                            responseApi.Message = "Date interval is required";
                            return BadRequest(responseApi);
                        }
                    }
                    else
                    {
                        responseApi.ResponseCode = WebApiResponseCodes.Failer;
                        responseApi.Message = "It seems you wanna get GetVisitReport/Detailed try the correct API";
                        return BadRequest(responseApi);
                    }
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

        [HttpGet("GetCanceledVisitReport")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetCanceledVisitReportQueryResponse>), 200)]
        public async Task<IActionResult> GetCanceledVisitReport(DateTime visitDateFrom, DateTime visitDateTo,
            Guid? countryId, Guid? governorateId, Guid? areaId, int? cancellationReason, int? CurrentPageIndex, int? PageSize)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetCanceledVisitReportQueryResponse>();

                    if (visitDateFrom != null && visitDateTo != null)
                    {
                        var _CountryOption = countryId == null ? Guid.Empty : (Guid)countryId;
                        var _GovernorateOption = governorateId == null ? Guid.Empty : (Guid)governorateId;
                        var _AreaOption = areaId == null ? Guid.Empty : (Guid)areaId;
                        var _CancellationReason = cancellationReason == null ? -1 : cancellationReason;

                        var result = await _queryProcessor.ProcessQueryAsync<IGetCanceledVisitReportQuery, IGetCanceledVisitReportQueryResponse>(new GetCanceledVisitReportQuery
                        {
                            VisitDateFrom = visitDateFrom,
                            VisitDateTo = visitDateTo,
                            CountryOption = _CountryOption,
                            CancellationReason = _CancellationReason,
                            GovernorateOption = _GovernorateOption,
                            AreaOption = _AreaOption,
                            UserId = userInfo.UserId,
                            cultureName = GetCultureName(),
                            CurrentPageIndex = CurrentPageIndex,
                            PageSize = PageSize
                        });

                        response.Response = new GetCanceledVisitReportQueryResponse
                        {
                            DateFrom = result.DateFrom,
                            DateTo = result.DateTo,
                            Country = result.Country,
                            Governorate = result.Governorate,
                            Area = result.Area,
                            canceledVisitReports = result.canceledVisitReports,
                            reason = result.reason,
                            PrintedBy = result.PrintedBy,
                            PrintedDate = result.PrintedDate,
                            CurrentPageIndex = result.CurrentPageIndex,
                            PageSize = result.PageSize,
                            TotalCount = result.TotalCount

                        };

                        response.ResponseCode = WebApiResponseCodes.Sucess;

                        return Ok(response);
                    }

                    else
                    {
                        response.ResponseCode = WebApiResponseCodes.Failer;
                        response.Message = "Date interval is required";
                        return BadRequest(response);
                    }

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw new Exception(GetCultureName() == CultureNames.ar ? "حدث خطاء" : "error");
            }
        }


        [HttpGet("GetRejectedVisitReport")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetCanceledVisitReportQueryResponse>), 200)]
        public async Task<IActionResult> GetRejectedVisitReport(DateTime visitDateFrom, DateTime visitDateTo,
                    Guid? countryId, Guid? governorateId, Guid? areaId, Guid? chemistId, int? reason, string delayed
            , int? CurrentPageIndex, int? PageSize)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<IGetRejectedVisitReportQueryResponse>();

                    if (visitDateFrom != null && visitDateTo != null)
                    {
                        var _CountryOption = countryId == null ? Guid.Empty : (Guid)countryId;
                        var _GovernorateOption = governorateId == null ? Guid.Empty : (Guid)governorateId;
                        var _AreaOption = areaId == null ? Guid.Empty : (Guid)areaId;
                        var _Reason = reason == null ? -1 : reason;
                        var _ChemistOption = chemistId == null ? Guid.Empty : (Guid)chemistId;
                        var _delayed = delayed ?? "All";

                        var result = await _queryProcessor.ProcessQueryAsync<IGetRejectedVisitReportQuery, IGetRejectedVisitReportQueryResponse>(new GetRejectedVisitReportQuery
                        {
                            VisitDateFrom = visitDateFrom,
                            VisitDateTo = visitDateTo,
                            CountryOption = _CountryOption,
                            Reason = _Reason,
                            ChemistOption = _ChemistOption,
                            DelayedOption = _delayed,
                            GovernorateOption = _GovernorateOption,
                            AreaOption = _AreaOption,
                            UserId = userInfo.UserId,
                            cultureName = GetCultureName(),
                            CurrentPageIndex = CurrentPageIndex,
                            PageSize = PageSize
                        });

                        response.Response = new GetRejectedVisitReportQueryResponse
                        {
                            DateFrom = result.DateFrom,
                            DateTo = result.DateTo,
                            Country = result.Country,
                            Governorate = result.Governorate,
                            Area = result.Area,
                            RejectedVisitReports = result.RejectedVisitReports,
                            Reason = result.Reason,
                            CancelledVisitsNo = result.CancelledVisitsNo,
                            Chemist = result.Chemist,
                            Delayed = result.Delayed,
                            ReassignedVisitsNo = result.ReassignedVisitsNo,
                            TotalVisitsNo = result.TotalVisitsNo,
                            PrintedBy = result.PrintedBy,
                            PrintedDate = result.PrintedDate,
                            CurrentPageIndex = result.CurrentPageIndex,
                            PageSize = result.PageSize,
                            TotalCount = result.TotalCount
                        };

                        response.ResponseCode = WebApiResponseCodes.Sucess;

                        return Ok(response);
                    }

                    else
                    {
                        response.ResponseCode = WebApiResponseCodes.Failer;
                        response.Message = "Date interval is required";
                        return BadRequest(response);
                    }

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw new Exception(GetCultureName() == CultureNames.ar ? "حدث خطاء" : "error");
            }
        }


    }
}