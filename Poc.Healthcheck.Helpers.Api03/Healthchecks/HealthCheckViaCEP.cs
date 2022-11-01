using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Poc.Healthcheck.Helpers.Api03.Healthchecks
{
    public class HealthCheckViaCEP : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    var response = await httpclient.GetAsync("https://viacep.com.br/ws/70635815/json", cancellationToken);
                    var _ = response.EnsureSuccessStatusCode();

                    if (_.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                        _.StatusCode == System.Net.HttpStatusCode.InternalServerError ||
                        _.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                    {
                        var rs = new HealthCheckResult(HealthStatus.Unhealthy, response.StatusCode.ToString());

                        return rs;
                    }
                };

                return HealthCheckResult.Healthy();
            }
            catch
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}
