using SW.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SW.Framework.Utilities
{
    /// <summary>
    ///     The class provides a reflection meta data cache for reference objects.
    /// </summary>
    public static class ReferenceObjectPropertyCache
    {
        private static readonly Dictionary<string, IEnumerable<PropertyInfo>> PropertyCache =
            new Dictionary<string, IEnumerable<PropertyInfo>>();

        /// <summary>
        ///     Gets the properties of the reference object type.
        /// </summary>
        /// <typeparam name="T">The type of object for which property meta data is to be returned.</typeparam>
        /// <returns>IEnumerable&lt;PropertyInfo&gt;.</returns>
        public static IEnumerable<PropertyInfo> GetProperties<T>() where T : class
        {
            var referenceObjectType = typeof(T);
            return GetProperties(referenceObjectType);
        }

        /// <summary>
        ///     Gets the properties of the reference object type.
        /// </summary>
        /// <param name="referenceObjectType">Type of the reference object.</param>
        /// <returns>IEnumerable&lt;PropertyInfo&gt;.</returns>
        public static IEnumerable<PropertyInfo> GetProperties(Type referenceObjectType)
        {
            Check.NotNull(referenceObjectType, nameof(referenceObjectType));

            if (!PropertyCache.ContainsKey(referenceObjectType.FullName))
                PropertyCache[referenceObjectType.FullName] =
                    referenceObjectType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(
                            property =>
                                property.GetIndexParameters().Length == 0 &&
                                property.CanRead &&
                                !property.Name.Equals("HasValue") &&
                                (property.PropertyType.IsValueType || property.PropertyType == CommonTypes.StringType))
                        .ToList();
            return PropertyCache[referenceObjectType.FullName];
        }

        public static void ClearCache()
        {
            PropertyCache.Clear();
        }
    }
}