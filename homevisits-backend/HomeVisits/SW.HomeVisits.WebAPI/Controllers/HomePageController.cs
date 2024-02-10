using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.Framework.Extensions;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Notification;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.IdentityManagement.Models;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.WebAPI.CustomAttribute;
using SW.HomeVisits.WebAPI.Helper;
using SW.HomeVisits.WebAPI.Models;

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeByUserPermission]
    public class HomePageController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICultureService _cultureService;
        private readonly INotitificationService _notificationService;
        public HomePageController(ICommandBus commandBus, IQueryProcessor queryProcessor, ICultureService cultureService, INotitificationService notificationService)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _cultureService = cultureService;
            _notificationService = notificationService;
        }

        [HttpGet("GetAreasByUserId")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetUserAreasForHomePageQueryResponse>), 200)]
        public async Task<IActionResult> GetAreasByUserId(Guid? userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetUserAreasForHomePageQueryResponse>();
                    if (userId == null)
                    {
                        int userType = 0;
                        int.TryParse(User.Claims.FirstOrDefault(x => x.Type == nameof(ApplicationUser.UserType))?.Value, out userType);
                        var isClientAdmin = userType == (int)UserTypes.ClientAdmin;
                        if (isClientAdmin)
                        {
                            var adminresponse = await _queryProcessor.ProcessQueryAsync<IGetGeoZonesKeyValueQuery, IGetGeoZonesKeyValueQueryResponse>(new GetGeoZonesKeyValueQuery
                            {
                                CultureName = GetCultureName(),
                                ClientId = userInfo.ClientId
                            });
                            if (adminresponse == null || !adminresponse.GeoZones.Any())
                            {
                                response.ResponseCode = WebApiResponseCodes.Sucess;
                                response.Response = null;
                                response.Message = GetCultureName() == CultureNames.ar ? "لا توجد بيانات" : "No Items found";
                            }
                            response.ResponseCode = WebApiResponseCodes.Sucess;
                            response.Response = new GetUserAreasForHomePageQueryResponse
                            {
                                UserAreas = adminresponse.GeoZones.Select(x => new UserAreasDto()
                                {
                                    GeoZoneId = x.GeoZoneId,
                                    GeoZoneNameEn = x.Name,
                                    GeoZoneNameAr = x.Name

                                }).ToList()
                            };
                            return Ok(response);
                        }

                        var result = await _queryProcessor.ProcessQueryAsync<IGetUserAreasForHomePageQuery, IGetUserAreasForHomePageQueryResponse>(new GetUserAreasForHomePageQuery
                        {
                            UserId = userInfo.UserId
                        });
                        response.Response = new GetUserAreasForHomePageQueryResponse
                        {
                            UserAreas = result.UserAreas
                        };
                    }
                    else
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetUserAreasForHomePageQuery, IGetUserAreasForHomePageQueryResponse>(new GetUserAreasForHomePageQuery
                        {
                            UserId = (Guid)userId
                        });
                        response.Response = new GetUserAreasForHomePageQueryResponse
                        {
                            UserAreas = result.UserAreas
                        };
                    }

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpGet("GetTotalVisitsList")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchVisitsQueryResponse>), 200)]
        public async Task<IActionResult> GetTotalVisitsList(Guid? geozoneId, int? pageIndex, int? pageSize)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchVisitsQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetVisitHomePageQuery, ISearchVisitsQueryResponse>(new GetVisitHomePageQuery
                    {
                        GeoZoneId = geozoneId == null ? Guid.Empty : (Guid)geozoneId,
                        CurrentPageIndex = pageIndex,
                        PageSize = pageSize,
                        cultureName = GetCultureName()
                    });
                    response.Response = new SearchVisitsQueryResponse
                    {
                        Visits = result.Visits,
                        CurrentPageIndex = result.CurrentPageIndex,
                        PageSize = result.PageSize,
                        TotalCount = result.TotalCount
                    };

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpGet("GetDelayedVisitsList")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchVisitsQueryResponse>), 200)]
        public async Task<IActionResult> GetDelayedVisitsList(Guid? geozoneId, int? pageIndex, int? pageSize)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchVisitsQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetDelayedVisitsListHomePageQuery, ISearchVisitsQueryResponse>(new GetDelayedVisitsListHomePageQuery
                    {
                        GeoZoneId = geozoneId == null ? Guid.Empty : (Guid)geozoneId,
                        CurrentPageIndex = pageIndex,
                        PageSize = pageSize,
                        cultureName = GetCultureName()
                    });
                    response.Response = new SearchVisitsQueryResponse
                    {
                        Visits = result.Visits,
                        CurrentPageIndex = result.CurrentPageIndex,
                        PageSize = result.PageSize,
                        TotalCount = result.TotalCount
                    };

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpGet("GetPendingVisitsList")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchVisitsQueryResponse>), 200)]
        public async Task<IActionResult> GetPendingVisitsList(Guid? geozoneId, int? pageIndex, int? pageSize)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchVisitsQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetPendingVisitsListHomePageQuery, ISearchVisitsQueryResponse>(new GetPendingVisitsListHomePageQuery
                    {
                        GeoZoneId = geozoneId == null ? Guid.Empty : (Guid)geozoneId,
                        CurrentPageIndex = pageIndex,
                        PageSize = pageSize,
                        cultureName = GetCultureName()
                    });
                    response.Response = new SearchVisitsQueryResponse
                    {
                        Visits = result.Visits,
                        CurrentPageIndex = result.CurrentPageIndex,
                        PageSize = result.PageSize,
                        TotalCount = result.TotalCount
                    };

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpGet("GetReassignedVisitsList")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<SearchVisitsQueryResponse>), 200)]
        public async Task<IActionResult> GetReassignedVisitsList(Guid? geozoneId, int? pageIndex, int? pageSize)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<SearchVisitsQueryResponse>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetReassignedVisitsListHomePageQuery, ISearchVisitsQueryResponse>(new GetReassignedVisitsList
                    {
                        GeoZoneId = geozoneId == null ? Guid.Empty : (Guid)geozoneId,
                        CurrentPageIndex = pageIndex,
                        PageSize = pageSize,
                        cultureName = GetCultureName()
                    });

                    response.Response = new SearchVisitsQueryResponse
                    {
                        Visits = result.Visits,
                        CurrentPageIndex = result.CurrentPageIndex,
                        PageSize = result.PageSize,
                        TotalCount = result.TotalCount
                    };

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpGet("GetActiveChemists")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetActiveChemistHomePageQueryResponse>), 200)]
        public async Task<IActionResult> GetActiveChemists(Guid? geozoneId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetActiveChemistHomePageQueryResponse>();
                    if (geozoneId == null)
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetActiveChemistHomePageQuery, IGetActiveChemistHomePageQueryResponse>(new GetActiveChemistHomePageQuery
                        {
                            GeoZoneId = Guid.Empty
                        });
                        response.Response = new GetActiveChemistHomePageQueryResponse
                        {
                            ActiveChemistNames = result.ActiveChemistNames
                        };
                    }
                    else
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetActiveChemistHomePageQuery, IGetActiveChemistHomePageQueryResponse>(new GetActiveChemistHomePageQuery
                        {
                            GeoZoneId = (Guid)geozoneId
                        });
                        response.Response = new GetActiveChemistHomePageQueryResponse
                        {
                            ActiveChemistNames = result.ActiveChemistNames
                        };
                    }

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpGet("GetIdleChemists")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetIdleChemistHomePageQueryResponse>), 200)]
        public async Task<IActionResult> GetIdleChemists(Guid? geozoneId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetIdleChemistHomePageQueryResponse>();
                    if (geozoneId == null)
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetIdleChemistHomePageQuery, IGetIdleChemistHomePageQueryResponse>(new GetIdleChemistHomePageQuery
                        {
                            GeoZoneId = Guid.Empty
                        });
                        response.Response = new GetIdleChemistHomePageQueryResponse
                        {
                            IdleChemistNames = result.IdleChemistNames
                        };
                    }
                    else
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetIdleChemistHomePageQuery, IGetIdleChemistHomePageQueryResponse>(new GetIdleChemistHomePageQuery
                        {
                            GeoZoneId = (Guid)geozoneId
                        });
                        response.Response = new GetIdleChemistHomePageQueryResponse
                        {
                            IdleChemistNames = result.IdleChemistNames
                        };
                    }

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    return Ok(response);
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

        [HttpGet("GetHomePageStatistics")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetVisitsHomePageQueryResponse>), 200)]
        public async Task<IActionResult> GetHomePageStatistics(Guid? geozoneId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetVisitsHomePageQueryResponse>();
                    if (geozoneId == null)
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetVisitsHomePageQuery, IGetVisitsHomePageQueryResponse>(new GetVisitsHomePageQuery
                        {
                            GeoZoneId = Guid.Empty
                        });
                        response.Response = new GetVisitsHomePageQueryResponse
                        {
                            DoneVisitsNo = result.DoneVisitsNo,
                            AllVisitsNo = result.AllVisitsNo,
                            CanceledVisitsNo = result.CanceledVisitsNo,
                            ConfirmedVisitsNo = result.ConfirmedVisitsNo,
                            DelayedVisitsNo = result.DelayedVisitsNo,
                            PendingVisitsNo = result.PendingVisitsNo,
                            ReassignedVisitsNo = result.ReassignedVisitsNo,
                            RejectedVisitsNo = result.RejectedVisitsNo,
                            AllChemistNo = result.AllChemistNo,
                            ActiveChemistNo = result.ActiveChemistNo,
                            IdleChemistNo = result.IdleChemistNo
                        };
                    }
                    else
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetVisitsHomePageQuery, IGetVisitsHomePageQueryResponse>(new GetVisitsHomePageQuery
                        {
                            GeoZoneId = geozoneId ?? Guid.Empty
                        });
                        response.Response = new GetVisitsHomePageQueryResponse
                        {
                            DoneVisitsNo = result.DoneVisitsNo,
                            AllVisitsNo = result.AllVisitsNo,
                            CanceledVisitsNo = result.CanceledVisitsNo,
                            ConfirmedVisitsNo = result.ConfirmedVisitsNo,
                            DelayedVisitsNo = result.DelayedVisitsNo,
                            PendingVisitsNo = result.PendingVisitsNo,
                            ReassignedVisitsNo = result.ReassignedVisitsNo,
                            RejectedVisitsNo = result.RejectedVisitsNo,
                            AllChemistNo = result.AllChemistNo,
                            ActiveChemistNo = result.ActiveChemistNo,
                            IdleChemistNo = result.IdleChemistNo
                        };
                    }

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
                throw new Exception(GetCultureName() == CultureNames.ar ? "حدث خطاء" : "error", ex);
            }
        }
    }
}