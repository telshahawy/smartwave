using System;
using System.Threading.Tasks;

namespace SW.Framework.Validation
{
    public static class ValidationRuleResult
    {
        public static async Task<(bool IsValid, int ErrorCode)> Success()
        {
            return await Task.FromResult((true, 0));
        }

        public static async Task<(bool IsValid, int ErrorCode)> Fail<TEnum>(TEnum ErrorCode)
            where TEnum : struct, IConvertible
        {
            if (typeof(TEnum).IsEnum)
                return await Task.FromResult((false, Convert.ToInt32(ErrorCode)));
            else
                return await Task.FromResult((false, 0));
        }
    }
}
