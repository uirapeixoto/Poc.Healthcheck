using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Poc.Helalthcheck.Helpers.Api.Extensions
{
    public static class HealthcheckConfigurationService
    {
        public static void AddHealthcheckServices(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheck = services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.DisableDatabaseMigrations();
                setup.MaximumHistoryEntriesPerEndpoint(6);
            }).AddInMemoryStorage();

            var builder = services.AddHealthChecks();

            //500 mb
            builder.AddProcessAllocatedMemoryHealthCheck(500 * 1024 * 1024, "Process Memory", tags: new[] { "self" });
            //1500 mb
            builder.AddPrivateMemoryHealthCheck(1500 * 1024 * 1024, "Private memory", tags: new[] { "self" });
        }
        public static void AddHealthcheckApp(this WebApplication app)
        {
            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI();
        }
    }
}
