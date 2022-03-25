using Ergus.Backend.Core.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Ergus.Backend.WebApi.Auth.Middlewares
{
    /// <summary>
    /// Middleware para fazer o tratamento de erros HTTP ou de exceptions e padronizar o retorno.
    /// </summary>
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            try
            {
                await _next.Invoke(context);

                if (context.Response.StatusCode >= 200 && context.Response.StatusCode <= 299)
                    return;

                switch (context.Response.StatusCode)
                {
                    case (int)HttpStatusCode.Unauthorized:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new UnauthorizedApiResponse(), jsonSettings));
                        break;
                    case (int)HttpStatusCode.Forbidden:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ForbiddenApiResponse(), jsonSettings));
                        break;
                    case (int)HttpStatusCode.NotFound:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new NotFoundApiResponse(context.Request.Path), jsonSettings));
                        break;
                    case (int)HttpStatusCode.UnsupportedMediaType:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new UnsupportedMediaTypeApiResponse(context.Request.ContentType!), jsonSettings));
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetBaseException().Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new InternalServerErrorApiResponse(ex), jsonSettings));
            }
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware(typeof(CustomExceptionHandlerMiddleware));
    }
}
