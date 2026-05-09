using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pluma.Library.InternalServiceAuth;

public static class InternalServiceAuthServiceCollectionExtensions
{
    /// <summary>
    /// Configura opções Incoming/Outgoing e regista <see cref="InternalServiceAuthDelegatingHandler"/>.
    /// </summary>
    public static IServiceCollection AddFitflowInternalServiceAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<InternalServiceAuthIncomingOptions>(
            configuration.GetSection("FitflowInternalServiceAuth:Incoming"));
        services.Configure<InternalServiceAuthOutgoingOptions>(
            configuration.GetSection("FitflowInternalServiceAuth:Outgoing"));
        services.AddTransient<InternalServiceAuthDelegatingHandler>();
        return services;
    }

    /// <summary>
    /// Encadeia o handler de segredo no <see cref="IHttpClientBuilder"/> (ex.: cliente que chama o outro serviço).
    /// </summary>
    public static IHttpClientBuilder AddFitflowInternalServiceAuthHeader(this IHttpClientBuilder builder)
        => builder.AddHttpMessageHandler<InternalServiceAuthDelegatingHandler>();
}
