using ContactManagementAPI.Enums;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ContactManagementAPI.Models
{
    public class ApiResponse : BaseAPIResponse
    {
        public System.Object? Result { get; set; }
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        public ApiResponse() : base()
        {
        }
        public ApiResponse(ErrorCodes code, int statusCode = StatusCodes.Status200OK)
        {
            Code = (int)code;
            Message = code.GetErrorMessage();
            StatusCode = statusCode;
        }

        public ApiResponse(object result)
        {
            Code = (int)ErrorCodes.Success;
            Message = "Success";
            StatusCode = StatusCodes.Status200OK;
            Result = result;
        }
    }

    public class BaseAPIResponse
    {
        public int Code { get; set; } = 0;
        public string CodeString { get; set; } = ErrorCodes.Success.ToString();
        public string Message { get; set; } = "Success";
    }
    public static class ErrorCodeExtensions
    {
        public static string GetErrorMessage(this ErrorCodes errorCode)
        {            
            var formatted = Regex.Replace(errorCode.ToString(), "_", " ").ToLower();
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formatted);
        }
    }
}