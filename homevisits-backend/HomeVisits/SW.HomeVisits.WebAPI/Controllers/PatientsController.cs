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
using System.Text.RegularExpressions;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.Framework.Exceptions;
using SW.HomeVisits.Domain.Enums;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.HomeVisits.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PatientsController : HomeVisitsControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public PatientsController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpPost("AddOrUpdatePatientByPatientApp")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> AddOrUpdatePatientByPatientApp([FromBody] AddPatientAuthModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<Guid>();
                if (ModelState.IsValid)
                {
                    //searchPatient..

                    var result = await _queryProcessor.ProcessQueryAsync<IGetPatientForEditQuery, IGetPatientForEditQueryResponse>(new GetPatientForEditQuery
                    {
                        PatientId = model.PatientId
                    });

                    var userDeviceResponse = await _queryProcessor.ProcessQueryAsync<IGetUserDeviceByChemistIdQuery, IGetUserDeviceByChemistIdQueryResponse>(new GetUserDeviceByChemistIdQuery
                    {
                        ChemistId = model.PatientId
                    });

                    if (result.Patients == null)
                    {
                        var userName = model.PatientId.ToString();
                        var name = result?.Patients?.Name == null ? model.PatientName : result?.Patients?.Name;
                        var isActive = true;
                        var password = "P@$$w0rd";
                        //var roleId =Guid.Parse( "48c21aba-40e8-451a-9f29-883ee06f9d94");
                        var roleId = Guid.Parse("e50138ce-a9c2-4370-b055-ff32d075f1d3");
                        var addUserDeviceCommand = new AddUserDeviceCommand
                        {
                            UserDeviceId = Guid.NewGuid(),
                            UserId = model.PatientId,
                            DeviceSerialNumber = model.DeviceSerialNumber,
                            FireBaseDeviceToken = model.FireBaseDeviceToken,

                        };

                        var patientModel = new CreatePatientAuthModel
                        {
                            Name = name,
                            ClientId = model.ClientId,
                            CreatedBy = model.PatientId,
                            IsActive = isActive,
                            Password = password,
                            UserName = userName,
                            UserId = model.PatientId,
                            RoleId = roleId,
                            PhoneNo = model.Phones.FirstOrDefault().Phone
                        };

                        var addPatientCommand = new AddPatientCommand
                        {
                            PatientId = model.PatientId,
                            DOB = "DOB",
                            Gender = model.Gender,
                            ClientId = model.ClientId,
                            Name = name,
                            BirthDate = model.BirthDate,
                            IsDeleted = false,

                            Phones = model.Phones.Select(p => new CreatePatientPhoneNumbersDto
                            {
                                PatientPhoneId = Guid.NewGuid(),
                                PhoneNumber = p.Phone,
                                CreatedBy = model.PatientId

                            }).ToList()

                        };

                        await _commandBus.SendAsync((IAddPatientCommand)addPatientCommand);
                        await _commandBus.SendAsync((ICreatePatientAuthCommand)patientModel);
                        response.Response = patientModel.UserId;


                        await AddUserDevice(addUserDeviceCommand, model.PatientId);
                        //await _commandBus.SendAsync((IAddUserDeviceCommand)addUserDeviceCommand);
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                        return Ok(response);
                    }
                    else
                    {
                        //if user/device exissts or not

                        var isUserExist = await _queryProcessor.ProcessQueryAsync<IGetUserQuery, IIsUserExist>(new GetUserQuery
                        {
                            PatientId = model.PatientId
                        });

                        if (isUserExist.IsExist)
                        {

                            if (userDeviceResponse?.UserDevice?.UserDeviceId == null)
                            {

                                var addUserDeviceCommand = new AddUserDeviceCommand
                                {
                                    UserDeviceId = Guid.NewGuid(),
                                    UserId = model.PatientId,
                                    DeviceSerialNumber = model.DeviceSerialNumber,
                                    FireBaseDeviceToken = model.FireBaseDeviceToken,

                                };
                                await AddUserDevice(addUserDeviceCommand, model.PatientId);

                            }

                        }
                        else
                        {
                            var userName = model.PatientId.ToString();
                            var name = result?.Patients?.Name == null ? model.PatientName : result?.Patients?.Name;
                            var isActive = true;
                            var password = "P@$$w0rd";
                            //var roleId =Guid.Parse( "48c21aba-40e8-451a-9f29-883ee06f9d94");
                            var roleId = Guid.Parse("e50138ce-a9c2-4370-b055-ff32d075f1d3");

                            var patientModel = new CreatePatientAuthModel
                            {
                                Name = name,
                                ClientId = model.ClientId,
                                CreatedBy = model.PatientId,
                                IsActive = isActive,
                                Password = password,
                                UserName = userName,
                                UserId = model.PatientId,
                                RoleId = roleId,
                                PhoneNo = result.Patients.PhoneNumber
                            };
                            await _commandBus.SendAsync((ICreatePatientAuthCommand)patientModel);
                            if (userDeviceResponse?.UserDevice?.UserDeviceId == null)
                            {
                                var addUserDeviceCommand = new AddUserDeviceCommand
                                {
                                    UserDeviceId = Guid.NewGuid(),
                                    UserId = model.PatientId,
                                    DeviceSerialNumber = model.DeviceSerialNumber,
                                    FireBaseDeviceToken = model.FireBaseDeviceToken,

                                };
                                await AddUserDevice(addUserDeviceCommand, model.PatientId);

                            }

                        }

                        response.Response = model.PatientId;

                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;

                        return Ok(response);
                        //Auto..Login
                    }


                }
                else
                {
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    response.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private async Task AddUserDevice(AddUserDeviceCommand model, Guid PatientId)
        {
            var addUserDeviceCommand = new AddUserDeviceCommand
            {
                UserDeviceId = Guid.NewGuid(),
                UserId = PatientId,
                DeviceSerialNumber = model.DeviceSerialNumber,
                FireBaseDeviceToken = model.FireBaseDeviceToken,

            };

            await _commandBus.SendAsync((IAddUserDeviceCommand)addUserDeviceCommand);
        }

        [HttpPost("AddPatientAddressByPatientApp")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<PatientAddressDto>), 200)]
        public async Task<IActionResult> AddPatientAddressByPatientApp([FromBody] AddPatientAddressModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<PatientAddressDto>();
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(model.LocationUrl) && !model.LocationUrl.StartsWith("https://www.google.com/maps") && !model.LocationUrl.Contains(','))
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = (GetCultureName() == CultureNames.ar) ? "الموقع غير صحيح" : "invalid URl";
                        return BadRequest(response);
                    }

                    var addPatientAddressCommand = new AddPatientAddressCommand
                    {
                        AdditionalInfo = model.AdditionalInfo,
                        Building = model.Building,
                        CreateBy = model.PatientId,
                        Flat = model.Flat,
                        Floor = model.Floor,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        GeoZoneId = model.GeoZoneId,
                        LocationUrl = !string.IsNullOrWhiteSpace(model.LocationUrl) ? model.LocationUrl : null,
                        PatientAddressId = Guid.NewGuid(),
                        PatientId = model.PatientId,
                        street = model.street
                    };
                    await _commandBus.SendAsync((IAddPatientAddressCommand)addPatientAddressCommand);



                    response.Response = new PatientAddressDto
                    {
                        PatientAddressId = addPatientAddressCommand.PatientAddressId,
                        GeoZoneId = addPatientAddressCommand.GeoZoneId,
                        AddressFormatted = string.Format("{0} - {1} - {2} - {3}", addPatientAddressCommand.Flat, addPatientAddressCommand.Floor, addPatientAddressCommand.Building, addPatientAddressCommand.street),
                        //AddressFormatted = $" Flat:{addPatientAddressCommand.Flat}, Floor:{addPatientAddressCommand.Floor}, Building:{addPatientAddressCommand.Building}, Street:{addPatientAddressCommand.street}",

                    };
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
                }
                else
                {
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    response.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("AddPatientAddress")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<PatientAddressDto>), 200)]
        public async Task<IActionResult> AddPatientAddress([FromBody] AddPatientAddressModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<PatientAddressDto>();
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(model.LocationUrl) && !model.LocationUrl.StartsWith("https://www.google.com/maps") && !model.LocationUrl.Contains(','))
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = (GetCultureName() == CultureNames.ar) ? "الموقع غير صحيح" : "invalid URl";
                        return BadRequest(response);
                    }

                    var addPatientAddressCommand = new AddPatientAddressCommand
                    {
                        AdditionalInfo = model.AdditionalInfo,
                        Building = model.Building,
                        CreateBy = GetCurrentUserId().UserId,
                        Flat = model.Flat,
                        Floor = model.Floor,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        GeoZoneId = model.GeoZoneId,
                        LocationUrl = !string.IsNullOrWhiteSpace(model.LocationUrl) ? model.LocationUrl : null,
                        PatientAddressId = Guid.NewGuid(),
                        PatientId = model.PatientId,
                        street = model.street
                    };
                    await _commandBus.SendAsync((IAddPatientAddressCommand)addPatientAddressCommand);



                    response.Response = new PatientAddressDto
                    {
                        PatientAddressId = addPatientAddressCommand.PatientAddressId,
                        GeoZoneId = addPatientAddressCommand.GeoZoneId,
                        AddressFormatted = string.Format("{0} - {1} - {2} - {3}", addPatientAddressCommand.Flat, addPatientAddressCommand.Floor, addPatientAddressCommand.Building, addPatientAddressCommand.street),

                    };
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
                }
                else
                {
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    response.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("AddPatientPhoneByPatientApp")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> AddPatientPhoneByPatientApp([FromBody] AddPatientPhoneModel model)
        {
            var response = new HomeVisitsWebApiResponse<Guid>();
            try
            {
                var regex = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";

                if (ModelState.IsValid)
                {
                    if (!Regex.Match(model.Phone, regex).Success)
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = GetCultureName() == CultureNames.ar ? "رقم الهاتف غير صحيح" : "Phone number is incorrect";
                        return BadRequest(response);
                    }
                    var addPatientPhoneCommand = new AddPatientPhoneCommand
                    {
                        PatientId = model.PatientId,
                        Phone = model.Phone,
                        CreateBy = model.PatientId,
                        PatientPhoneId = Guid.NewGuid()
                    };
                    await _commandBus.SendAsync((IAddPatientPhoneCommand)addPatientPhoneCommand);
                    response.Response = addPatientPhoneCommand.PatientPhoneId;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
                }
                else
                {
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    response.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                    return BadRequest(response);
                }
            }
            catch (ValidationRuleException ex)
            {
                ErrorCodes code = (ErrorCodes)ex.ErrorCode;
                switch (code)
                {
                    case ErrorCodes.PhoneNumberAlreadyExists:
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = GetCultureName() == CultureNames.ar ? "رقم الهاتف مستخدم من قبل" : "phone number already exists";
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

        [HttpPost("AddPatientPhone")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> AddPatientPhone([FromBody] AddPatientPhoneModel model)
        {
            var response = new HomeVisitsWebApiResponse<Guid>();
            try
            {
                var regex = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";

                if (ModelState.IsValid)
                {
                    if (!Regex.Match(model.Phone, regex).Success)
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = GetCultureName() == CultureNames.ar ? "رقم الهاتف غير صحيح" : "Phone number is incorrect";
                        return BadRequest(response);
                    }
                    var addPatientPhoneCommand = new AddPatientPhoneCommand
                    {
                        PatientId = model.PatientId,
                        Phone = model.Phone,
                        CreateBy = GetCurrentUserId().UserId,
                        PatientPhoneId = Guid.NewGuid()
                    };
                    await _commandBus.SendAsync((IAddPatientPhoneCommand)addPatientPhoneCommand);
                    response.Response = addPatientPhoneCommand.PatientPhoneId;
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Sucess;
                    return Ok(response);
                    //return Created(new Uri(Url.Link("GetUserRoleById", new { UserRoleId = model.Id })), null);
                }
                else
                {
                    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    response.Message = GetCultureName() == CultureNames.ar ? "حدث خطاء" : "Something wrong";
                    return BadRequest(response);
                }
            }
            catch (ValidationRuleException ex)
            {
                ErrorCodes code = (ErrorCodes)ex.ErrorCode;
                switch (code)
                {
                    case ErrorCodes.PhoneNumberAlreadyExists:
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = GetCultureName() == CultureNames.ar ? "رقم الهاتف مستخدم من قبل" : "phone number already exists";
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

        [HttpGet("PatientsList")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<List<PatientsDto>>), 200)]
        public async Task<IActionResult> PatientsList([FromQuery] SearchPatientModel model)
        {
            try
            {
                var userInfo = GetCurrentUserId();
                var apiResponse = new HomeVisitsWebApiResponse<List<PatientsDto>>();
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("String1", GetCulture());
                        return BadRequest(apiResponse);
                    }
                    var response = await _queryProcessor.ProcessQueryAsync<IPatientsListQuery, IPatientsListQueryResponse>(new PatientsListQuery
                    {
                        PhoneNumber = model.PhoneNumber,
                        CultureName = GetCultureName(),
                        ClientId = userInfo.ClientId.GetValueOrDefault()
                    });
                    if (!response.Patients.Any())
                    {
                        apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                        apiResponse.Response = response.Patients;
                        apiResponse.Message = Resources.Resource.ResourceManager.GetString("PatientsNotFound", GetCulture());
                    }
                    apiResponse.ResponseCode = WebApiResponseCodes.Sucess;
                    apiResponse.Response = response.Patients;
                    return Ok(apiResponse);

                }
                else
                {
                    apiResponse.ResponseCode = WebApiResponseCodes.Failer;
                    apiResponse.Message = "Invalid Input Parameter";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("AddPatient")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<Guid>), 200)]
        public async Task<IActionResult> AddPatient([FromBody] AddPatientModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<Guid>();

                if (ModelState.IsValid)
                {
                    var patient_Id = Guid.NewGuid();
                    var userInfo = GetCurrentUserId();
                    if (model.Addresses != null)
                    {
                        //////SaveAuth///

                        var userName = patient_Id.ToString();
                        var name = model.Name;
                        var isActive = true;
                        var password = "P@$$w0rd";
                        //var roleId =Guid.Parse( "48c21aba-40e8-451a-9f29-883ee06f9d94");
                        var roleId = Guid.Parse("e50138ce-a9c2-4370-b055-ff32d075f1d3");
                      
                        var patientModel = new CreatePatientAuthModel
                        {
                            Name = name,
                            ClientId = userInfo.ClientId.GetValueOrDefault(),
                            CreatedBy = userInfo.UserId,
                            IsActive = isActive,
                            Password = password,
                            UserName = userName,
                            UserId = patient_Id,
                            RoleId = roleId,
                            PhoneNo = model.Phones.FirstOrDefault().Phone
                        };

                       
                        await _commandBus.SendAsync((ICreatePatientAuthCommand)patientModel);
                        
                        //////
                        var addPatientCommand = new AddPatientCommand
                        {
                            PatientId = Guid.NewGuid(),
                            DOB = "DOB",
                            Gender = model.Gender,
                            ClientId = userInfo.ClientId.Value,
                            Name = model.Name,
                            BirthDate = model.BirthDate,
                            IsDeleted = false,

                            Addresses = model.Addresses.Select(item => new CreatePatientAddressDto
                            {

                                PatientAddressId = patient_Id,
                                Building = item.Building,
                                AdditionalInfo = item.AdditionalInfo,
                                CreateBy = userInfo.UserId,
                                Flat = item.Flat,
                                Floor = item.Floor,
                                GeoZoneId = item.GeoZoneId,
                                Latitude = item.Latitude,
                                Longitude = item.Longitude,
                                street = item.street

                            }).ToList(),
                            Phones = model.Phones.Select(p => new CreatePatientPhoneNumbersDto
                            {
                                PatientPhoneId = Guid.NewGuid(),
                                PhoneNumber = p.Phone,
                                CreatedBy = userInfo.UserId

                            }).ToList()

                        };

                        await _commandBus.SendAsync((IAddPatientCommand)addPatientCommand);

                        response.ResponseCode = WebApiResponseCodes.Sucess;
                        response.Response = addPatientCommand.PatientId;
                        return Ok(response);
                    }
                    else
                    {
                        //////SaveAuth///

                        var userName = patient_Id.ToString();
                        var name = model.Name;
                        var isActive = true;
                        var password = "P@$$w0rd";
                        //var roleId =Guid.Parse( "48c21aba-40e8-451a-9f29-883ee06f9d94");
                        var roleId = Guid.Parse("e50138ce-a9c2-4370-b055-ff32d075f1d3");

                        var patientModel = new CreatePatientAuthModel
                        {
                            Name = name,
                            ClientId = userInfo.ClientId.GetValueOrDefault(),
                            CreatedBy = userInfo.UserId,
                            IsActive = isActive,
                            Password = password,
                            UserName = userName,
                            UserId = patient_Id,
                            RoleId = roleId,
                            PhoneNo = model.Phones.FirstOrDefault().Phone
                        };


                        await _commandBus.SendAsync((ICreatePatientAuthCommand)patientModel);

                        //////
                        var addPatientCommand = new AddPatientCommand
                        {
                            PatientId = patient_Id,
                            DOB = "DOB",
                            Gender = model.Gender,
                            ClientId = userInfo.ClientId.Value,
                            Name = model.Name,
                            BirthDate = model.BirthDate,
                            IsDeleted = false,

                            Phones = model.Phones.Select(p => new CreatePatientPhoneNumbersDto
                            {
                                PatientPhoneId = Guid.NewGuid(),
                                PhoneNumber = p.Phone,
                                CreatedBy = userInfo.UserId

                            }).ToList()

                        };

                        await _commandBus.SendAsync((IAddPatientCommand)addPatientCommand);

                        response.ResponseCode = WebApiResponseCodes.Sucess;
                        response.Response = addPatientCommand.PatientId;
                        return Ok(response);
                    }
                }
                else
                {
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    response.Message = "Invalid Input Parameter";
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("GetPatientForEdit")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<PatientsDto>), 200)]
        public async Task<IActionResult> GetPatientForEdit([FromQuery] Guid patientId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<PatientsDto>();

                    var result = await _queryProcessor.ProcessQueryAsync<IGetPatientForEditQuery, IGetPatientForEditQueryResponse>(new GetPatientForEditQuery
                    {
                        PatientId = patientId
                    });

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = result.Patients;
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

        [HttpPut("UpdatePatient")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientModel model)
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<bool>();

                if (ModelState.IsValid)
                {
                    var userInfo = GetCurrentUserId();

                    var updatePatientCommand = new UpdatePatientCommand
                    {
                        PatientId = model.PatientId,
                        PatientNo = model.PatientNo,
                        DOB = model.DOB,
                        Gender = model.Gender,
                        ClientId = userInfo.ClientId.Value,
                        Name = model.Name,
                        BirthDate = model.BirthDate,
                        IsDeleted = false,
                        Addresses = model.Addresses.Select(item => new CreatePatientAddressDto
                        {

                            PatientAddressId = item.PatientAddressId,
                            Building = item.Building,
                            AdditionalInfo = item.AdditionalInfo,
                            CreateBy = userInfo.UserId,
                            Flat = item.Flat,
                            Floor = item.Floor,
                            GeoZoneId = item.GeoZoneId,
                            Latitude = item.Latitude,
                            Longitude = item.Longitude,
                            street = item.street

                        }).ToList(),
                        Phones = model.Phones.Select(p => new CreatePatientPhoneNumbersDto
                        {
                            PatientPhoneId = p.PatientPhoneId,
                            PhoneNumber = p.Phone,
                            CreatedBy = userInfo.UserId

                        }).ToList()

                    };

                    await _commandBus.SendAsync((IUpdatePatientCommand)updatePatientCommand);

                    response.ResponseCode = WebApiResponseCodes.Sucess;
                    response.Response = true;
                    return Ok(response);
                }
                else
                {
                    response.ResponseCode = WebApiResponseCodes.Failer;
                    response.Message = "Invalid Input Parameter";
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpDelete("DeletePatientAddress")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeletePatientAddress([FromQuery] Guid patientAddressId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeletePatientAddressCommand)new DeletePatientAddressCommand
                    {
                        PatientAddressId = patientAddressId
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

        [HttpDelete("DeletePatientPhone")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeletePatientPhone([FromQuery] Guid patientPhoneId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeletePatientPhoneCommand)new DeletePatientPhoneCommand
                    {
                        PatientPhoneId = patientPhoneId
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

        [HttpDelete("DeletePatient")]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<bool>), 200)]
        public async Task<IActionResult> DeletePatient([FromQuery] Guid patientId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HomeVisitsWebApiResponse<bool>();
                    await _commandBus.SendAsync((IDeletePatientCommand)new DeletePatientCommand
                    {
                        PatientId = patientId
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
