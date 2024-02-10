using System.Threading.Tasks;

namespace SW.Framework.Cqrs
{
    /// <summary>
    ///     Defined the contract for a command handler as specified in the CQRS pattern.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<in TCommand> where TCommand : class
    {
        /// <summary>
        ///     Handles the specified command.
        /// </summary>
        /// <param name="command">The command to be handled.</param>
        void Handle(TCommand command);
        //void HandleFault(TCommand command, Exception exception);
    }

}