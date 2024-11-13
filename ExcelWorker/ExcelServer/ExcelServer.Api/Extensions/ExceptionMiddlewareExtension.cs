using ExcelServer.Api.Common.Models;
using ExcelServer.UseCases.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ExcelServer.Api.Extensions
{
    internal static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app,
            ILogger<Program> logger)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.ContentType = "application/problem + json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionFeature != null)
                    {
                        context.Response.StatusCode = exceptionFeature.Error switch
                        {
                            InvalidExcelFormatException => StatusCodes.Status422UnprocessableEntity,
                            TurnoverDocumentNotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError("Something went worng {ex}", exceptionFeature.Error.Message);

                        string message;

                        if (context.Response.StatusCode == StatusCodes.Status500InternalServerError ||
                            string.IsNullOrWhiteSpace(exceptionFeature.Error.Message))
                        {
                            message = "Something went wrong";
                        }
                        else
                        {
                            message = exceptionFeature.Error.Message;
                        }

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = message
                        }.ToString());
                    }
                });
            });
        }
    }
}