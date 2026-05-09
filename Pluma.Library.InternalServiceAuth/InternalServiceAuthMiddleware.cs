using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Pluma.Library.InternalServiceAuth;

public sealed class InternalServiceAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<InternalServiceAuthIncomingOptions> _options;
    private readonly ILogger<InternalServiceAuthMiddleware> _logger;

    public InternalServiceAuthMiddleware(
        RequestDelegate next,
        IOptions<InternalServiceAuthIncomingOptions> options,
        ILogger<InternalServiceAuthMiddleware> logger)
    {
        _next = next;
        _options = options;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var opts = _options.Value;

        if (!opts.Enabled || string.IsNullOrEmpty(opts.ExpectedSecret))
        {
            await _next(context);
            return;
        }

        var pathPrefix = string.IsNullOrWhiteSpace(opts.PathPrefix)
            ? PathString.Empty
            : new PathString(opts.PathPrefix.StartsWith('/') ? opts.PathPrefix : "/" + opts.PathPrefix);

        var path = context.Request.Path;
        if (!path.StartsWithSegments(pathPrefix))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(opts.HeaderName, out var provided) || provided.Count == 0)
        {
            _logger.LogWarning("Pedido internal sem header {Header}", opts.HeaderName);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var secret = provided[^1];
        if (!InternalServiceSecretComparer.EqualsUtf8(secret, opts.ExpectedSecret))
        {
            _logger.LogWarning("Pedido internal com segredo inválido (path={Path})", path);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(context);
    }
}
