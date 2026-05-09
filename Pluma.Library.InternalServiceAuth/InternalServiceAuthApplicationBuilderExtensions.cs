using Microsoft.AspNetCore.Builder;

namespace Pluma.Library.InternalServiceAuth;

public static class InternalServiceAuthApplicationBuilderExtensions
{
    /// <summary>
    /// Middleware de validação de segredo para rotas sob o prefixo em <see cref="InternalServiceAuthIncomingOptions.PathPrefix"/>.
    /// </summary>
    public static IApplicationBuilder UseFitflowInternalServiceAuth(this IApplicationBuilder app)
        => app.UseMiddleware<InternalServiceAuthMiddleware>();
}
