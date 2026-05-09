using System.ComponentModel.DataAnnotations;

namespace Pluma.Library.InternalServiceAuth;

/// <summary>
/// Opções para validar pedidos HTTP recebidos de outro serviço interno (ex.: firebase-backend → Pluma).
/// </summary>
public sealed class InternalServiceAuthIncomingOptions
{
    /// <summary>Se false, o middleware não valida.</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>Segredo esperado no header.</summary>
    [Required]
    public string ExpectedSecret { get; set; } = string.Empty;

    /// <summary>Nome do header HTTP.</summary>
    public string HeaderName { get; set; } = "X-Fitflow-Internal-Secret";

    /// <summary>Prefixo de caminho (ex.: /internal). Configuração JSON-friendly.</summary>
    public string PathPrefix { get; set; } = "/internal";
}

/// <summary>
/// Opções para anexar o segredo em chamadas HttpClient saíntes (ex.: Pluma → firebase-backend).
/// </summary>
public sealed class InternalServiceAuthOutgoingOptions
{
    /// <summary>Se false, o handler não adiciona header.</summary>
    public bool Enabled { get; set; } = true;

    [Required]
    public string Secret { get; set; } = string.Empty;

    public string HeaderName { get; set; } = "X-Fitflow-Internal-Secret";
}
