using System;
using System.Transactions;

namespace SW.Framework.Utilities
{
    /// <summary>
    ///     Creates <see cref="TransactionScope" /> objects.
    /// </summary>
    public static class TransactionFactory
    {
        /// <summary>
        ///     Creates a <see cref="TransactionScope" /> object with the supplied options.
        /// </summary>
        /// <param name="isolationLevel">
        ///     Specifies the isolation level of a transaction. Default is
        ///     <see cref="IsolationLevel.ReadCommitted" />
        /// </param>
        /// <param name="transactionScopeOption">
        ///     Provides additional options for creating a transaction scope. Default is
        ///     <see cref="TransactionScopeOption.Required" />
        /// </param>
        /// <param name="timeOut">{Optional} A <see cref="TimeSpan" /> value that specifies the time out period for the transaction</param>
        /// <returns>A new <see cref="TransactionScope" /> object.</returns>
        public static TransactionScope CreateTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            TransactionScopeOption transactionScopeOption = TransactionScopeOption.Required,
            TimeSpan? timeOut = null)
        {
            var transactionOptions = new TransactionOptions {IsolationLevel = isolationLevel};
            if (!timeOut.HasValue)
                transactionOptions.Timeout = TransactionManager.MaximumTimeout;
            return new TransactionScope(transactionScopeOption, transactionOptions);
        }
    }
}