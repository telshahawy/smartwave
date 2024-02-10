using SW.Framework.Cqrs;
using SW.Framework.Validation;

namespace SW.Framework.Decorators
{
    public sealed class ValidationQueryHandlerDecorator<TQuery, TResponse> : IQueryDecorator<TQuery, TResponse>
        where TQuery : class
    {
        private readonly IValidationManager _validationManager;
        private readonly IQueryHandler<TQuery, TResponse> _decorated;

        public ValidationQueryHandlerDecorator(IQueryHandler<TQuery, TResponse> decorated,
            IValidationManager validationManager)
        {
            _decorated = decorated;
            _validationManager = validationManager;
        }

        public TResponse Read(TQuery query)
        {
            _validationManager.Validate(query);
            return _decorated.Read(query);
        }
    }
}