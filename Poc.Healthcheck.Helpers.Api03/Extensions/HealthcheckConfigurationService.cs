using HealthChecks.UI.Core;
using Poc.Healthcheck.Helpers.Api03.Healthchecks;

namespace Poc.Helalthcheck.Helpers.Api.Extensions
{
    public static class HealthcheckConfigurationService
    {
        public static void AddHealthcheckServices(this IServiceCollection services, IConfiguration configuration)
        {

            var webhookTeamsUrl = configuration["TeamsWebhook:EndPoint"];
            var healthcheckdbstorage = configuration.GetConnectionString("HealthCheckStore");

            var healthCheck = services.AddHealthChecksUI(setupSettings: setup =>
            {
                //setup.DisableDatabaseMigrations();
                setup.MaximumHistoryEntriesPerEndpoint(6);

                setup.AddWebhookNotification("Teams", webhookTeamsUrl,
                        payload: File.ReadAllText(Path.Combine(".", "MessageCard", "ServiceDown.json")),
                        restorePayload: File.ReadAllText(Path.Combine(".", "MessageCard", "ServiceRestore.json")),
                        customMessageFunc: (message, report) =>
                        {
                            Console.WriteLine($"Enviando mensagem para o webhook. {message}");
                            var failing = report.Entries.Where(e => e.Value.Status == UIHealthStatus.Unhealthy);
                            return $"{AppDomain.CurrentDomain.FriendlyName}: {failing.Count()} healthchecks are failing";
                        });
            })
            .AddSqliteStorage(healthcheckdbstorage)
            .AddInMemoryStorage();

            var builder = services.AddHealthChecks();

            //500 mb
            builder.AddProcessAllocatedMemoryHealthCheck(500 * 1024 * 1024, "Process Memory", tags: new[] { "self" });
            //500 mb
            builder.AddPrivateMemoryHealthCheck(1500 * 1024 * 1024, "Private memory", tags: new[] { "self" });

            // da aplicação através de Health Checks
            builder.AddSqlServer(configuration.GetConnectionString("Default"), name: "DataBase", tags: new[] { "services" }); 

            // verificando a integração com o viacep
            builder.AddCheck<HealthCheckViaCEP>("ViaCepHttpClient", tags: new[] { "services" });
            
            // verificando a integração do a api01
            builder.AddCheck<HealthCheckApi01>("Api01", tags: new[] { "services" });
        }
        public static void AddHealthcheckApp(this WebApplication app)
        {
            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI();
        }
    }
}
