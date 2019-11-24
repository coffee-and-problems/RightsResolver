using System;
using RightsResolver.BusinessObjects;

namespace RightsResolver.Implementation.Exceptions
{
    public class InvalidRulesException : ArgumentException
    {
        public ErrorTypes ErrorType { get; }

        public InvalidRulesException(string message, ErrorTypes errorType)
            : base(message)
        {
            ErrorType = errorType;
        }
    }
}
