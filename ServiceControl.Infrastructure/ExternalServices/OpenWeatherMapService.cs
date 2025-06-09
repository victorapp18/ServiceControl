using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Settings;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceControl.Infrastructure.ExternalServices
{
    public class OpenWeatherMapService : IClimaService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherMapSettings _settings;

        public OpenWeatherMapService(HttpClient httpClient, IOptions<OpenWeatherMapSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public async Task<double> ObterClimaAsync(string cidade)
        {
            var url = $"{_settings.BaseUrl}?q={cidade}&appid={_settings.ApiKey}&units=metric";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);
            return json["main"]?["temp"]?.Value<double>() ?? 0.0;
        }
    }
}
