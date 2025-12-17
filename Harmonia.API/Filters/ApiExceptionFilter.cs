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
                context.Result = new NotFoundObjectResult(exception.Message);
                context.ExceptionHandled = true;
                break;

            case BadRequestException exception:
                context.Result = new BadRequestObjectResult(exception.Message);
                context.ExceptionHandled = true;
                break;

            default:
                HandleUnexpectedException(context);
                break;
        }
    }

    private void HandleUnexpectedException(ExceptionContext context)
    {
        _logger.LogError(
                context.Exception,
                "{Method} {Path} | {Controller}.{Action} | IP: {IP}",
                context.HttpContext.Request.Method,
                context.HttpContext.Request.Path,
                context.RouteData.Values["controller"],
                context.RouteData.Values["action"],
                context.HttpContext.Connection.RemoteIpAddress
            );

        if (_env.IsDevelopment())
        {
            var resposta = new
            {
                erro = "Ocorreu um erro interno do servidor.",
                detalhe = context.Exception.Message,
                stackTrace = context.Exception.StackTrace
            };

            context.Result = new ObjectResult(resposta)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        else
        {
            context.Result = new ObjectResult("Ocorreu um erro interno do servidor.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        context.ExceptionHandled = true;
    }
}
