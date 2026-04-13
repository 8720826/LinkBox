using System.Text.Json;

namespace LinkBox.Common.Exceptions;

/// <summary>
/// 应用异常基类
/// </summary>
public class AppException : Exception
{
    public int StatusCode { get; }

    public AppException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}

/// <summary>
/// 未找到异常
/// </summary>
public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message, 404)
    {
    }
}

/// <summary>
/// 验证异常
/// </summary>
public class ValidationException : AppException
{
    public ValidationException(string message) : base(message, 400)
    {
    }
}

/// <summary>
/// 授权异常
/// </summary>
public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message, 401)
    {
    }
}

/// <summary>
/// 全局异常处理中间件
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ErrorResponse();

        switch (exception)
        {
            case AppException appEx:
                response.StatusCode = appEx.StatusCode;
                errorResponse = new ErrorResponse
                {
                    Success = false,
                    Message = appEx.Message,
                    Code = appEx.StatusCode
                };
                break;

            case KeyNotFoundException:
                response.StatusCode = 404;
                errorResponse = new ErrorResponse
                {
                    Success = false,
                    Message = exception.Message,
                    Code = 404
                };
                break;

            case UnauthorizedAccessException:
                response.StatusCode = 401;
                errorResponse = new ErrorResponse
                {
                    Success = false,
                    Message = exception.Message,
                    Code = 401
                };
                break;

            default:
                response.StatusCode = 500;
                errorResponse = new ErrorResponse
                {
                    Success = false,
                    Message = "服务器内部错误",
                    Code = 500
                };
                _logger.LogError(exception, "发生未处理的异常");
                break;
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var result = JsonSerializer.Serialize(errorResponse, options);
        await response.WriteAsync(result);
    }
}

/// <summary>
/// 错误响应
/// </summary>
public class ErrorResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }
}
