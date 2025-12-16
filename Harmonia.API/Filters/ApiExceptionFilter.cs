using Harmonia.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Harmonia.API.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new NotFoundObjectResult(context.Exception.Message);
        }
        else if (context.Exception is BadRequestException)
        {
            context.Result = new BadRequestObjectResult(context.Exception.Message);
        }
        else
        {
            context.Result = new ObjectResult("Ocorreu um erro interno do servidor.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
