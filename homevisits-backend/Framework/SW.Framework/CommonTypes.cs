using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SW.Framework
{
    /// <summary>
    ///     Provides a list of common types. use this class when wanting to avoid frequent "typeof" calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [DebuggerStepThrough]
    public static class CommonTypes
    {
        /// <summary>
        ///     The <see cref="object" /> type
        /// </summary>
        public static readonly Type ObjectType = typeof(object);

        /// <summary>
        ///     The <see cref="bool" /> type
        /// </summary>
        public static readonly Type BooleanType = typeof(bool);

        /// <summary>
        ///     The nullable <see cref="bool" /> type
        /// </summary>
        public static readonly Type NullableBooleanType = typeof(bool?);

        /// <summary>
        ///     The <see cref="byte" /> type
        /// </summary>
        public static readonly Type ByteType = typeof(byte);

        /// <summary>
        ///     The nullable <see cref="bool" /> type
        /// </summary>
        public static readonly Type NullableByteType = typeof(byte?);

        /// <summary>
        ///     The <see cref="int" /> type
        /// </summary>
        public static readonly Type IntegerType = typeof(int);

        /// <summary>
        ///     The nullable <see cref="int" /> type
        /// </summary>
        public static readonly Type NullableIntegerType = typeof(int?);

        /// <summary>
        ///     The <see cref="char" /> type
        /// </summary>
        public static readonly Type CharType = typeof(char);

        /// <summary>
        ///     The nullable <see cref="char" /> type
        /// </summary>
        public static readonly Type NullableCharType = typeof(char?);

        /// <summary>
        ///     The <see cref="decimal" /> type
        /// </summary>
        public static readonly Type DecimalType = typeof(decimal);

        /// <summary>
        ///     The nullable <see cref="decimal" /> type
        /// </summary>
        public static readonly Type NullableDecimalType = typeof(decimal?);

        /// <summary>
        ///     The <see cref="double" /> type
        /// </summary>
        public static readonly Type DoubleType = typeof(double);

        /// <summary>
        ///     The nullable <see cref="double" /> type
        /// </summary>
        public static readonly Type NullableDoubleType = typeof(double?);

        /// <summary>
        ///     The <see cref="Single" /> type
        /// </summary>
        public static readonly Type SingleType = typeof(float);

        /// <summary>
        ///     The nullable <see cref="Single" /> type
        /// </summary>
        public static readonly Type NullableSingleType = typeof(float?);

        /// <summary>
        ///     The <see cref="string" /> type
        /// </summary>
        public static readonly Type StringType = typeof(string);

        /// <summary>
        ///     The simple <see cref="IEnumerable" /> type
        /// </summary>
        public static readonly Type SimpleIEnumerableType = typeof(IEnumerable);

        /// <summary>
        ///     The generic <see cref="IEnumerable{T}" /> type
        /// </summary>
        public static readonly Type GenericIEnumerableType = typeof(IEnumerable<>);

        /// <summary>
        ///     The <see cref="DateTime" /> type
        /// </summary>
        public static readonly Type DateTimeType = typeof(DateTime);

        /// <summary>
        ///     The nullable <see cref="DateTimeType" /> type
        /// </summary>
        public static readonly Type NullableDateTimeType = typeof(DateTime?);
    }
}