using System.Threading.Tasks;
using SW.Framework.Cqrs;
using SW.Framework.Security;

namespace SW.Framework.Decorators
{
    public sealed class AuthorisationCommandHandlerDecorator<TCommand> : ICommandDecorator<TCommand>
        where TCommand : class
    {
        private readonly IAuthorisationManager _authorisationManager;
        private readonly ICommandHandler<TCommand> _decorated;

        public AuthorisationCommandHandlerDecorator(ICommandHandler<TCommand> decorated,
            IAuthorisationManager authorisationManager)
        {
            _decorated = decorated;
            _authorisationManager = authorisationManager;
        }

        public  void Handle(TCommand command)
        {
            try
            {
                _authorisationManager.Authorize(command);
                 _decorated.Handle(command);
            }
            catch (System.Exception)
            {
                throw;
            }
           
        }

        //public void HandleFault(TCommand command, Exception exception)
        //{
        //    _decorated.HandleFault(command,exception);
        //}
    }
}