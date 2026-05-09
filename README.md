# Pluma.Library.InternalServiceAuth

Middleware ASP.NET Core e `DelegatingHandler` para autenticação **serviço-a-servidor** Fitflow (header com segredo, comparação em tempo constante).

## Pacote NuGet

`Pluma.Library.InternalServiceAuth` — publicado no **GitHub Packages** ao criar tag `v*` (ver `.github/workflows/main.yml`). Requer secret `PACKAGES_TOKEN` no repositório (igual às outras bibliotecas Pluma).

## Configuração (`appsettings` / variáveis de ambiente)

```json
{
  "FitflowInternalServiceAuth": {
    "Incoming": {
      "Enabled": true,
      "ExpectedSecret": "definir-via-env",
      "HeaderName": "X-Fitflow-Internal-Secret",
      "PathPrefix": "/internal"
    },
    "Outgoing": {
      "Enabled": true,
      "Secret": "definir-via-env",
      "HeaderName": "X-Fitflow-Internal-Secret"
    }
  }
}
```

Mapeamento sugerido de env:

- Pluma (valida firebase-backend): `FitflowInternalServiceAuth__Incoming__ExpectedSecret` = `FITFLOW_INTERNAL_FIREBASE_TO_PLUMA`
- Pluma (chama firebase-backend): `FitflowInternalServiceAuth__Outgoing__Secret` = `FITFLOW_INTERNAL_PLUMA_TO_FIREBASE`
- firebase-backend: inverter Incoming/Outgoing conforme o plano.

## Uso no host

```csharp
builder.Services.AddFitflowInternalServiceAuth(builder.Configuration);

// Cliente HTTP que chama o outro serviço:
builder.Services.AddHttpClient("FitflowPartner")
    .AddFitflowInternalServiceAuthHeader();

var app = builder.Build();
app.UseFitflowInternalServiceAuth(); // cedo no pipeline; antes de rotas /internal
```

## Repositório

https://github.com/alanfalcoski/Pluma.Library.InternalServiceAuth
