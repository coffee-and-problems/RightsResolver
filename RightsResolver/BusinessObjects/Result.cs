using System.Collections.Generic;
using JetBrains.Annotations;
using RightsResolver.Implementation.Extensions;

namespace RightsResolver.BusinessObjects
{
    public class Result
    {
        public bool IsSuccessful { get; }
        [CanBeNull] public string Message { get; }
        [CanBeNull] public List<UserRights> UserRights { get; }
        [CanBeNull] public ErrorTypes? ErrorType { get; }

        public Result(bool isSuccessful, string message = null, List<UserRights> userRights = null,
            ErrorTypes? errorType = null)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            UserRights = userRights;
            ErrorType = errorType;
        }

        public static Result CreateSuccess([NotNull] List<UserRights> data)
        {
            return new Result(true, userRights: data);
        }

        public static Result CreateFail([NotNull] string message, ErrorTypes errorType = 0)
        {
            message = string.Join(":", errorType.GetDescription(), message);
            return new Result(false, message, errorType: errorType);
        }
    }
}