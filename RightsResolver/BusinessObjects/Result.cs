using System.Collections.Generic;

namespace RightsResolver
{
    public class Result
    {
        public bool IsSuccessful { get; }
        public string Message { get; }
        public List<UserRights> Data { get; }

        public Result(bool isSuccessful, string message = null, List<UserRights> data = null)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Data = data;
        }

        public static Result GenerateSuccess(List<UserRights> data)
        {
            return new Result(true, data: data);
        }

        public static Result GenerateFail(string message)
        {
            return new Result(false, message: message);
        }
    }
}