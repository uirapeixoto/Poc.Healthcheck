using Poc.Healthcheck.Helpers.Domain.Interface.services;
using Poc.Healthcheck.Helpers.Infra.HttpServices;
using System.Net.Http.Headers;

namespace Poc.Healthcheck.Helpers.Api03.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.WebHookTeams(configuration);
            services.WeatherForecast(configuration);
        }

        public static void WebHookTeams(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("teamsWebhook", c =>
            {
                c.BaseAddress = new Uri(configuration["TeamsWebhook:EndPoint"]);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }
        
        public static void WeatherForecast(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IWetherForecastHttpService, WetherForecastHttpService>((s, c) => 
            {
                c.BaseAddress = new Uri("https://localhost:7269");
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }


    }
}
