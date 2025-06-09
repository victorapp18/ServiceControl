using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using Polly.CircuitBreaker;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Settings;
using Polly.Fallback;

namespace ServiceControl.Application.Services
{
    public class WeatherService : IClimaService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger;
        private readonly OpenWeatherMapSettings _settings;
        private readonly IAsyncPolicy<double> _policy;

        public WeatherService(
            HttpClient httpClient,
            ILogger<WeatherService> logger,
            IOptions<OpenWeatherMapSettings> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _settings = options.Value;

            // RetryPolicy<double>
            AsyncRetryPolicy<double> retryPolicy = Policy<double>
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (outcome, sleep, attempt, context) =>
                    {
                        _logger.LogWarning($"[Retry {attempt}] falhou: {outcome.Exception?.Message}. " +
                                           $"Aguardando {sleep} antes da próxima tentativa.");
                    });

            // CircuitBreakerPolicy (não genérico) para HttpRequestException
            AsyncCircuitBreakerPolicy circuitBreakerNonGeneric = Policy
                .Handle<HttpRequestException>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromMinutes(1),
                    onBreak: (exception, breakDelay) =>
                    {
                        _logger.LogWarning($"[Circuit Breaker] aberto por {breakDelay.TotalSeconds}s " +
                                           $"devido à exceção: {exception.Message}");
                    },
                    onReset: () =>
                    {
                        _logger.LogInformation("[Circuit Breaker] resetado.");
                    },
                    onHalfOpen: () =>
                    {
                        _logger.LogInformation("[Circuit Breaker] meio-aberto; próxima chamada definirá o estado.");
                    });

            // Converte para genérico em double
            IAsyncPolicy<double> circuitBreakerPolicy = circuitBreakerNonGeneric.AsAsyncPolicy<double>();

            // FallbackPolicy<double>
            AsyncFallbackPolicy<double> fallbackPolicy = Policy<double>
                .Handle<HttpRequestException>()
                .OrResult(r => r == double.MinValue)
                .FallbackAsync(
                    fallbackValue: double.MinValue,
                    onFallbackAsync: async b =>
                    {
                        _logger.LogError("[Fallback] API de clima indisponível. Retornando double.MinValue.");
                        await Task.CompletedTask;
                    });

            // Encadeia: fallback → circuit breaker → retry
            _policy = Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy, retryPolicy);
        }

        public async Task<double> ObterClimaAsync(string cidade)
        {
            return await _policy.ExecuteAsync(async () =>
            {
                // Usa URI relativa porque BaseAddress já está configurado
                var relativeUrl = $"weather?q={cidade}&appid={_settings.ApiKey}&units=metric&lang=pt_br";

                var response = await _httpClient.GetAsync(relativeUrl);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"[WeatherService] Erro na API de clima: {response.StatusCode}");
                    throw new HttpRequestException("Falha ao obter clima do OpenWeatherMap.");
                }

                var data = await response.Content.ReadFromJsonAsync<OpenWeatherResponse>();
                if (data?.Main == null)
                    throw new HttpRequestException("Resposta da API de clima inválida ou vazia.");

                return data.Main.Temp;
            });
        }

        private class OpenWeatherResponse
        {
            public MainData Main { get; set; }
            public class MainData
            {
                public double Temp { get; set; }
            }
        }
    }
}
