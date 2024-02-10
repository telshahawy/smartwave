using System.Collections.Generic;
using System.Threading.Tasks;

namespace SW.Framework.Security
{
    public static class AuthorisationRuleResult
    {
        public static async Task<(bool IsAuthorized, string ErrorCode, Dictionary<object, object> ExceptionData)> Success()
        {
            return await Task.FromResult((true, string.Empty, new Dictionary<object, object>()));
        }

        public static async Task<(bool IsAuthorized, string ErrorCode, Dictionary<object, object> ExceptionData)> Fail(string ErrorCode, Dictionary<object, object> ExceptionData = null)
        {
            return await Task.FromResult((false, ErrorCode, ExceptionData));
        }
    }
}
