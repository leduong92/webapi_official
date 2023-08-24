using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private const string apiKeyName = AuthConstants.ApiKeyValueName;
        public ApiKeyAuthMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogError("MIddleware 2 work");
                if (!context.Request.Headers.TryGetValue(apiKeyName, out var extractedApiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Api Key not given with Middleware ");
                    return;
                }
                var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
                var key = appSettings.GetValue<string>(apiKeyName);

                if (!key.Equals(extractedApiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Api Key is not valid /Unauthorized with ApiKeyAuthMiddleware");
                    return;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, ex.Message);
                // context.Response.ContentType = "application/json";
                // context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // var response = _env.IsDevelopment()
                //     ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                //     : new ApiException((int)HttpStatusCode.InternalServerError);

                // var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                // var json = JsonSerializer.Serialize(response, options);

                // await context.Response.WriteAsync(json);
            }
        }
    }
}