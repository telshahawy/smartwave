using System.Threading.Tasks;
using SW.Framework.Cqrs;
using SW.Framework.Validation;

namespace SW.Framework.Decorators
{
    public sealed class ValidationCommandHandlerDecorator<TCommand> : ICommandDecorator<TCommand>
        where TCommand : class
    {
        private readonly IValidationManager _validationManager;
        private readonly ICommandHandler<TCommand> _decorated;

        public ValidationCommandHandlerDecorator(ICommandHandler<TCommand> decorated,
            IValidationManager validationManager)
        {
            _decorated = decorated;
            _validationManager = validationManager;
        }

        public void Handle(TCommand command)
        {
            try
            {
                _validationManager.Validate(command);
                 _decorated.Handle(command);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        //public void HandleFault(TCommand command, Exception exception)
        //{
        //    _decorated.HandleFault(command,exception);
        //}
    }
}