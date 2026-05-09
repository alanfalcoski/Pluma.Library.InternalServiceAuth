using Microsoft.Extensions.Options;

namespace Pluma.Library.InternalServiceAuth;

/// <summary>
/// Adiciona o header de segredo a pedidos HttpClient (chamadas Pluma ↔ firebase-backend).
/// </summary>
public sealed class InternalServiceAuthDelegatingHandler : DelegatingHandler
{
    private readonly IOptions<InternalServiceAuthOutgoingOptions> _options;

    public InternalServiceAuthDelegatingHandler(IOptions<InternalServiceAuthOutgoingOptions> options)
    {
        _options = options;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var o = _options.Value;
        if (o.Enabled && !string.IsNullOrEmpty(o.Secret))
            request.Headers.TryAddWithoutValidation(o.HeaderName, o.Secret);

        return base.SendAsync(request, cancellationToken);
    }
}
