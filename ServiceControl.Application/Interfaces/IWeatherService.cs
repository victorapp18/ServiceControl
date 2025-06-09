using System.Threading.Tasks;

namespace ServiceControl.Application.Services
{
    public interface IWeatherService
    {
        Task<(double temperature, string condition)> GetTemperatureAndConditionAsync(string city);
    }
}
