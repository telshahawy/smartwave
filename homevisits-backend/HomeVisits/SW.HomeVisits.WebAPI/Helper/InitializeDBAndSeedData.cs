using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SW.Framework.Cqrs;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Models;
using SW.HomeVisits.Infrastruture.Presistance.DbInitializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Helper
{
    public static class InitializeDBAndSeedData
    {
        public static void InitializeDBAndSeedDefualtData(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                //dbInitializer.Initialize();
                dbInitializer.SeedData();

                //var _commandBus = scope.ServiceProvider.GetService<ICommandBus>();
                //_commandBus.SendAsync((IAddPermissionCommand) new AddPermissionCommand
                //{
                //    ControllerActionModelList = ReflectionHelper.GetControllerActionList()
                //}).Wait();
            }
        }

    }
}
