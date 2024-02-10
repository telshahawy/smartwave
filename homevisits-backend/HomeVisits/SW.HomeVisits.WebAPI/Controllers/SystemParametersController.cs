using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.Framework.Exceptions;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Notification;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.HomeVisits.Application.Models;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.WebAPI.CustomAttribute;
using SW.HomeVisits.WebAPI.Helper;
using SW.HomeVisits.WebAPI.Models;

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/Setup/[controller]")]
    [ApiController]
    [AuthorizeByUserPermission]
    public class SystemParametersController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICultureService _cultureService;
        private readonly INotitificationService _notificationService;

        public SystemParametersController(ICommandBus commandBus, IQueryProcessor queryProcessor, ICultureService cultureService, INotitificationService notificationService)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _cultureService = cultureService;
            _notificationService = notificationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> CreateSystemParemeters([FromBody] CreateSystemParametersModel model)
        {

            try
            {
                var apiResponse = new HomeVisitsWebApiResponse<Guid>();
                var userInfo = GetCurrentUserId();
                if (userInfo.ClientId != Guid.Empty)
                {

                    if (ModelState.IsValid)
                    {
                        if (model.IsOptimizezonebefore == true && model.OptimizezonebeforeInMin == null)
                        {
                            apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                            apiResponse.Message = "Optimize zone before is missing";
                            apiResponse.Response = Guid.Empty;
                            return BadRequest(apiResponse);
                        }

                        if (model.EstimatedVisitDurationInMin > 0 && model.EstimatedVisitDurationInMin <= 999)
                        {
                            if (model.NextReserveHomevisitInDay > 0 && model.NextReserveHomevisitInDay <= 99)
                            {

                                if (model.VisitApprovalBy != null && model.VisitCancelBy != null)
                                {
                                    //var file= UploadPrecautionsFile();

                                    var createSystemParameter = new CreateSystemParametersCommand
                                    {
                                        //SystemParametersId = Guid.NewGuid(),
                                        CreateBy = userInfo.UserId,
                                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                                        EstimatedVisitDurationInMin = model.EstimatedVisitDurationInMin,
                                        NextReserveHomevisitInDay = model.NextReserveHomevisitInDay,
                                        RoutingSlotDurationInMin = model.RoutingSlotDurationInMin,
                                        VisitApprovalBy = model.VisitApprovalBy,
                                        VisitCancelBy = model.VisitCancelBy,
                                        DefaultCountryId = model.DefaultCountryId,
                                        DefaultGovernorateId = model.DefaultGovernorateId,
                                        IsOptimizezonebefore = model.IsOptimizezonebefore,
                                        IsSendPatientTimeConfirmation = model.IsSendPatientTimeConfirmation,
                                        OptimizezonebeforeInMin = model.OptimizezonebeforeInMin,
                                        CallCenterNumber = model.CallCenterNumber,
                                        WhatsappBusinessLink = model.WhatsappBusinessLink,
                                        PrecautionsFile = model.PrecautionsFile,
                                        FileName = model.FileName
                                    };
                                    await _commandBus.SendAsync((ICreateSystemParametersCommand)createSystemParameter);
                                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                                    apiResponse.Response = createSystemParameter.ClientId;
                                    //apiResponse.Message = Resources.Resource.ResourceManager.GetString("Successfully added", GetCulture());
                                    apiResponse.Message = "Data saved successfully";
                                    return Ok(apiResponse);
                                }
                                else
                                {
                                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                    apiResponse.Message = "Please fill the required data";
                                    return BadRequest(apiResponse);
                                }
                            }
                            else
                            {
                                apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                                apiResponse.Message = "Invalid Home Visit reservation available days";
                                return BadRequest(apiResponse);
                            }
                        }
                        else
                        {
                            apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                            apiResponse.Message = "Invalid Estimated Visit Duration";
                            return BadRequest(apiResponse);
                        }


                    }
                    else
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = "Invalide Input Parameter";
                        return BadRequest(apiResponse);
                    }
                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "No Client Signed";
                    apiResponse.Response = Guid.Empty;
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("UploadPrecautionsFile")]
        [DisableRequestSizeLimitAttribute]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<string>), 200)]
        public IActionResult UploadPrecautionsFile()
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<string>();
                var userInfo = GetCurrentUserId();
                var file = Request.Form.Files[0];


                var folderName = Path.Combine("Uploads", "PrecautionsFile");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileExtention = fileName.Split('.').Last().ToLower();
                    if (fileExtention.ToLower() != "pdf")
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = "You have to upload a PDF file format";
                        return BadRequest(response);
                    }
                    var fileNameToSave = Guid.NewGuid().ToString() + "." + fileExtention;
                    var fullPath = Path.Combine(pathToSave, fileNameToSave);
                    var filePath = Path.Combine(folderName, fileNameToSave);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.FlushAsync();

                    }
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    response.Response = filePath;

                    return Ok(response);
                }
                else
                {
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    response.Message = "no file to upload";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateSystemParemeters([FromBody] UpdateSystemParemetersModel model)
        {
            var response = new HomeVisitsWebApiResponse<bool>();
            var userInfo = GetCurrentUserId();
            if (userInfo.ClientId != Guid.Empty)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (model.IsOptimizezonebefore == true && model.OptimizezonebeforeInMin == null)
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "Optimize zone before is missing";
                            response.Response = false;
                            return BadRequest(response);
                        }
                        if (model.EstimatedVisitDurationInMin > 0 && model.EstimatedVisitDurationInMin <= 999)
                        {
                            if (model.NextReserveHomevisitInDay > 0 && model.NextReserveHomevisitInDay <= 99)
                            {

                                if (model.VisitApprovalBy != null && model.VisitCancelBy != null)
                                {
                                    var updateSystemParemetersCommand = new UpdateSystemParemetersCommand
                                    {
                                        ClientId = userInfo.ClientId.GetValueOrDefault(),
                                        CreateBy = userInfo.UserId,
                                        DefaultCountryId = model.DefaultCountryId,
                                        DefaultGovernorateId = model.DefaultGovernorateId,
                                        EstimatedVisitDurationInMin = model.EstimatedVisitDurationInMin,
                                        NextReserveHomevisitInDay = model.NextReserveHomevisitInDay,
                                        OptimizezonebeforeInMin = model.OptimizezonebeforeInMin,
                                        RoutingSlotDurationInMin = model.RoutingSlotDurationInMin,
                                        VisitApprovalBy = model.VisitApprovalBy,
                                        VisitCancelBy = model.VisitCancelBy,
                                        WhatsappBusinessLink = model.WhatsappBusinessLink,
                                        PrecautionsFile = model.PrecautionsFile,
                                        CallCenterNumber = model.CallCenterNumber,
                                        IsOptimizezonebefore = model.IsOptimizezonebefore,
                                        IsSendPatientTimeConfirmation = model.IsSendPatientTimeConfirmation,
                                        FileName = model.FileName
                                    };

                                    await _commandBus.SendAsync((IUpdateSystemParemetersCommand)updateSystemParemetersCommand);

                                    response.Response = true;
                                    response.ResponseCode = WebApiResponseCodes.Sucess;
                                    response.Message = "Data updated successfully";
                                    return Ok(response);
                                }
                                else
                                {
                                    response.ResponseCode = WebApiResponseCodes.Failer;
                                    response.Message = "Please fill the required data";
                                    return BadRequest(response);
                                }
                            }
                            else
                            {
                                response.ResponseCode = WebApiResponseCodes.Failer;
                                response.Message = "Invalid Home Visit reservation available days";
                                return BadRequest(response);
                            }
                        }
                        else
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "Invalid Estimated Visit Duration";
                            return BadRequest(response);
                        }
                    }
                    else
                    {
                        response.Response = false;
                        response.ResponseCode = WebApiResponseCodes.Failer;
                        return BadRequest(response);
                    }
                }

                catch (Exception)
                {
                    response.Response = false;
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    return BadRequest(response);
                }
            }
            else
            {
                response.ResponseCode = WebApiResponseCodes.Failer;
                response.Message = "No Client Signed";
                response.Response = false;
                return BadRequest(response);
            }
        }

        [HttpGet("GetSystemParameterByClientId")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetSystemParametersForEditQueryResponse>), 200)]
        public async Task<IActionResult> GetSystemParameterByClientId(Guid? clientId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetSystemParametersForEditQueryResponse>();
                    if (clientId == null)
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
                        {
                            ClientId = userInfo.ClientId.GetValueOrDefault()
                        });
                        response.Response = new GetSystemParametersForEditQueryResponse
                        {
                            SystemParameters = result.SystemParameters
                        };
                    }
                    else
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetSystemParametersForEditQueryResponse>(new GetSystemParametersByClientIdForEditQuery
                        {
                            ClientId = (Guid)clientId
                        });
                        response.Response = new GetSystemParametersForEditQueryResponse
                        {
                            SystemParameters = result.SystemParameters
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

        [HttpGet("GetVisitAcceptCancelPermission")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetVisitDatesRegardingSysParamQueryResponse>), 200)]
        public async Task<IActionResult> GetVisitAcceptCancelPermission(Guid? clientId, string? isCancelledBy, string? isAcceptedBy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<IGetVisitAcceptCancelPermissionQueryResponse>();
                    if (clientId == null)
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetVisitAcceptCancelPermissionQuery, IGetVisitAcceptCancelPermissionQueryResponse>(new GetVisitAcceptCancelPermissionQuery
                        {
                            ClientId = userInfo.ClientId.GetValueOrDefault(),
                            IsAcceptedBy = isAcceptedBy,
                            IsCancelledBy = isCancelledBy
                        });
                        if (result != null)
                        {
                            response.Response = new GetVisitAcceptCancelPermissionQueryResponse
                            {
                                Value=result.Value
                            };
                        }
                        else
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "No System Parameters added";
                            return BadRequest(response);
                        }
                    }
                    else
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetVisitAcceptCancelPermissionQuery, IGetVisitAcceptCancelPermissionQueryResponse>(new GetVisitAcceptCancelPermissionQuery
                        {
                            ClientId = (Guid)clientId,
                            IsAcceptedBy = isAcceptedBy,
                            IsCancelledBy = isCancelledBy
                        });
                        if (result != null)
                        {
                            response.Response = new GetVisitAcceptCancelPermissionQueryResponse
                            {
                                Value = result.Value

                            };
                        }
                        else
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "No System Parameters added";
                            return BadRequest(response);
                        }
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
        
        [HttpGet("GetVisitAcceptAndCancelPermission")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetVisitDatesRegardingSysParamQueryResponse>), 200)]
        public async Task<IActionResult> GetVisitAcceptAndCancelPermission(Guid? clientId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<IGetVisitAcceptAndCancelPermissionQueryResponse>();
                    if (clientId == null)
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetVisitAcceptCancelPermissionQuery, IGetVisitAcceptAndCancelPermissionQueryResponse>(new GetVisitAcceptCancelPermissionQuery
                        {
                            ClientId = userInfo.ClientId.GetValueOrDefault(),
                            
                        });
                        if (result != null)
                        {
                            response.Response = new GetVisitAcceptAndCancelPermissionQueryResponse
                            {
                                IsApprovedByCallCenter=result.IsApprovedByCallCenter,
                                IsCancelledByCallCenter=result.IsCancelledByCallCenter,
                                IsApprovedByChemist=result.IsApprovedByChemist,
                                IsCancelledByChemist=result.IsCancelledByChemist
                            };
                        }
                        else
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "No System Parameters added";
                            return BadRequest(response);
                        }
                    }
                    else
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetVisitAcceptCancelPermissionQuery, IGetVisitAcceptAndCancelPermissionQueryResponse>(new GetVisitAcceptCancelPermissionQuery
                        {
                            ClientId = (Guid)clientId
                        });
                        if (result != null)
                        {
                            response.Response = new GetVisitAcceptAndCancelPermissionQueryResponse
                            {
                                IsApprovedByCallCenter = result.IsApprovedByCallCenter,
                                IsCancelledByCallCenter = result.IsCancelledByCallCenter,
                                IsApprovedByChemist = result.IsApprovedByChemist,
                                IsCancelledByChemist = result.IsCancelledByChemist
                            };
                        }
                        else
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "No System Parameters added";
                            return BadRequest(response);
                        }
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

        [HttpGet("GetVisitDatesRegardingSysParam")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<GetVisitDatesRegardingSysParamQueryResponse>), 200)]
        public async Task<IActionResult> GetVisitDatesRegardingSysParam(Guid? clientId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();
                    var response = new HomeVisitsWebApiResponse<GetVisitDatesRegardingSysParamQueryResponse>();
                    if (clientId == null)
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetVisitDatesRegardingSysParamQueryResponse>(new GetSystemParametersByClientIdForEditQuery
                        {
                            ClientId = userInfo.ClientId.GetValueOrDefault()
                        });
                        if (result != null)
                        {
                            response.Response = new GetVisitDatesRegardingSysParamQueryResponse
                            {
                                StartDate = result.StartDate,
                                EndDate = result.EndDate
                            };
                        }
                        else
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "No System Parameters added";
                            return BadRequest(response);
                        }
                    }
                    else
                    {
                        var result = await _queryProcessor.ProcessQueryAsync<IGetSystemParametersByClientIdForEditQuery, IGetVisitDatesRegardingSysParamQueryResponse>(new GetSystemParametersByClientIdForEditQuery
                        {
                            ClientId = (Guid)clientId
                        });
                        if (result != null)
                        {
                            response.Response = new GetVisitDatesRegardingSysParamQueryResponse
                            {
                                StartDate = result.StartDate,
                                EndDate = result.EndDate
                            };
                        }
                        else
                        {
                            response.ResponseCode = WebApiResponseCodes.Failer;
                            response.Message = "No System Parameters added";
                            return BadRequest(response);
                        }
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

    }
}