using System;

namespace SW.Framework.Exceptions
{
    public class ValidationRuleException : Exception
    {
        public int ErrorCode { get; private set; }

        public ValidationRuleException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ValidationRuleException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ValidationRuleException(int errorCode, string message, Exception inner) : base(message, inner)
        {
            ErrorCode = errorCode;
        }
    }

}
