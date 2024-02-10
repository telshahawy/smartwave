using System.Threading.Tasks;

namespace SW.Framework.Cqrs
{
    /// <summary>
    ///     Defines the contract for a query router.
    /// </summary>
    public interface IQueryProcessor
    {
        /// <summary>
        ///     Processes the query.
        /// </summary>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <typeparam name="TQueryResponse">The type of the query response.</typeparam>
        /// <param name="query">The query to be processed.</param>
        /// <returns>The relevant query response.</returns>
        //TQueryResponse ProcessQuery<TQuery, TQueryResponse>(TQuery query) where TQuery : class
        //    where TQueryResponse : class;

        /// <summary>
        ///     Processes the query asynchronous.
        /// </summary>
        /// <typeparam name="TQuery">The type of the t query.</typeparam>
        /// <typeparam name="TQueryResponse">The type of the t query response.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>Task&lt;TQueryResponse&gt;.</returns>
        Task<TQueryResponse> ProcessQueryAsync<TQuery, TQueryResponse>(TQuery query) where TQuery : class
            where TQueryResponse : class;
    }
}