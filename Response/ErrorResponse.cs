using System.ComponentModel.DataAnnotations;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Response
{
    public class ErrorResponse
    {
            public string? Message { get; set; }
            public int ErrorCode { get; set; }
    }

    public class Result<T>
    {
        public Result(int responseCode, T data)
        {
            ResponseCode = responseCode;
            Data = data;
        }
        public Result(int responseCode, string message)
        {
            ResponseCode = responseCode;
        }

        public int ResponseCode { get; set; }
        public T? Data { get; set; }



        public static Result<T> Success(string message, T data)
        {
            return new Result<T>(200, data);
        }
        public static Result<T> Failure(string message)
        {
            return new Result<T>(500, message);
        }
    }

}
