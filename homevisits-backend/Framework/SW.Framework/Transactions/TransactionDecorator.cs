using SW.Framework.Cqrs;
using SW.Framework.Utilities;
using System;
using System.Threading.Tasks;

namespace SW.Framework.Transactions
{
    /// <summary>
    ///     A decorator that wraps the command handler within a .NET transaction.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public sealed class TransactionDecorator<TCommand> : ICommandDecorator<TCommand>
        where TCommand : class
    {
        private readonly ICommandHandler<TCommand> _decorated;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransactionDecorator{TCommand}" /> class.
        /// </summary>
        /// <param name="decorated">The decorated.</param>
        public TransactionDecorator(ICommandHandler<TCommand> decorated)
        {
            _decorated = decorated;
        }

        /// <summary>
        ///     Handles the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Handle(TCommand command)
        {
            try
            {
                using (var scope = TransactionFactory.CreateTransaction())
                {
                    _decorated.Handle(command);
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        //public void HandleFault(TCommand command, Exception exception)
        //{
        //    _decorated.HandleFault(command, exception);
        //}
    }
}