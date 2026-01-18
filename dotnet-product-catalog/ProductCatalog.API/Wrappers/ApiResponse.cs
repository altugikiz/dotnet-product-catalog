using System.Text.Json.Serialization;

namespace ProductCatalog.API.Wrappers;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }

    // JSON response in body not just header
    public int StatusCode { get; set; }

    // Constructor for successful responses
    public static ApiResponse<T> SuccessResult(T data, int statusCode = 200, string message = "Operation Successful!")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            StatusCode = statusCode,
            Message = message
        };
    }

    // Constructor for failed responses (no Data)
    public static ApiResponse<T> FailResult(string message, int statusCode)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Data = default, // null 
            StatusCode = statusCode,
            Message = message
        };
    }
}