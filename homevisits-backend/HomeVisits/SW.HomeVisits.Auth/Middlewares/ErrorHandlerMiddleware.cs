using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Auth.Midelwares
{
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(httpContext, exception);
            }
        }

        public static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            var errorList = new List<string>();
            if (exception.InnerException != null)
            {
                errorList.Add(exception.InnerException.Message);
                var num = ((SqlException)exception.InnerException).Number;
                errorList.Add(num + "");
            }
            else
                errorList.Add(exception.Message);
            var response = new HomeVisitsWebApiResponse<bool>
            {
                Message = exception.Message,
                ResponseCode = WebApiResponseCodes.Failer,
                ErrorList = errorList
            };
        var payload = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(payload);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
