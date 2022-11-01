using Newtonsoft.Json;
using Poc.Healthcheck.Helpers.Domain.DTOs;
using Poc.Healthcheck.Helpers.Domain.Interface.services;

namespace Poc.Healthcheck.Helpers.Infra.HttpServices
{
    public class WetherForecastHttpService : IWetherForecastHttpService
    {
        private readonly HttpClient _httpClient;

        public WetherForecastHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherForecast> GetAll()
        {
            #nullable disable

            try
            {
                var response = await _httpClient.GetAsync("/wetherforecast", HttpCompletionOption.ResponseContentRead);

                response.EnsureSuccessStatusCode();

                if (response.Content is object)
                {
                    var stream = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<WeatherForecast>(stream,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    return data;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
