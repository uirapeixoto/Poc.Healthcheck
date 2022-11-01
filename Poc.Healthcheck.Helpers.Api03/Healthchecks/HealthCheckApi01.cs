using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Poc.Healthcheck.Helpers.Domain.Interface.services;

namespace Poc.Healthcheck.Helpers.Api03.Healthchecks
{
    public class HealthCheckApi01 : IHealthCheck
    {
        readonly IConfiguration _configuration;

        public HealthCheckApi01(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            try
            {
                using (var httpclient = new HttpClient())
                {
                    var response = await httpclient.GetAsync($"{_configuration["ExternalHttpServices:API01"]}/status", cancellationToken);

                    var _ = response.EnsureSuccessStatusCode();

                    if (_.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                        _.StatusCode == System.Net.HttpStatusCode.InternalServerError ||
                        _.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                    {
                        return HealthCheckResult.Unhealthy();
                    }
                        else if(response.Content is object)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject<HealthCheckStatus>(data);

                        if(obj?.StatusApplication != "Healthy")
                        {
                            return new HealthCheckResult(
                                HealthStatus.Unhealthy, 
                                $"{response.StatusCode} - { response.ReasonPhrase }");
                        }
                    }
                };

                return HealthCheckResult.Healthy();
            }
            catch(Exception e)
            {

                return new HealthCheckResult(HealthStatus.Unhealthy, e.StackTrace);
            }

        }
    }

    internal class HealthCheckStatus
    {
        #nullable disable
        public string StatusApplication { get; set; }
    }
}
