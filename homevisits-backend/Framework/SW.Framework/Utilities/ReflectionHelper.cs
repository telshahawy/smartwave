using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SW.Framework.Utilities
{
    public static class ReflectionHelper
    {
        private static Assembly[] _currentDomainAssemblies;

        public static IEnumerable<Assembly> GetAssembliesForCurrentDomain()
        {
            return _currentDomainAssemblies ?? (_currentDomainAssemblies = AppDomain.CurrentDomain.GetAssemblies());
        }

        //public static List<ControllerActionModel> GetControllerActionList()
        //{
        //    Assembly asm = Assembly.GetCallingAssembly();
        //    return asm.GetTypes()
        //            .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
        //            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
        //            .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
        //            .Select(x => new ControllerActionModel { ControllerName = x.DeclaringType.Name, ActionName = x.Name, ReturnType = x.ReturnType, ReturnTypeName = x.ReturnType.Name, Attributes = x.GetCustomAttributes().ToList(), AttributesName = x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).ToList() })
        //            .OrderBy(x => x.ControllerName).ThenBy(x => x.ActionName).ToList();
        //}
    }
}