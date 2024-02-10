namespace SW.Framework.Cqrs
{
    /// <summary>
    ///     Defines a decorator for query handlers. Each decorator provides the ability to add additional functionality to
    ///     query handlers while not violating the single responsibility principle or the open/closed principle for the
    ///     S.O.L.I.D design patterns.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IQueryDecorator<in TQuery, out TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : class
    {
    }
}