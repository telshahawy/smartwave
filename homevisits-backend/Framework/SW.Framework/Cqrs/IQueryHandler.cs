namespace SW.Framework.Cqrs
{
    /// <summary>
    ///     Defines the contract of query handler.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IQueryHandler<in TQuery, out TResponse> where TQuery : class
    {
        /// <summary>
        ///     Reads the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The response.</returns>
        TResponse Read(TQuery query);
    }
}