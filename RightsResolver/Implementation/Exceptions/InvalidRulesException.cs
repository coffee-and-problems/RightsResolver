using System;

namespace RightsResolver
{
    public class InvalidRulesException : ArgumentException
    {
        public InvalidRulesException(string message)
            : base(message)
        { }
    }
}