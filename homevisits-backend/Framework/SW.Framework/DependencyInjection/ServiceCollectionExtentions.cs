using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SW.Framework.Cqrs;
using SW.Framework.DependencyInjection;
using SW.Framework.Security;
using SW.Framework.Validation;

namespace SW.Framework.DependencyInjectionCore
{
    public static class ServiceCollectionExtentions
    {
        public static void RegisterDefaultCommandBus(this IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
        }

        public static void RegisterDefaultQueryProcessor(this IServiceCollection services)
        {
            services.AddScoped<IQueryProcessor, QueryProcessor>();
      
        }

        public static void RegisterDefaultSystemEventBus(this IServiceCollection services)
        {
            services.AddScoped<ISystemEventBus, SystemEventBus>();
       
        }

        public static void RegisterDefaultAuthorizationManager(this IServiceCollection services)
        {
            services.AddScoped<IAuthorisationManager, SW.Framework.Security.AuthorisationManager>();
  
        }

        public static void RegisterDefaultValidationManager(this IServiceCollection services)
        {
            services.AddScoped<IValidationManager, SW.Framework.Validation.ValidationManager>();
        }

        public static void RegisterAll<TService>(this IServiceCollection services,
            IEnumerable<Type> Types) where TService : class
        {
            foreach (var item in Types)
            {
                services.AddTransient(typeof(TService), item);
            }
        }
        //public static void InitialiseModule<TModule>(this IServiceCollection services) where TModule : IModule, new()
        //{
        //    TModule module = new TModule();
        //    module.Initialise(container);
        //    return container;
        //}
    }
}
