using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Poc.Helalthcheck.Helpers.Api01.Extensions
{
    public static class HealthcheckConfigurationService
    {
        public static void AddHealthcheckServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
        }
        public static void AddHealthcheckApp(this WebApplication app)
        {
            //app.UseHealthChecks("/status");
            app.UseHealthChecks("/status",
               new HealthCheckOptions()
               {
                   ResponseWriter = async (context, report) =>
                   {
                       var result = JsonConvert.SerializeObject(
                           new
                           {
                               statusApplication = report.Status.ToString(),
                               healchChecks = report.Entries.Select(e => new
                               {
                                   check = e.Key,
                                   ErrorMessage = e.Value.Exception?.Message,
                                   status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                               })
                           });
                       context.Response.ContentType = MediaTypeNames.Application.Json;
                       await context.Response.WriteAsync(result);
                   }
               });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI();
        }
    }
}
