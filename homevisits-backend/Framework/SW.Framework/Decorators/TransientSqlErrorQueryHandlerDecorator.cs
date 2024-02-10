using SW.Framework.Cqrs;
using SW.Framework.Data.SqlServer;
using SW.Framework.TransientFaultHandling;

namespace SW.Framework.Decorators
{
    /// <summary>
    ///     Decorator adding retry logic for SQL Azure / SQL Server transient errors.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class TransientSqlErrorQueryHandlerDecorator<TQuery, TResponse> : IQueryDecorator<TQuery, TResponse>
        where TQuery : class
    {
        private readonly IQueryHandler<TQuery, TResponse> _decorated;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransientSqlErrorQueryHandlerDecorator{TQuery, TResponse}" /> class.
        /// </summary>
        /// <param name="decorated">The decorated.</param>
        public TransientSqlErrorQueryHandlerDecorator(IQueryHandler<TQuery, TResponse> decorated)
        {
            _decorated = decorated;
        }

        /// <summary>
        ///     Reads the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>TResponse.</returns>
        public TResponse Read(TQuery query)
        {
            var policy = RetryPolicy.CreateRetryPolicy(6, 1);
            return policy.ExecuteFunction<SqlTransientExceptionStrategy, TResponse>(() => _decorated.Read(query));
        }
    }
}