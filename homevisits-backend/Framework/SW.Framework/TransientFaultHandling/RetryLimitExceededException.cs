﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SW.Framework.TransientFaultHandling
{
    /// <summary>
    ///     Exception that occurs when transient policies have been exhausted without a successful execution.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly")]
    [Serializable]
    public sealed class RetryLimitExceededException : Exception
    {
        internal RetryLimitExceededException(Stack<Exception> exceptions)
            : base("The retries limit has been reached.")
        {
            RetryExceptions = exceptions;
        }

        /// <summary>
        ///     Gets the exceptions that took place while retries were being executed.
        /// </summary>
        /// <value>The retry exceptions.</value>
        public Stack<Exception> RetryExceptions { get; }
    }
}