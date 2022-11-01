using Poc.Healthcheck.Helpers.Domain.DTOs;

namespace Poc.Healthcheck.Helpers.Domain.Interface.services
{
    public interface IWetherForecastHttpService
    {
        Task<WeatherForecast> GetAll();
    }
}
