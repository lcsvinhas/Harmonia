using Harmonia.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Harmonia.API.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;
    private readonly IWebHostEnvironment _env;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NotFoundException exception:
                Log(context, LogLevel.Warning);
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = exception.Message,
                    Instance = context.HttpContext.Request.Path,
                    Extensions = {
                        ["method"] = context.HttpContext.Request.Method,
                        ["traceId"] = context.HttpContext.TraceIdentifier
                    }
                });
                context.ExceptionHandled = true;
                break;

            case BadRequestException exception:
                Log(context, LogLevel.Warning);
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = exception.Message,
                    Instance = context.HttpContext.Request.Path,
                    Extensions = {
                        ["method"] = context.HttpContext.Request.Method,
                        ["traceId"] = context.HttpContext.TraceIdentifier
                    }
                });
                context.ExceptionHandled = true;
                break;

            default:
                HandleUnexpectedException(context);
                break;
        }
    }

    private void HandleUnexpectedException(ExceptionContext context)
    {
        Log(context, LogLevel.Error);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Ocorreu um erro interno no servidor.",
            Detail = _env.IsDevelopment()
                ? context.Exception.Message
                : "Erro inesperado. Contate o suporte se o problema persistir.",
            Instance = context.HttpContext.Request.Path,
            Extensions = {
                ["method"] = context.HttpContext.Request.Method,
                ["traceId"] = context.HttpContext.TraceIdentifier
            }
        };

        if (_env.IsDevelopment())
        {
            problemDetails.Extensions["stackTrace"] = context.Exception.StackTrace;
        }

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private void Log(ExceptionContext context, LogLevel level)
    {
        _logger.Log(
            level,
            context.Exception,
            "{Method} {Path} | {Controller}.{Action} | IP: {IP} | TraceId: {TraceId}",
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path,
            context.RouteData.Values["controller"],
            context.RouteData.Values["action"],
            context.HttpContext.Connection.RemoteIpAddress,
            context.HttpContext.TraceIdentifier
        );
    }
}
