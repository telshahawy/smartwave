using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SW.Framework.Cqrs;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
    public class AuthorizeByUserPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var _queryProcessor = context.HttpContext.RequestServices.GetService(typeof(IQueryProcessor)) as IQueryProcessor;

            //var userId = Guid.Parse(context.HttpContext.User.Identity.GetUserId());
            //var clientId = Guid.Parse(context.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "Client")?.Value);

            //var userPermission = _queryProcessor.ProcessQueryAsync<IGetUserPermissionQuery, IGetUserPermissionQueryResponse>(new GetUserPermissionQuery
            //{
            //    UserId = userId,
            //    ClientId = clientId
            //}).Result;

            //if (userPermission.UserPermission.UserType == (int)UserTypes.ClientAdmin || userPermission.UserPermission.UserType == (int)UserTypes.Administration)
            //    return;

            //var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //if (descriptor == null)
            //    throw new Exception("Unexpected Exception from Controller Action Descriptor");

            //var actionName = descriptor.ActionName;
            //var controllerName = descriptor.ControllerName;

            //var permissions = _queryProcessor.ProcessQueryAsync<IGetPermissionQuery, IGetPermissionQueryResponse>(new GetPermissionQuery
            //{
            //    ActionName = actionName,
            //    ControllerName = controllerName
            //}).Result;

            //if (permissions != null && permissions.PermissionDtos != null && permissions.PermissionDtos.Count > 0 && permissions.PermissionDtos.Any(p => p.IsActive && p.IsNeedToAuthorized))
            //{
            //    if (!userPermission.UserPermission.PermissionDtos.Any(p => p.ControllerName.Trim().ToLower() == controllerName.Trim().ToLower() && p.ActionName.Trim().ToLower() == actionName.Trim().ToLower()))
            //        context.Result = new UnauthorizedResult();
            //}
        }
    }
}
