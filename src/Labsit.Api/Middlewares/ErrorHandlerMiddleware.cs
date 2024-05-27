﻿using FluentValidation;
using Labsit.Application.Common.Response;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Labsit.Api.Middlewares
{
    public class ErrorHandlerMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        private const string InternalErrorMessage = "An internal error occurred while processing your request.";

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = MediaTypeNames.Application.Json;

                Result<string> responseModel;

                switch (ex)
                {
                    case ValidationException e:
                        responseModel = Result<string>.Fail(e.Errors);
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        logger.LogError(ex, "An unexpected exception was thrown: {Message}", ex.Message);
                        responseModel = Result<string>.Fail(InternalErrorMessage,HttpStatusCode.InternalServerError);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
