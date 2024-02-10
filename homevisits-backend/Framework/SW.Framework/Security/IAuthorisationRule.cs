using System.Collections.Generic;
using System.Threading.Tasks;

namespace SW.Framework.Security
{
    /// <summary>
    ///     Defines the contract for an authorisation test for an authenticated user trying to execute an operation.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IAuthorisationRule<in TMessage> where TMessage : class
    {
        //string ErrorCode { get; }
        Task<(bool IsAuthorized, string ErrorCode, Dictionary<object, object> ExceptionData)> IsAuthorized(TMessage message);
    }
}