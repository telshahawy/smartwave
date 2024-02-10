namespace SW.Framework.Validation
{
    /// <summary>
    ///     Defines the contract for a manager providing authorisation checks for authenticated messages.
    /// </summary>
    public interface IValidationManager
    {
        /// <summary>
        ///     Authorizes the specified authenticated message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="validatedMessage">The validated message.</param>
        void Validate<TMessage>(TMessage validatedMessage) where TMessage : class;
    }
}