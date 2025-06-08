using Newtonsoft.Json.Linq;
using ServiceControl.Domain.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceControl.Infrastructure.ExternalServices
{
    public class OpenWeatherMapService : IClimaService
    {
        private const string ApiKey = "SUA_API_KEY_AQUI"; // Substituir por segredo seguro
        private readonly HttpClient _httpClient;

        public OpenWeatherMapService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<double> ObterClimaAsync(string cidade)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={ApiKey}&units=metric";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);
            return json["main"]?["temp"]?.Value<double>() ?? 0.0;
        }
    }
}
