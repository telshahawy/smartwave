using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.WebAPI.CustomAttribute;
using SW.HomeVisits.WebAPI.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.HomeVisits.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AuthorizeByUserPermission]
    public class AttachmentsController : HomeVisitsControllerBase
    {
        [HttpPost("UpoadUserImage")]
        [DisableRequestSizeLimitAttribute]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<string>), 200)]
        public IActionResult UpoadUserImage()
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<string>();
                var userInfo = GetCurrentUserId();
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Uploads", "UsersPhotos");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileExtention = fileName.Split('.').Last().ToLower();
                    if (fileExtention.ToLower() != "jpeg" && fileExtention.ToLower() != "png" && fileExtention.ToLower() != "jpg")
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = "Not Supported File Type";
                        return BadRequest(response);
                    }
                    var fileNameToSave = Guid.NewGuid().ToString() + "." + fileExtention;
                    var fullPath = Path.Combine(pathToSave, fileNameToSave);
                    var filePath = Path.Combine(folderName, fileNameToSave);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
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

        [HttpPost("UpoadKmlFile")]
        [DisableRequestSizeLimitAttribute]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<string>), 200)]
        public IActionResult UpoadKmlFile()
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<string>();
                var userInfo = GetCurrentUserId();
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Uploads", "KmlFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileExtention = fileName.Split('.').Last().ToLower();
                    if (fileExtention.ToLower() != "kml")
                    {
                        response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                        response.Message = "Not Supported File Type";
                        return BadRequest(response);
                    }
                    //var fileNameToSave = Guid.NewGuid().ToString() + "." + fileExtention;
                    var fullPath = Path.Combine(pathToSave, "HV_"+fileName);
                    var filePath = Path.Combine(folderName, "HV_"+fileName);
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

        [HttpPost("UpoadVisitData")]
        [DisableRequestSizeLimitAttribute]
        [ProducesResponseType(typeof(HomeVisitsWebApiResponse<string>), 200)]
        public IActionResult UpoadVisitData()
        {
            try
            {
                var response = new HomeVisitsWebApiResponse<string>();
                var userInfo = GetCurrentUserId();
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Uploads", "Visits");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileExtention = fileName.Split('.').Last().ToLower();
                    //if (fileExtention.ToLower() != "kml")
                    //{
                    //    response.ResponseCode = Application.Abstract.Enum.WebApiResponseCodes.Failer;
                    //    response.Message = "Not Supported File Type";
                    //    return BadRequest(response);
                    //}
                    var fileNameToSave = Guid.NewGuid().ToString() + "." + fileExtention;
                    var fullPath = Path.Combine(pathToSave, fileNameToSave);
                    var filePath = Path.Combine(folderName, fileNameToSave);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
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

        [HttpPost("DownloadKML")]
        public IActionResult DownloadKML(string fileName)
        {
          
            byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine("Uploads/KmlFiles", fileName));
            return File(bytes, "application/vnd.google-earth.kml+xml");
        }
    }
}
