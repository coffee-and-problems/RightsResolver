using System.Collections.Generic;
using JetBrains.Annotations;

namespace RightsResolver
{
    public class Result
    {
        public bool IsSuccessful { get; }
        [CanBeNull] public string Message { get; }
        [CanBeNull] public List<UserRights> UserRights { get; }

        public Result(bool isSuccessful, string message = null, List<UserRights> userRights = null)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            UserRights = userRights;
        }

        public static Result GenerateSuccess(List<UserRights> data)
        {
            return new Result(true, userRights: data);
        }

        public static Result GenerateFail(string message)
        {
            return new Result(false, message: message);
        }
    }
}