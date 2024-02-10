using SW.Framework.Cqrs;
using SW.Framework.Security;

namespace SW.Framework.Decorators
{
    public sealed class AuthorisationQueryHandlerDecorator<TQuery, TResponse> : IQueryDecorator<TQuery, TResponse>
        where TQuery : class
    {
        private readonly IAuthorisationManager _authorisationManager;
        private readonly IQueryHandler<TQuery, TResponse> _decorated;

        public AuthorisationQueryHandlerDecorator(IQueryHandler<TQuery, TResponse> decorated,
            IAuthorisationManager authorisationManager)
        {
            _decorated = decorated;
            _authorisationManager = authorisationManager;
        }

        public TResponse Read(TQuery query)
        {
            _authorisationManager.Authorize(query);
            return _decorated.Read(query);
        }
    }
}