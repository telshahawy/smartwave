using System.Threading.Tasks;

namespace SW.Framework.Validation
{
    /// <summary>
    ///     Defines the contract for an authorisation test for an authenticated user trying to execute an operation.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IValidationRule<in TMessage> where TMessage : class
    {
        //string ErrorCode { get; }
        Task<(bool IsValid, int ErrorCode)> Validate(TMessage message);
    }
}