namespace SW.Framework.Security
{
    /// <summary>
    ///     Defines the contract for a manager providing authorisation checks for authenticated messages.
    /// </summary>
    public interface IAuthorisationManager
    {
        /// <summary>
        ///     Authorizes the specified authenticated message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="authenticatedMessage">The authenticated message.</param>
        void Authorize<TMessage>(TMessage authenticatedMessage) where TMessage : class;
    }
}